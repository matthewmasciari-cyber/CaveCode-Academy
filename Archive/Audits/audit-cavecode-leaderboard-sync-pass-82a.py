#!/usr/bin/env python3
from pathlib import Path
import re
import sys


LEADERBOARD = Path("Pages/Leaderboard.razor")
AUTH_JS = Path("wwwroot/js/caveCodeAuth.js")
REPORT = Path("docs/leaderboard-sync-pass-82a-audit.txt")


def find_root() -> Path:
    root = Path.cwd().resolve()

    while root.parent != root and not (root / "CaveCode.csproj").is_file():
        root = root.parent

    if not (root / "CaveCode.csproj").is_file():
        raise RuntimeError(
            "Could not find CaveCode.csproj. "
            "Run this from /workspaces/CaveCode-Academy."
        )

    return root


def line_number(text: str, index: int) -> int:
    return text.count("\n", 0, index) + 1


def collect_matches(text: str, patterns: list[tuple[str, str]]) -> list[str]:
    findings: list[str] = []

    for label, pattern in patterns:
        for match in re.finditer(pattern, text, flags=re.IGNORECASE | re.MULTILINE):
            line = line_number(text, match.start())
            excerpt = text[match.start():match.end()].replace("\n", " ").strip()
            findings.append(f"{line:>5} | {label:<30} | {excerpt[:180]}")

    return findings


def extract_context(text: str, pattern: str, radius: int = 6) -> list[str]:
    lines = text.splitlines()
    output: list[str] = []

    for match in re.finditer(pattern, text, flags=re.IGNORECASE | re.MULTILINE):
        line = line_number(text, match.start())
        start = max(1, line - radius)
        end = min(len(lines), line + radius)

        output.append(f"--- lines {start}-{end} ---")

        for number in range(start, end + 1):
            output.append(f"{number:>5}: {lines[number - 1]}")

    return output


def analyze_leaderboard(text: str) -> dict[str, object]:
    patterns = [
        ("refresh handler", r"\b(?:Refresh|Reload|Load|Sync)[A-Za-z0-9_]*\s*\("),
        ("lifecycle method", r"\bOn(?:Initialized|AfterRender|ParametersSet)Async?\b"),
        ("JS interop", r"\bJS(?:Runtime)?\.Invoke(?:Async|VoidAsync)"),
        ("entry assignment", r"\b(?:entries|players|leaderboard|rows)\s*="),
        ("list clear", r"\.(?:Clear|RemoveAll)\s*\("),
        ("list add", r"\.(?:Add|AddRange)\s*\("),
        ("current user filter", r"\b(?:currentUser|currentPlayer|profile|viewer)\b"),
        ("local storage", r"\b(?:localStorage|sessionStorage)\b"),
        ("sort/order", r"\b(?:OrderBy|OrderByDescending|Sort)\b"),
        ("state refresh", r"\bStateHasChanged\s*\("),
    ]

    return {
        "matches": collect_matches(text, patterns),
        "refresh_context": extract_context(
            text,
            r"\b(?:Refresh|Reload|Load|Sync)[A-Za-z0-9_]*\s*\(",
            radius=9,
        ),
        "lifecycle_context": extract_context(
            text,
            r"\bOn(?:Initialized|AfterRender|ParametersSet)Async?\b",
            radius=8,
        ),
    }


def analyze_auth(text: str) -> dict[str, object]:
    patterns = [
        ("leaderboard function", r"\b[A-Za-z0-9_]*(?:leaderboard|ranking|scoreboard)[A-Za-z0-9_]*\s*[:=]?\s*(?:async\s*)?\("),
        ("fetch call", r"\bfetch\s*\("),
        ("storage read", r"\b(?:localStorage|sessionStorage)\.getItem\s*\("),
        ("storage write", r"\b(?:localStorage|sessionStorage)\.setItem\s*\("),
        ("current user object", r"\b(?:currentUser|currentPlayer|profile|viewer)\b"),
        ("array replacement", r"\b[A-Za-z0-9_]+\s*=\s*\[[^\]]*\]"),
        ("array filter", r"\.filter\s*\("),
        ("array map", r"\.map\s*\("),
        ("array merge", r"\b(?:concat|push|spread|merge|dedup)\b"),
        ("JSON parse", r"\bJSON\.parse\s*\("),
        ("JSON stringify", r"\bJSON\.stringify\s*\("),
    ]

    return {
        "matches": collect_matches(text, patterns),
        "leaderboard_context": extract_context(
            text,
            r"\b[A-Za-z0-9_]*(?:leaderboard|ranking|scoreboard)[A-Za-z0-9_]*\b",
            radius=10,
        ),
        "storage_context": extract_context(
            text,
            r"\b(?:localStorage|sessionStorage)\.(?:getItem|setItem)\s*\(",
            radius=7,
        ),
    }


def likely_risks(leaderboard: str, auth: str) -> list[str]:
    risks: list[str] = []

    if re.search(r"\.(?:Clear|RemoveAll)\s*\(", leaderboard):
        risks.append(
            "Leaderboard.razor clears an existing collection during load/refresh. "
            "If the next data source returns only the signed-in profile, the visible list collapses."
        )

    if re.search(r"\b(?:entries|players|leaderboard|rows)\s*=", leaderboard, re.I):
        risks.append(
            "Leaderboard.razor directly replaces a leaderboard collection. "
            "The refresh path should be checked for merge-versus-replace behavior."
        )

    if re.search(r"\.filter\s*\(", auth) and re.search(
        r"\b(?:currentUser|currentPlayer|profile|viewer)\b",
        auth,
        re.I,
    ):
        risks.append(
            "caveCodeAuth.js contains both filtering and current-user logic. "
            "A current-user filter may be applied to the shared leaderboard payload."
        )

    if re.search(r"\blocalStorage\.getItem\s*\(", auth) and not re.search(
        r"\bfetch\s*\(",
        auth,
        re.I,
    ):
        risks.append(
            "caveCodeAuth.js appears to rely on browser-local data without an obvious remote fetch. "
            "Fresh-page behavior may differ from same-tab behavior."
        )

    if re.search(r"\bfetch\s*\(", auth) and re.search(
        r"\blocalStorage\.getItem\s*\(",
        auth,
        re.I,
    ):
        risks.append(
            "Both remote and local data sources are present. "
            "The refresh bug may be caused by one source overwriting the other instead of reconciling them."
        )

    if not risks:
        risks.append(
            "No single overwrite pattern was obvious from keyword analysis. "
            "Use the included context sections to identify the actual refresh function and payload shape."
        )

    return risks


def main() -> None:
    root = find_root()
    leaderboard_path = root / LEADERBOARD
    auth_path = root / AUTH_JS
    report_path = root / REPORT

    if not leaderboard_path.is_file():
        raise RuntimeError(f"Missing {LEADERBOARD}")

    if not auth_path.is_file():
        raise RuntimeError(f"Missing {AUTH_JS}")

    leaderboard = leaderboard_path.read_text(encoding="utf-8")
    auth = auth_path.read_text(encoding="utf-8")

    leaderboard_analysis = analyze_leaderboard(leaderboard)
    auth_analysis = analyze_auth(auth)
    risks = likely_risks(leaderboard, auth)

    report: list[str] = [
        "=" * 96,
        "CAVECODE LEADERBOARD SYNCHRONIZATION — PASS 82A AUDIT",
        "=" * 96,
        "",
        "Purpose",
        "-------",
        "Identify why the leaderboard can show normal entries initially but collapse to only the",
        "current user after a refresh. This audit does not modify project files.",
        "",
        f"Leaderboard file: {LEADERBOARD}",
        f"Authentication file: {AUTH_JS}",
        "",
        "=" * 96,
        "LIKELY RISK AREAS",
        "=" * 96,
    ]

    for index, risk in enumerate(risks, start=1):
        report.append(f"{index}. {risk}")

    report += [
        "",
        "=" * 96,
        "LEADERBOARD.RAZOR — MATCH SUMMARY",
        "=" * 96,
    ]

    report.extend(leaderboard_analysis["matches"] or ["No matching patterns found."])

    report += [
        "",
        "=" * 96,
        "LEADERBOARD.RAZOR — LOAD / REFRESH CONTEXT",
        "=" * 96,
    ]

    report.extend(
        leaderboard_analysis["refresh_context"]
        or ["No named load/refresh methods found."]
    )

    report += [
        "",
        "=" * 96,
        "LEADERBOARD.RAZOR — LIFECYCLE CONTEXT",
        "=" * 96,
    ]

    report.extend(
        leaderboard_analysis["lifecycle_context"]
        or ["No Blazor lifecycle methods found."]
    )

    report += [
        "",
        "=" * 96,
        "CAVECODEAUTH.JS — MATCH SUMMARY",
        "=" * 96,
    ]

    report.extend(auth_analysis["matches"] or ["No matching patterns found."])

    report += [
        "",
        "=" * 96,
        "CAVECODEAUTH.JS — LEADERBOARD CONTEXT",
        "=" * 96,
    ]

    report.extend(
        auth_analysis["leaderboard_context"]
        or ["No leaderboard-named functions or variables found."]
    )

    report += [
        "",
        "=" * 96,
        "CAVECODEAUTH.JS — STORAGE CONTEXT",
        "=" * 96,
    ]

    report.extend(
        auth_analysis["storage_context"]
        or ["No local/session storage calls found."]
    )

    report += [
        "",
        "=" * 96,
        "NEXT REPAIR TARGET",
        "=" * 96,
        "",
        "The next pass should change only the exact refresh/load function identified above.",
        "The repair should:",
        "  1. preserve the full shared leaderboard payload;",
        "  2. merge or update the current user's row without discarding other rows;",
        "  3. deduplicate by stable user ID, not display name;",
        "  4. sort only after reconciliation;",
        "  5. keep the last valid shared list if refresh returns partial/local-only data;",
        "  6. avoid touching XP awards, authentication, course progress, or minigames.",
        "",
    ]

    report_path.parent.mkdir(parents=True, exist_ok=True)
    report_path.write_text("\n".join(report), encoding="utf-8", newline="\n")

    print("Leaderboard Synchronization Pass 82A audit completed.")
    print()
    print(f"Report: {REPORT}")
    print()
    print("No project files were modified.")
    print()
    print("Run:")
    print(f"  cat {REPORT}")
    print()
    print("Paste the report output back into ChatGPT for the targeted 82B repair.")


if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
