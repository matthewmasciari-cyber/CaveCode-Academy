# CaveCode Academy — Official Branding Pass 2

This pass extends the approved CaveCode Academy logo to shared website contexts without modifying the SVG artwork.

## Updated surfaces

- Learning Paths section heading
- C#, Python, C++, and HTML/CSS course sidebars
- Signed-out account panel
- Minigames hub header
- Settings header
- Achievements header
- Leaderboard header
- Blazor loading screen

## Artwork integrity

The installer requires the exact Pass 1 logo asset at:

`wwwroot/branding/cavecode-academy-logo.svg`

Required SHA-256:

`bad04dda3921d86d211d8cfb99d0b99e2ebfecd1a2bb025011b93e6a772b4b96`

No SVG paths, typography outlines, colors, spacing, viewBox, or proportions are edited.

## Protected behavior

- Learning Path cards and actions remain byte-for-byte unchanged.
- Razor `@code` blocks remain byte-for-byte unchanged.
- Authentication logic remains unchanged.
- Course lessons, progress, XP, crystals, achievements, leaderboards, minigames, and Supabase behavior remain unchanged.
