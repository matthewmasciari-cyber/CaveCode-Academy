# CaveCode Academy — Pass 6C

Pass 6C activates automated validation and scoring for Interface Rescue
Mission 1.

## Run

```bash
cd /workspaces/CaveCode-Academy
python3 apply-cavecode-html-css-pass-6c.py
dotnet build
dotnet run
```

Hard-refresh with `Ctrl + Shift + R`, then open:

```text
/minigames/html-css
```

## Live repair requirements

1. semantic page landmarks and three project articles
2. valid Projects, Skills, Contact, and primary-action destinations
3. meaningful image alternative text
4. wide-screen header, hero, card grid, and readable primary button
5. visible `:focus-visible` keyboard treatment
6. responsive narrow-screen stacking without fixed body overflow

Each requirement displays live pass/pending evidence while the learner edits.

## Score

- 140 points per completed requirement
- 840 requirement points maximum
- 160 time-bonus points maximum
- 40-point deduction per hint
- 1,000 total points maximum
- all six requirements are mandatory for a mission pass

Submitting freezes the timer and records a final pass/fail result for the
current attempt. Reset starts a new attempt.

## Deliberately excluded

- XP
- Code Crystals
- achievements
- saved best scores
- mission completion persistence
- Chapter 1 locking
- homepage changes
- Learning Paths changes
- course-progress changes

Pass 6D will integrate rewards, persistent best scores, mission completion,
and the intended HTML/CSS Chapter 1 unlock requirement.
