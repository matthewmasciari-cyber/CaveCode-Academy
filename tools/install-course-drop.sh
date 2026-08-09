#!/usr/bin/env bash
set -euo pipefail
ROOT="/workspaces/CaveCode-Academy"
cd "$ROOT"

echo "== CaveCode course drop installer =="

# Find zip anywhere
FOUND=$(find /workspaces /home "$ROOT" -name "Maker-Hardware-FULL*.zip" -o -name "Arduino*FLAT*.zip" -o -name "*-course-drop.zip" 2>/dev/null | head -1 || true)
if [ -z "${FOUND:-}" ]; then
  FOUND=$(find /workspaces /home "$ROOT" -name "*.zip" 2>/dev/null | xargs -r ls -t 2>/dev/null | head -1 || true)
fi
if [ -z "${FOUND:-}" ]; then
  echo "ERROR: no course zip found. Upload Maker-Hardware-FULL.zip first."
  exit 1
fi
echo "Found: $FOUND"
cp "$FOUND" /tmp/course-drop.zip

rm -rf /tmp/course-drop-src
mkdir -p /tmp/course-drop-src
unzip -q /tmp/course-drop.zip -d /tmp/course-drop-src

SRC=$(find /tmp/course-drop-src -type d -name CourseEngine | head -1 | xargs dirname)
echo "SRC=$SRC"
test -d "$SRC/CourseEngine"
test -d "$SRC/Pages"

# Copy C# / razor / css / js
cp -f "$SRC"/Pages/*.razor Pages/ 2>/dev/null || true
cp -f "$SRC"/CourseEngine/*.cs CourseEngine/ 2>/dev/null || true
# Prefer MERGED overwrites for ids/catalog if present
if [ -f "$SRC/CourseEngine/CourseIds.MERGED.cs" ]; then
  cp -f "$SRC/CourseEngine/CourseIds.MERGED.cs" CourseEngine/CourseIds.cs
fi
if [ -f "$SRC/CourseEngine/CourseCatalogService.MERGED.cs" ]; then
  cp -f "$SRC/CourseEngine/CourseCatalogService.MERGED.cs" CourseEngine/CourseCatalogService.cs
fi
mkdir -p wwwroot/css wwwroot/js
cp -f "$SRC"/wwwroot/css/*.css wwwroot/css/ 2>/dev/null || true
if [ -f "$SRC/wwwroot/js/caveCodeCourseEngine.MERGED.js" ]; then
  cp -f "$SRC/wwwroot/js/caveCodeCourseEngine.MERGED.js" wwwroot/js/caveCodeCourseEngine.js
fi
if [ -f "$SRC/wwwroot/js/caveCodeProgression.MERGED.js" ]; then
  cp -f "$SRC/wwwroot/js/caveCodeProgression.MERGED.js" wwwroot/js/caveCodeProgression.js
fi
# Home if provided as full file
if [ -f "$SRC/Pages/Home.razor" ]; then
  cp -f "$SRC/Pages/Home.razor" Pages/Home.razor
fi
# csproj excludes if provided
if [ -f "$SRC/CaveCode.csproj.FRAGMENT.xml" ]; then
  echo "(csproj fragment present — apply manually if not already excluded)"
fi

# index.html links
if ! grep -q 'arduino-course-shell.css' wwwroot/index.html 2>/dev/null; then
  sed -i 's|css/gcl-course-shell.css?v=2" />|css/gcl-course-shell.css?v=2" />\n    <link rel="stylesheet" href="css/arduino-course-shell.css?v=1" />|' wwwroot/index.html || true
fi
if ! grep -q 'raspi-course-shell.css' wwwroot/index.html 2>/dev/null; then
  sed -i 's|css/arduino-course-shell.css?v=1" />|css/arduino-course-shell.css?v=1" />\n    <link rel="stylesheet" href="css/raspi-course-shell.css?v=1" />|' wwwroot/index.html || true
fi
if grep -q 'caveCodeProgression.js?v=cloud-sync-4' wwwroot/index.html 2>/dev/null; then
  sed -i 's|caveCodeProgression.js?v=cloud-sync-4|caveCodeProgression.js?v=cloud-sync-6|' wwwroot/index.html || true
fi
if grep -q 'caveCodeProgression.js?v=cloud-sync-5' wwwroot/index.html 2>/dev/null; then
  sed -i 's|caveCodeProgression.js?v=cloud-sync-5|caveCodeProgression.js?v=cloud-sync-6|' wwwroot/index.html || true
fi

rm -rf /tmp/course-drop-src /tmp/course-drop.zip
# do not leave extract in repo
rm -rf arduino-tmp arduino-deploy gcl-deploy

echo "== building =="
dotnet build

echo "== done =="
echo "Commit when green:"
echo "  git add Pages CourseEngine wwwroot/css wwwroot/js wwwroot/index.html"
echo "  git commit -m \"Add full Arduino + Raspberry Pi applied courses\""
echo "  git push origin main"
