# CaveCode Academy — Pass 6B

Pass 6B turns the Interface Rescue foundation into the first fully authored
HTML/CSS minigame mission.

## Run

```bash
cd /workspaces/CaveCode-Academy
python3 apply-cavecode-html-css-pass-6b.py
dotnet build
dotnet run
```

Hard-refresh with `Ctrl + Shift + R`, then open:

```text
/minigames/html-css
```

## Mission 1

**Restore the Workshop Landing Page**

The learner receives six repair goals covering:

1. semantic page landmarks
2. valid in-page navigation
3. useful image alternative text
4. wide-screen hero and project-card layouts
5. visible keyboard focus
6. responsive narrow-screen behavior

## Damage profiles

- **Full Incident** — HTML structure, accessibility, layout, contrast, focus,
  and responsive behavior are all damaged.
- **HTML Systems** — layout support remains while semantic and accessibility
  failures are isolated.
- **CSS Systems** — semantic HTML remains while layout, contrast, focus, and
  responsive failures are isolated.

## Added mission tools

- incident report for the selected damage profile
- complete repair requirements and acceptance descriptions
- desktop, tablet, and mobile preview widths
- target blueprint
- reset and profile reload behavior
- mission-specific hints
- fixed high-contrast Minigames hub button

## Deliberately excluded

- automated validation
- score calculation
- pass/fail completion
- XP
- Code Crystals
- achievements
- best-score saving
- chapter locking
- homepage changes
- Learning Paths changes
- course-progress changes

Pass 6C will activate requirement validation, live feedback, scoring, time
bonuses, and hint penalties. Pass 6D will add progression and rewards.
