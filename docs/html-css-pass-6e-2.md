# CaveCode Academy — HTML & CSS Pass 6E-2

## Interface Rescue Mission 3

Route: `/minigames/html-css/mission-3`

Mission 3, **Mobile Navigation Recovery**, adds a complete responsive-navigation repair challenge while preserving Missions 1 and 2.

### Repair requirements

1. Restore the semantic site header, brand link, and labeled primary navigation.
2. Build a native `details` and `summary` mobile menu that works without JavaScript.
3. Restore Overview, Projects, and Contact links with matching section targets.
4. Rebuild the wide-screen header and horizontal navigation row.
5. Restore readable controls and visible keyboard focus.
6. Add the mobile breakpoint, wrapped header, full-width navigation, stacked links, and remove fixed-width overflow.

### Damage profiles

- Full Incident
- HTML Systems
- CSS Systems

### Scoring

- Six requirements at 140 points each
- Up to 160 time-bonus points
- 40-point deduction per revealed hint
- All six requirements must pass

### Persistence and rewards

Mission 3 uses separate record fields and reward keys inside `cavecode.htmlcss.minigame.v1`.

- First clear: 100 HTML/CSS XP and 6 Code Crystals
- Repeat clear: 40 HTML/CSS XP and 2 Code Crystals
- Validated HTML and CSS lines are also awarded

Mission 1 and Mission 2 records remain intact.

### Isolation

Pass 6E-2 changes only:

- `Pages/HtmlCssMinigame.razor`
- `Pages/HtmlCssMinigameMissionTwo.razor`
- `Pages/HtmlCssMinigameMissionThree.razor`
- `Pages/Minigames.razor`
- `wwwroot/js/caveCodeHtmlCssMinigame.js`
- this documentation file

The homepage, Learning Paths, course lessons, progression services, other minigames, and unrelated assets are protected by hashes.
