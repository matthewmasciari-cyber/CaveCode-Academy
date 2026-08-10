#!/usr/bin/env python3
"""Generate wwwroot/updates.json from full git history for the Updates page.

Runs at GitHub Pages deploy time into publish/wwwroot only (not committed back).
"""

from __future__ import annotations

import json
import os
import re
import subprocess
import sys
from datetime import datetime, timezone

from pathlib import Path

CODE_EXTS = {
    ".cs", ".razor", ".css", ".js", ".ts", ".json", ".yml", ".yaml",
    ".md", ".html", ".py", ".sql", ".xml", ".scss",
}

SKIP_DIRS = {
    ".git", "node_modules", "bin", "obj", "publish", ".vs",
    "e2e/node_modules", "TestResults",
}


def count_repo_lines() -> tuple[int, int]:
    root = Path(".").resolve()
    total = 0
    files = 0
    for path in root.rglob("*"):
        if not path.is_file():
            continue
        if any(part in SKIP_DIRS for part in path.parts):
            continue
        if path.suffix.lower() not in CODE_EXTS:
            continue
        try:
            if path.stat().st_size > 2_000_000:
                continue
            text = path.read_text(encoding="utf-8", errors="ignore")
        except Exception:
            continue
        n = text.count("
")
        if text and not text.endswith("
"):
            n += 1
        total += n
        files += 1
    return total, files



CONVENTIONAL = re.compile(
    r"^(?P<type>feat|fix|improve|polish|add|update|docs|chore|ci|refactor|perf|security|ui|ux)"
    r"(?:\([^)]+\))?!?\s*:\s*(?P<rest>.+)$",
    re.I,
)

TYPE_LABEL = {
    "feat": "Feature",
    "add": "Feature",
    "fix": "Fix",
    "improve": "Improve",
    "polish": "Polish",
    "update": "Update",
    "docs": "Docs",
    "chore": "Chore",
    "ci": "CI",
    "refactor": "Refactor",
    "perf": "Performance",
    "security": "Security",
    "ui": "UI",
    "ux": "UX",
}


def run(cmd: list[str]) -> str:
    return subprocess.check_output(cmd, text=True, stderr=subprocess.PIPE).strip()


def is_skipped(subject: str) -> bool:
    s = subject.lower().strip()
    if not s:
        return True
    return any(
        s.startswith(p)
        for p in (
            "merge ",
            "merge branch",
            "merge pull request",
            "wip",
            "tmp",
            "temp ",
            "retrigger",
            "empty commit",
        )
    )


def parse_title(subject: str) -> tuple[str, str | None]:
    subject = subject.strip()
    m = CONVENTIONAL.match(subject)
    if m:
        kind = TYPE_LABEL.get(m.group("type").lower())
        rest = m.group("rest").strip()
        if rest:
            rest = rest[0].upper() + rest[1:]
        return (rest or subject), kind

    cleaned = re.sub(
        r"^(merge|chore|ci|docs|style)\b[\s:/-]*",
        "",
        subject,
        flags=re.I,
    ).strip()
    if cleaned:
        cleaned = cleaned[0].upper() + cleaned[1:]
    return (cleaned or subject), None


def clean_body(body: str) -> str | None:
    if not body or not body.strip():
        return None
    lines: list[str] = []
    for line in body.replace("\r\n", "\n").split("\n"):
        line = line.strip()
        if not line:
            if lines:
                break
            continue
        if re.match(r"^(co-authored-by|signed-off-by|made-with):", line, re.I):
            break
        lines.append(line)
    text = " ".join(lines).strip()
    if len(text) < 8:
        return None
    if len(text) > 280:
        text = text[:277].rstrip() + "…"
    return text


def main() -> int:
    raw_limit = os.environ.get("UPDATE_LIMIT", "250").strip().lower()
    limit = 5000 if raw_limit in {"all", "0", "inf"} else int(raw_limit)

    try:
        sha = run(["git", "rev-parse", "HEAD"])
    except Exception:
        sha = os.environ.get("GITHUB_SHA", "unknown")

    try:
        # One commit per block, easy to parse
        log = run(
            [
                "git",
                "log",
                f"-n{max(limit * 2, limit)}",
                "--date=iso-strict",
                "--pretty=format:%H%n%cI%n%s%n%b%n==END==",
            ]
        )
    except Exception as e:
        print(f"git log failed: {e}", file=sys.stderr)
        log = ""

    entries: list[dict] = []
    seen: set[str] = set()

    for block in log.split("==END=="):
        lines = [ln.rstrip() for ln in block.strip("\n").split("\n")]
        if len(lines) < 3:
            continue
        full_sha, when, subject = lines[0].strip(), lines[1].strip(), lines[2].strip()
        body = "\n".join(lines[3:]).strip()

        if len(full_sha) < 7 or is_skipped(subject):
            continue

        title, kind = parse_title(subject)
        key = title.lower()
        if key in seen:
            continue
        seen.add(key)

        entry: dict = {
            "id": full_sha[:12],
            "sha": full_sha[:7],
            "title": title,
            "at": when,
            "url": (
                "https://github.com/matthewmasciari-cyber/"
                f"CaveCode-Academy/commit/{full_sha}"
            ),
        }
        if kind:
            entry["kind"] = kind
        desc = clean_body(body)
        if desc and desc.lower() != title.lower():
            entry["description"] = desc

        entries.append(entry)
        if len(entries) >= limit:
            break

    try:
        code_lines, file_count = count_repo_lines()
    except Exception:
        code_lines, file_count = 0, 0

    payload = {
        "generatedAt": datetime.now(timezone.utc).strftime("%Y-%m-%dT%H:%M:%SZ"),
        "deploySha": (sha[:12] if sha else None),
        "source": "git-log",
        "count": len(entries),
        "repo": {
            "codeLines": code_lines,
            "fileCount": file_count,
        },
        "updates": entries,
    }
    sys.stdout.write(json.dumps(payload, indent=2, ensure_ascii=False) + "\n")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
