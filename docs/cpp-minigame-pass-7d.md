# C++ Engine Foundry — Pass 7D

Pass 7D completes Mission 1 reward and persistence integration.

## First clear

- 100 C++ XP
- 6 Code Crystals
- validated C++ line credit
- Engine Restored achievement
- Engine Technician title

## Repeat clear

- 40 C++ XP
- 2 Code Crystals
- validated C++ line credit

## Saved record

Stored under `cavecode.cpp.minigame.v1`:

- Mission 1 completion
- best score
- fastest clear
- total clears
- total C++ minigame XP
- total crystals earned
- last score, time, and profile
- bounded processed-attempt history

The same attempt ID cannot award twice. Successful clears request an immediate
leaderboard sync, while local rewards remain valid if cloud sync is unavailable.
