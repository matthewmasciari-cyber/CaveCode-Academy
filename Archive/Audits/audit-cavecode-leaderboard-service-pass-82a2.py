#!/usr/bin/env python3
from pathlib import Path
import re
import sys

REPORT = Path("docs/leaderboard-service-pass-82a2-audit.txt")

SEARCH_ROOTS = [
    Path("Services"),
    Path("Models"),
    Path("Data"),
    Path("Pages"),
    Path("."),
]

TARGET_PATTERNS = [
    r"\bGetLeaderboardAsync\b",
    r"\bLeaderboardResult\b",
    r"\bLeaderboardEntry\b",
    r"\bgetLeaderboardProfiles\b",
    r"\bUpsertLeaderboard\b",
    r"\bpublic leaderboard\b",
    r"\bcurrent player\b",
]


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


def candidate_files(root: Path) -> list[Path]:
    seen: set[Path] = set()
    files: list[Path] = []

    for relative_root in SEARCH_ROOTS:
        base = (root / relative_root).resolve()

        if not base.exists():
            continue

        for path in base.rglob("*"):
            if not path.is_file():
                continue

            if path.suffix.lower() not in {".cs", ".razor", ".js"}:
                continue

            if any(part in {"bin", "obj", ".git", "node_modules"} for part in path.parts):
                continue

            resolved = path.resolve()

            if resolved in seen:
                continue

            seen.add(resolved)
            files.append(resolved)

    return sorted(files)


def line_number(text: str, index: int) -> int:
    return text.count("\n", 0, index) + 1


def extract_block(lines: list[str], center: int, radius: int = 18) -> list[str]:
    start = max(1, center - radius)
    end = min(len(lines), center + radius)

    output = [f"--- lines {start}-{end} ---"]

    for number in range(start, end + 1):
        output.append(f"{number:>5}: {lines[number - 1]}")

    return output


def scan_file(path: Path, root: Path) -> dict[str, object] | None:
    try:
        text = path.read_text(encoding="utf-8")
    except UnicodeDecodeError:
        return None

    hits: list[tuple[str, int, str]] = []

    for pattern in TARGET_PATTERNS:
        for match in re.finditer(pattern, text, flags=re.IGNORECASE | re.MULTILINE):
            hits.append(
                (
                    pattern,
                    line_number(text, match.start()),
                    match.group(0),
                )
            )

    if not hits:
        return None

    lines = text.splitlines()
    contexts: list[str] = []

    for _, line, token in sorted(hits, key=lambda item: item[1]):
        contexts.append(f"### {token} at line {line}")
        contexts.extend(extract_block(lines, line, radius=20))
        contexts.append("")

    return {
        "path": path.relative_to(root),
        "hits": hits,
        "contexts": contexts,
        "text": text,
    }


def risk_findings(scans: list[dict[str, object]]) -> list[str]:
    risks: list[str] = []

    combined = "\n".join(str(scan["text"]) for scan in scans)

    patterns = [
        (
            r"Entries\s*=\s*new\s*\[\]\s*\{[^}]*current",
            "A result appears to be created with only the current player.",
        ),
        (
            r"Entries\s*=\s*new\s+List<[^>]+>\s*\(\s*\)\s*;",
            "A leaderboard list is initialized empty and may later receive only the local player.",
        ),
        (
            r"\.(?:Clear|RemoveAll)\s*\(",
            "A leaderboard or cache collection is explicitly cleared.",
        ),
        (
            r"\b(?:cloud|remote)[A-Za-z0-9_]*\s*=\s*await",
            "Cloud data is loaded separately; inspect whether a later local assignment overwrites it.",
        ),
        (
            r"\b(?:local|fallback)[A-Za-z0-9_]*\s*=",
            "Local/fallback data is constructed separately; inspect merge-versus-replace behavior.",
        ),
        (
            r"\bDistinctBy\s*\(",
            "Stable deduplication exists; confirm the key is user ID rather than display name.",
        ),
        (
            r"\bGroupBy\s*\(",
            "Grouped reconciliation exists; confirm the grouping key is the stable user ID.",
        ),
        (
            r"\bOrderByDescending\s*\(",
            "Sorting exists; ensure it happens after cloud and local reconciliation.",
        ),
        (
            r"\bgetLeaderboardProfiles\b",
            "The service calls the JavaScript cloud profile query.",
        ),
    ]

    for pattern, message in patterns:
        if re.search(pattern, combined, flags=re.IGNORECASE | re.MULTILINE | re.DOTALL):
            risks.append(message)

    if not risks:
        risks.append(
            "No obvious overwrite signature was found by static pattern matching. "
            "Use the extracted method contexts below to identify the cloud/local return paths."
        )

    return risks


def main() -> None:
    root = find_root()
    scans: list[dict[str, object]] = []

    for path in candidate_files(root):
        result = scan_file(path, root)
        if result:
            scans.append(result)

    if not scans:
        raise RuntimeError(
            "Could not locate GetLeaderboardAsync, leaderboard models, "
            "or related service code."
        )

    risks = risk_findings(scans)

    report: list[str] = [
        "=" * 100,
        "CAVECODE LEADERBOARD SERVICE TRACE — PASS 82A-2",
        "=" * 100,
        "",
        "Purpose",
        "-------",
        "Trace the service and model logic behind ProgressionService.GetLeaderboardAsync without",
        "modifying Leaderboard.razor, caveCodeAuth.js, or any other project file.",
        "",
        "=" * 100,
        "FILES LOCATED",
        "=" * 100,
    ]

    for scan in scans:
        report.append(f"- {scan['path']}")

    report += [
        "",
        "=" * 100,
        "STATIC RISK SIGNALS",
        "=" * 100,
    ]

    for index, risk in enumerate(risks, start=1):
        report.append(f"{index}. {risk}")

    for scan in scans:
        report += [
            "",
            "=" * 100,
            f"FILE: {scan['path']}",
            "=" * 100,
        ]
        report.extend(scan["contexts"])

    report += [
        "",
        "=" * 100,
        "TARGETED REPAIR RULES FOR PASS 82B",
        "=" * 100,
        "",
        "The repair should be limited to the service/reconciliation method identified above.",
        "It should:",
        "  1. keep the last valid shared cloud list if a later refresh is unavailable or partial;",
        "  2. merge the current user's local row into the shared list by stable ID;",
        "  3. never replace a multi-user cloud list with a single local row;",
        "  4. deduplicate by stable user ID;",
        "  5. sort after merging;",
        "  6. preserve filters, public visibility, XP totals, lines, and titles;",
        "  7. avoid changing authentication, awards, progress saves, minigames, or course logic.",
        "",
    ]

    report_path = root / REPORT
    report_path.parent.mkdir(parents=True, exist_ok=True)
    report_path.write_text("\n".join(report), encoding="utf-8", newline="\n")

    print("Leaderboard Service Trace Pass 82A-2 completed.")
    print()
    print(f"Report: {REPORT}")
    print()
    print("No project files were modified.")
    print()
    print("Run:")
    print(f"  cat {REPORT}")
    print()
    print("Paste the report output back into ChatGPT for Pass 82B.")


if __name__ == "__main__":
    try:
        main()
    except Exception as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
