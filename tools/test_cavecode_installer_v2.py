#!/usr/bin/env python3
"""
Self-test for CaveCode Installer Framework v2.
"""

from __future__ import annotations

import tempfile
from pathlib import Path

from cavecode_installer_v2 import (
    InstallerSession,
    insert_after_marker,
    replace_between_markers,
)


def main() -> None:
    with tempfile.TemporaryDirectory() as temp:
        root = Path(temp)

        (root / "CaveCode.csproj").write_text(
            "<Project />",
            encoding="utf-8",
        )

        (root / "wwwroot").mkdir()
        (root / "wwwroot/index.html").write_text(
            "<main>\n"
            "<!-- START -->\n"
            "old\n"
            "<!-- END -->\n"
            "</main>\n",
            encoding="utf-8",
        )

        (root / "protected.txt").write_text(
            "unchanged",
            encoding="utf-8",
        )

        session = InstallerSession(
            name="Framework self-test",
            version="v2",
            root=root,
            backup_dir=Path(".self-test-backup"),
            allowed_paths={
                "wwwroot/index.html",
                "docs/result.txt",
            },
        )

        session.patch_text(
            "wwwroot/index.html",
            lambda text:
                replace_between_markers(
                    text,
                    "<!-- START -->",
                    "<!-- END -->",
                    "\nnew\n",
                ),
        )

        session.write_text(
            "docs/result.txt",
            "created\n",
        )

        session.validate(
            "section replacement",
            "new" in (
                root / "wwwroot/index.html"
            ).read_text(encoding="utf-8"),
        )

        session.finish()

        assert (
            root / "protected.txt"
        ).read_text(encoding="utf-8") == "unchanged"

    print(
        "CaveCode Installer Framework v2 self-test passed."
    )


if __name__ == "__main__":
    main()
