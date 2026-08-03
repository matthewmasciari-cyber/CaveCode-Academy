# CaveCode Academy — Pass 6D

Pass 6D completes the first Interface Rescue mission integration.

## Run

```bash
cd /workspaces/CaveCode-Academy
python3 apply-cavecode-html-css-pass-6d.py
dotnet build
dotnet run
```

Hard-refresh with `Ctrl + Shift + R`.

Open:

```text
/minigames/html-css
```

## Chapter gate

Interface Rescue requires HTML & CSS Workshop Chapter 1. The route and the
Minigames hub both check that Module 8 is complete before allowing entry.

## Persistent Mission 1 record

Stored under:

```text
cavecode.htmlcss.minigame.v1
```

The record includes:

- Mission 1 completion
- Best score
- Fastest clear
- Total clears
- Total minigame XP earned
- Total Code Crystals earned
- Last score, time, and damage profile
- A bounded processed-attempt history used for repeat-safe reward delivery

## Rewards

First clear:

- 100 HTML/CSS XP
- 6 Code Crystals
- Validated HTML and CSS line credit
- `Interface Restored` achievement unlock
- Claimable `Interface Rescuer` title

Repeat clear:

- 40 HTML/CSS XP
- 2 Code Crystals
- Validated HTML and CSS line credit

The existing progression and crystal reward keys prevent the same clear from
being awarded twice. A new successful attempt receives a new clear key.

## Modified files

- `Pages/HtmlCssMinigame.razor`
- `Pages/Minigames.razor`
- `wwwroot/js/caveCodeHtmlCssMinigame.js`
- `wwwroot/js/caveCodeAchievements.js`
- `wwwroot/index.html`
- `docs/html-css-pass-6d.md`

## Protected

The installer hashes and protects every unrelated page, course, component,
service, JavaScript file, stylesheet, and root project file. In particular it
does not modify:

- `Pages/Home.razor`
- Learning Paths files
- C#, Python, or C++ pages
- HTML/CSS course lessons
- Course progress
- Existing minigames
- Existing progression behavior
