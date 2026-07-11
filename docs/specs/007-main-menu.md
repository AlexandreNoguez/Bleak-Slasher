# 007 — Main Menu

## Goal

Create a functional main menu.

## Rules

- Menu must have Play button.
- Menu must have Quit button.
- Use the retro pixel font.
- Use Fantasy Wooden GUI elements.
- Play button loads `SCN_Level_01`.

## Acceptance Criteria

- Clicking Play loads `SCN_Level_01`.
- Clicking Quit exits the game in a build.
- Menu works with mouse.
- Menu is visually consistent with the fantasy pixel art style.

## Unity Setup Notes

- Build `PF_MainMenu` manually in Unity using Canvas, Button, Image, and TextMeshPro.
- Button handlers should call menu controller methods, not gameplay logic.
- Validate in `SCN_MainMenu`.
