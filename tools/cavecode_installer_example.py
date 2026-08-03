#!/usr/bin/env python3
"""
Reference template for future CaveCode passes.

Copy this file, rename it, then update:
- PASS_NAME
- PASS_VERSION
- BACKUP_DIR
- ALLOWED_PATHS
- patch logic
- validations
"""

from pathlib import Path

from cavecode_installer_v2 import (
    InstallerError,
    InstallerSession,
    find_repo_root,
    insert_after_marker,
    print_report,
)


PASS_NAME = "Example CaveCode Pass"
PASS_VERSION = "v1"
BACKUP_DIR = Path(".example-pass-backup")

ALLOWED_PATHS = {
    "docs/example-pass-output.txt",
}


def main() -> None:
    root = find_repo_root(Path.cwd())

    session = InstallerSession(
        name=PASS_NAME,
        version=PASS_VERSION,
        root=root,
        backup_dir=BACKUP_DIR,
        allowed_paths=ALLOWED_PATHS,
    )

    session.write_text(
        "docs/example-pass-output.txt",
        "CaveCode Installer Framework v2 example.\n",
    )

    session.validate(
        "example output exists",
        (
            root
            / "docs/example-pass-output.txt"
        ).is_file(),
    )

    manifest = session.finish()
    print_report(session.report)
    print(f"Manifest: {manifest}")


if __name__ == "__main__":
    try:
        main()
    except InstallerError as error:
        print(f"ERROR: {error}")
        raise SystemExit(1)
