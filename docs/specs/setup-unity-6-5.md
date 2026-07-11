# Unity 6.5 Project Setup

## Current Editor

```txt
Unity 6.5
Editor build: 6000.5.3f1
```

## Create/Open Project

1. Open Unity Hub.
2. Create a new project using a 2D URP template if available.
3. Use this workspace folder as the project location:

```txt
/home/alexandre/workspace/unity/action-medieval-2d
```

4. If Unity Hub refuses to create into a non-empty folder, create the project in a temporary folder and move/copy the generated Unity files into this workspace, keeping the existing `Assets/_Project`, `Assets/ThirdParty`, `docs`, `AGENTS.md`, `.gitignore`, and `.gitattributes`.

## Required Packages

- Universal Render Pipeline.
- Unity New Input System.
- TextMeshPro.
- Unity Test Framework.
- 2D packages required by the selected Unity template.

## First Unity Editor Tasks

1. Confirm URP 2D is active.
2. Configure sprite import defaults for pixel art:
   - Filter Mode: Point;
   - Compression: None or Low Quality;
   - consistent Pixels Per Unit per asset pack.
3. Create these scenes under `Assets/_Project/Scenes`:
   - `SCN_Bootstrap`;
   - `SCN_MainMenu`;
   - `SCN_Level_01`;
   - `SCN_GameOver`;
   - `SCN_Sandbox_Player`.
4. Create Input Actions under `Assets/_Project/Settings/Input`.
5. Configure layers:
   - `Player`;
   - `Enemy`;
   - `Ground`;
   - `Platform`;
   - `DamageZone`;
   - `Interactable`;
   - `Collectable`.
6. Import Asset Store packages into `Assets/ThirdParty/AssetStore`.

## Git Note

The current workspace contains an empty invalid `.git` directory. Repair or recreate Git before relying on branches, commits, or Git LFS.
