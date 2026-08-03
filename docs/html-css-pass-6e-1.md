# HTML & CSS Interface Rescue — Pass 6E-1

## Mission 2

Pass 6E-1 adds **Project Grid Recovery** at:

```text
/minigames/html-css/mission-2
```

Mission 1 remains at `/minigames/html-css` and keeps its existing records,
rewards, achievement, and Chapter 1 access requirement.

## Mission 2 requirements

1. Restore a semantic projects section and exactly three article cards.
2. Give each card a heading, description, and action link.
3. Add meaningful alternative text to all three project images.
4. Rebuild a three-column desktop CSS Grid with visible spacing.
5. Contain images, keep consistent card structure, preserve action contrast,
   and add visible keyboard focus.
6. Stack the grid into one column at a mobile breakpoint without fixed-width
   overflow.

## Profiles and scoring

- Full Incident
- HTML Systems
- CSS Systems
- Desktop, tablet, and mobile previews
- 840 requirement points
- Up to 160 time-bonus points
- 40-point penalty per hint
- All six requirements are required to pass

## Records and rewards

Mission 2 stores its own completion flag, best score, fastest clear, clear
count, last score, last elapsed time, and last profile inside the existing
`cavecode.htmlcss.minigame.v1` record.

- First Mission 2 clear: 100 HTML/CSS XP and 6 Code Crystals
- Repeat Mission 2 clear: 40 HTML/CSS XP and 2 Code Crystals
- Validated HTML and CSS lines are also awarded

Mission 1 records remain backward compatible and are not overwritten.

## Isolation

Pass 6E-1 modifies only:

- `Pages/HtmlCssMinigame.razor`
- `Pages/HtmlCssMinigameMissionTwo.razor`
- `Pages/Minigames.razor`
- `wwwroot/js/caveCodeHtmlCssMinigame.js`
- `docs/html-css-pass-6e-1.md`

It does not modify the homepage, Learning Paths, course lessons, progression
services, achievements, global stylesheets, or other minigames.
