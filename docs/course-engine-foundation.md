# CaveCode shared course engine — Pass 1

This foundation prepares CaveCode to add C++ and HTML/CSS without copying the
entire C# and Python implementation.

## Added

- Stable course IDs and aliases
- One course catalog
- One eight-stage training model
- One language-neutral lesson format
- Version-tolerant, non-destructive progress normalization
- A reusable code-validation contract
- Shared progress, stage-tab, and editor components
- A browser course registry and save bridge

## Intentionally unchanged

- `Pages/CSharp.razor`
- `Pages/Python.razor`
- Existing lesson wording and validation
- XP, crystals, achievements, minigames, authentication, and Supabase
- Existing C# and Python storage keys

## Planned phases

1. Shared course engine foundation
2. C++ course shell
3. C++ Chapter 1
4. HTML/CSS course shell
5. HTML/CSS Chapter 1
6. Chapter-by-chapter expansion
7. C++ and HTML/CSS minigames

## Save keys

```text
cavecode.csharp.progress.v1
cavecode.python.progress.v1
cavecode.cpp.progress.v1
cavecode.htmlcss.progress.v1
```

The engine preserves unknown fields and overlapping progress.

## Browser checks

```javascript
caveCodeCourseEngine.version
caveCodeCourseEngine.getCatalog()
caveCodeCourseEngine.inspectProgress("csharp")
caveCodeCourseEngine.inspectProgress("python")
```

Expected version:

```text
course-engine-foundation-v1
```
