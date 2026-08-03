#!/usr/bin/env python3
"""
CaveCode Installer Framework v2

Reusable utilities for future CaveCode passes.

Capabilities:
- repository discovery
- marker-based text replacement
- section insertion
- idempotent writes
- automatic backups
- protected-file hashing
- rollback manifests
- validation helpers
- structured install reports
"""

from __future__ import annotations

import hashlib
import json
import shutil
from dataclasses import dataclass, field
from pathlib import Path
from typing import Callable, Iterable


class InstallerError(RuntimeError):
    """Raised when a patch cannot be applied safely."""


@dataclass
class FileChange:
    path: str
    action: str
    before_sha256: str | None
    after_sha256: str | None


@dataclass
class InstallReport:
    name: str
    version: str
    root: str
    backup_dir: str
    changed_files: list[FileChange] = field(default_factory=list)
    skipped_files: list[str] = field(default_factory=list)
    validations: list[str] = field(default_factory=list)

    def to_dict(self) -> dict:
        return {
            "name": self.name,
            "version": self.version,
            "root": self.root,
            "backup_dir": self.backup_dir,
            "changed_files": [
                {
                    "path": item.path,
                    "action": item.action,
                    "before_sha256": item.before_sha256,
                    "after_sha256": item.after_sha256,
                }
                for item in self.changed_files
            ],
            "skipped_files": list(self.skipped_files),
            "validations": list(self.validations),
        }


def sha256_bytes(data: bytes) -> str:
    return hashlib.sha256(data).hexdigest()


def sha256_file(path: Path) -> str:
    return sha256_bytes(path.read_bytes())


def find_repo_root(
    start: Path,
    required_paths: Iterable[str] = (
        "CaveCode.csproj",
        "wwwroot/index.html",
    ),
) -> Path:
    required = tuple(Path(item) for item in required_paths)

    for candidate in [start.resolve(), *start.resolve().parents]:
        if all((candidate / item).exists() for item in required):
            return candidate

    raise InstallerError(
        "Could not locate the CaveCode repository. "
        "Run the installer from /workspaces/CaveCode-Academy."
    )


def require_once(
    text: str,
    marker: str,
    *,
    label: str | None = None,
) -> int:
    count = text.count(marker)

    if count != 1:
        name = label or marker
        raise InstallerError(
            f"Expected exactly one {name!r} marker, found {count}."
        )

    return text.index(marker)


def replace_between_markers(
    text: str,
    start_marker: str,
    end_marker: str,
    replacement_body: str,
    *,
    keep_markers: bool = True,
) -> str:
    start_index = require_once(
        text,
        start_marker,
        label="start marker",
    )
    end_index = require_once(
        text,
        end_marker,
        label="end marker",
    )

    if end_index <= start_index:
        raise InstallerError(
            "End marker appears before start marker."
        )

    body_start = start_index + len(start_marker)

    if keep_markers:
        return (
            text[:body_start]
            + replacement_body
            + text[end_index:]
        )

    return (
        text[:start_index]
        + replacement_body
        + text[end_index + len(end_marker):]
    )


def insert_after_marker(
    text: str,
    marker: str,
    addition: str,
    *,
    idempotency_marker: str | None = None,
) -> str:
    if (
        idempotency_marker
        and idempotency_marker in text
    ):
        return text

    index = require_once(text, marker)

    return (
        text[: index + len(marker)]
        + addition
        + text[index + len(marker):]
    )


def insert_before_marker(
    text: str,
    marker: str,
    addition: str,
    *,
    idempotency_marker: str | None = None,
) -> str:
    if (
        idempotency_marker
        and idempotency_marker in text
    ):
        return text

    index = require_once(text, marker)

    return (
        text[:index]
        + addition
        + text[index:]
    )


def replace_exact_once(
    text: str,
    old: str,
    new: str,
    *,
    label: str = "target block",
) -> str:
    count = text.count(old)

    if count != 1:
        raise InstallerError(
            f"Expected exactly one {label}, found {count}."
        )

    return text.replace(old, new, 1)


class InstallerSession:
    def __init__(
        self,
        *,
        name: str,
        version: str,
        root: Path,
        backup_dir: Path,
        allowed_paths: Iterable[str],
    ) -> None:
        self.name = name
        self.version = version
        self.root = root.resolve()
        self.backup_dir = backup_dir
        self.allowed_paths = {
            Path(item)
            for item in allowed_paths
        }
        self.report = InstallReport(
            name=name,
            version=version,
            root=str(self.root),
            backup_dir=str(backup_dir),
        )
        self._before_protected = (
            self._protected_hashes()
        )

    def _is_ignored(self, relative: Path) -> bool:
        return (
            relative in self.allowed_paths
            or (
                relative.parts
                and relative.parts[0].startswith(".")
            )
            or "bin" in relative.parts
            or "obj" in relative.parts
            or "__pycache__" in relative.parts
            or relative.suffix in {".pyc", ".pyo"}
            or relative.name.startswith(
                "apply-cavecode-"
            )
            or relative.name.startswith(
                "repair-cavecode-"
            )
        )

    def _protected_hashes(self) -> dict[str, str]:
        hashes: dict[str, str] = {}

        for path in self.root.rglob("*"):
            if not path.is_file():
                continue

            relative = path.relative_to(self.root)

            if self._is_ignored(relative):
                continue

            hashes[str(relative)] = sha256_file(path)

        return hashes

    def backup_once(self, relative: str | Path) -> None:
        relative = Path(relative)
        source = self.root / relative

        if not source.exists():
            return

        destination = (
            self.root
            / self.backup_dir
            / relative
        )

        if destination.exists():
            return

        destination.parent.mkdir(
            parents=True,
            exist_ok=True,
        )
        shutil.copy2(source, destination)

    def write_text(
        self,
        relative: str | Path,
        content: str,
        *,
        newline: str = "\n",
    ) -> None:
        relative = Path(relative)

        if relative not in self.allowed_paths:
            raise InstallerError(
                f"{relative} is not in allowed_paths."
            )

        destination = self.root / relative
        before_hash = (
            sha256_file(destination)
            if destination.exists()
            else None
        )

        existing = (
            destination.read_text(encoding="utf-8")
            if destination.exists()
            else None
        )

        if existing == content:
            self.report.skipped_files.append(
                str(relative)
            )
            return

        self.backup_once(relative)

        destination.parent.mkdir(
            parents=True,
            exist_ok=True,
        )
        destination.write_text(
            content,
            encoding="utf-8",
            newline=newline,
        )

        after_hash = sha256_file(destination)

        self.report.changed_files.append(
            FileChange(
                path=str(relative),
                action=(
                    "updated"
                    if before_hash
                    else "created"
                ),
                before_sha256=before_hash,
                after_sha256=after_hash,
            )
        )

    def patch_text(
        self,
        relative: str | Path,
        patcher: Callable[[str], str],
    ) -> None:
        relative = Path(relative)
        source = self.root / relative

        if not source.exists():
            raise InstallerError(
                f"Required file does not exist: {relative}"
            )

        original = source.read_text(
            encoding="utf-8"
        )
        updated = patcher(original)
        self.write_text(relative, updated)

    def validate(
        self,
        description: str,
        condition: bool,
    ) -> None:
        if not condition:
            raise InstallerError(
                f"Validation failed: {description}"
            )

        self.report.validations.append(
            description
        )

    def finish(self) -> Path:
        after = self._protected_hashes()

        if self._before_protected != after:
            changed = sorted(
                set(self._before_protected) ^ set(after)
                |
                {
                    key
                    for key in (
                        set(self._before_protected)
                        & set(after)
                    )
                    if (
                        self._before_protected[key]
                        != after[key]
                    )
                }
            )

            raise InstallerError(
                "Protected files changed unexpectedly: "
                + ", ".join(changed[:20])
            )

        manifest = (
            self.root
            / self.backup_dir
            / "install-manifest.json"
        )
        manifest.parent.mkdir(
            parents=True,
            exist_ok=True,
        )
        manifest.write_text(
            json.dumps(
                self.report.to_dict(),
                indent=2,
            ),
            encoding="utf-8",
            newline="\n",
        )

        return manifest


def restore_from_manifest(
    root: Path,
    manifest_path: Path,
) -> None:
    manifest = json.loads(
        manifest_path.read_text(
            encoding="utf-8"
        )
    )

    backup_root = manifest_path.parent

    for item in manifest.get(
        "changed_files",
        [],
    ):
        relative = Path(item["path"])
        target = root / relative
        backup = backup_root / relative

        if backup.exists():
            target.parent.mkdir(
                parents=True,
                exist_ok=True,
            )
            shutil.copy2(backup, target)
        elif target.exists():
            target.unlink()


def print_report(report: InstallReport) -> None:
    print(
        f"{report.name} {report.version} "
        "completed successfully."
    )
    print()

    if report.changed_files:
        print("Changed:")
        for item in report.changed_files:
            print(
                f"  - {item.action}: {item.path}"
            )

    if report.skipped_files:
        print("Already current:")
        for item in report.skipped_files:
            print(f"  - {item}")

    if report.validations:
        print("Validated:")
        for item in report.validations:
            print(f"  - {item}")

    print()
    print(f"Backup: {report.backup_dir}/")
