#!/usr/bin/env python3
"""Generate wwwroot/updates.json from recent git commits.

Runs during GitHub Pages deploy so the Updates page stays automatic.
Does not commit back to the repo (avoids deploy loops).
"""

from __future__ import annotations

import json
import os
import re
import subprocess
import sys
from datetime import datetime, timezone


def run(cmd: list[str]) -> str:
    return subprocess.check_output(cmd, text=True, stderr=subprocess.DEVNULL).strip()


def clean_subject(subject: str) -> str:
    subject = subject.strip()
    # Drop noisy prefixes while keeping the useful part
    subject = re.sub(
        r"^(merge|chore|ci|docs|style)\b[\s:/-]*",
        "",
        subject,
        flags=re.I,
    ).strip()
    # Capitalize first letter if present
    if subject:
        subject = subject[0].upper() + subject[1:]
    return subject or "Update"


def is_skipped(subject: str) -> bool:
    s = subject.lower().strip()
    if not s:
        return True
    skip_prefixes = (
        "merge ",
        "merge branch",
        "merge pull request",
        "wip",
        "tmp",
        "temp ",
        "retrigger",
        "empty commit",
    )
    return any(s.startswith(p) for p in skip_prefixes)


def main() -> int:
    limit = int(os.environ.get("UPDATE_LIMIT", "40"))
    try:
        sha = run(["git", "rev-parse", "HEAD"])
    except Exception:
        sha = os.environ.get("GITHUB_SHA", "unknown")

    try:
        # ISO-ish date, subject, short hash
        log = run(
            [
                "git",
                "log",
                f"-n{limit * 2}",
                "--date=iso-strict",
                "--pretty=format:%H%x09%cI%x09%s",
            ]
        )
    except Exception as e:
        print(f"git log failed: {e}", file=sys.stderr)
        log = ""

    entries = []
    seen_subjects: set[str] = set()

    for line in log.splitlines():
        if not line.strip() or "\t" not in line:
            continue
        parts = line.split("\t", 2)
        if len(parts) < 3:
            continue
        full_sha, when, subject = parts[0], parts[1], parts[2]
        if is_skipped(subject):
            continue
        title = clean_subject(subject)
        key = title.lower()
        if key in seen_subjects:
            continue
        seen_subjects.add(key)

        entries.append(
            {
                "id": full_sha[:12],
                "sha": full_sha[:7],
                "title": title,
                "at": when,
                "url": f"https://github.com/matthewmasciari-cyber/CaveCode-Academy/commit/{full_sha}",
            }
        )
        if len(entries) >= limit:
            break

    payload = {
        "generatedAt": datetime.now(timezone.utc).strftime("%Y-%m-%dT%H:%M:%SZ"),
        "deploySha": sha[:12] if sha else None,
        "source": "git-log",
        "updates": entries,
    }

    out = json.dumps(payload, indent=2, ensure_ascii=False)
    sys.stdout.write(out + "\n")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
