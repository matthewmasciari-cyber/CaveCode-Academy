# CaveCode Installer Framework v2

This framework replaces brittle, full-block text matching with reusable,
marker-based installer utilities.

## Installed files

- `tools/cavecode_installer_v2.py`
- `tools/cavecode_rollback.py`
- `tools/cavecode_installer_example.py`
- `tools/test_cavecode_installer_v2.py`

## Core features

- repository discovery;
- exact marker validation;
- insertion before or after stable markers;
- replacement between explicit markers;
- repeat-safe file writes;
- automatic one-time backups;
- protected-file hashing;
- JSON installation manifests;
- manifest-based rollback;
- structured validation reports.

## Future pass standard

Every new pass should:

1. declare its allowed files;
2. create an `InstallerSession`;
3. use stable markers rather than matching entire functions;
4. write or patch only allowed files;
5. run pass-specific validations;
6. call `finish()` to verify protected files;
7. print the resulting report and manifest path.

## Rollback

```bash
python3 tools/cavecode_rollback.py \
  .some-pass-backup/install-manifest.json
```

## Self-test

```bash
python3 tools/test_cavecode_installer_v2.py
```

This pass does not modify CaveCode runtime or gameplay behavior.
