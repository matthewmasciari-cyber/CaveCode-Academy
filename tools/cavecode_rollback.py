#!/usr/bin/env python3
"""
Rollback helper for CaveCode Installer Framework v2 manifests.
"""

from __future__ import annotations

import sys
from pathlib import Path

from cavecode_installer_v2 import (
    InstallerError,
    find_repo_root,
    restore_from_manifest,
)


def main() -> None:
    if len(sys.argv) != 2:
        raise InstallerError(
            "Usage: python3 tools/cavecode_rollback.py "
            "<path-to-install-manifest.json>"
        )

    root = find_repo_root(Path.cwd())
    manifest = Path(sys.argv[1])

    if not manifest.is_absolute():
        manifest = root / manifest

    if not manifest.exists():
        raise InstallerError(
            f"Manifest not found: {manifest}"
        )

    restore_from_manifest(
        root,
        manifest,
    )

    print("Rollback completed successfully.")
    print(f"Manifest: {manifest}")


if __name__ == "__main__":
    try:
        main()
    except InstallerError as error:
        print(f"ERROR: {error}", file=sys.stderr)
        raise SystemExit(1)
