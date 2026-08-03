# CaveCode Academy — Pass 6A

Pass 6A adds the isolated HTML/CSS minigame foundation.

## Run

```bash
cd /workspaces/CaveCode-Academy
python3 apply-cavecode-html-css-pass-6a.py
dotnet build
dotnet run
```

Hard-refresh with `Ctrl + Shift + R`, then open:

```text
/minigames/html-css
```

## Added

- `Interface Rescue` minigame shell
- HTML editor
- CSS editor
- sandboxed live preview
- mission objectives
- timer start, pause, and reset controls
- hint display
- placeholder scoreboard
- HTML/CSS card on the Minigames hub
- responsive desktop, tablet, and mobile layout

## Safety

The preview iframe uses an empty sandbox. Before rendering, the page removes:

- script elements
- inline event-handler attributes
- `javascript:` URLs
- CSS `@import`
- CSS external `url(...)` requests

## Deliberately excluded

- requirement validation
- scoring
- XP
- Code Crystals
- achievements
- best-score saving
- mission completion
- homepage changes
- Learning Paths changes
- course-progress changes

Pass 6B will add the first complete challenge content and damaged-state variants.
Pass 6C will activate structural scoring and feedback.
Pass 6D will integrate progression and rewards.
