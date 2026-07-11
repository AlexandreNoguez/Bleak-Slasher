# AGENTS.md

## Project Context

This is a Unity 2D pixel art fantasy platformer project developed with a spec-driven workflow.

The project uses Unity as the source of truth for scenes, prefabs, animations, physics, layers, input configuration, visual testing, and builds.

VS Code + Codex should be used for C# scripts, tests, architecture, refactors, documentation, and implementation of small well-defined specs.

The goal is to build the game through small vertical slices, avoiding large uncontrolled changes.

---

## Game Overview

### Genre

2D pixel art fantasy platformer.

### Initial Gameplay Loop

1. Player starts in a level.
2. Player moves and jumps through platforms.
3. Player attacks enemies.
4. Enemies can damage the player.
5. Player health is displayed in the HUD.
6. If the player dies, the game shows a game over flow.
7. The player can restart or return to the main menu.

### First Vertical Slice

The first playable version must include:

- controllable 2D player;
- horizontal movement;
- jump;
- basic melee attack;
- basic enemy;
- health system;
- HUD;
- main menu;
- game over screen;
- one playable level;
- Windows build.

---

## Unity Version

Current project version:

```txt
Unity 6.5
Editor build: 6000.5.3f1
```

Baseline decision:

```txt
Use Unity 6.5 for this project because that is the installed editor version currently in use.
```

If the project later moves to an LTS release, update this file, `ProjectSettings/ProjectVersion.txt`, and any onboarding docs in the same change.

---

## Render Pipeline

Use:

```txt
Universal Render Pipeline 2D
```

Recommended 2D pixel art settings:

- orthographic camera;
- consider Pixel Perfect Camera;
- sprites should use Filter Mode = Point;
- avoid compression that blurs pixel art;
- standardize Pixels Per Unit across assets where possible.

---

## External Assets

The project uses these Unity Asset Store packages:

1. Warrior Free Asset
   Used for the main player character.

2. Pixel Art Platformer - Village Props
   Used for environment, props, platforms, and village/fantasy decoration.

3. Free Retro Pixel Font - GNF
   Used for HUD, menus, buttons, and game text.

4. Fantasy Wooden GUI : Free
   Used for menus, buttons, panels, HUD frames, and fantasy-themed UI.

---

## Asset Organization Rules

Third-party assets must stay isolated.

Do not modify imported Asset Store files directly unless explicitly requested.

Use this rule:

```txt
Assets/ThirdParty/AssetStore/  -> imported external packages
Assets/_Project/               -> actual game code, prefabs, scenes, configs, and adapted assets
```

If an external sprite, prefab, font, or UI element needs to be adapted for the game, copy or reference it from the project folders instead of editing the original package directly.

---

## Recommended Folder Structure

Use this structure as the project baseline:

```txt
Assets/
  _Project/
    Art/
      Characters/
      Environment/
      UI/
      Fonts/

    Audio/
      Music/
      SFX/

    Prefabs/
      Player/
      Enemies/
      Environment/
      UI/
      Systems/

    Scenes/
      SCN_Bootstrap.unity
      SCN_MainMenu.unity
      SCN_Level_01.unity
      SCN_GameOver.unity
      SCN_Sandbox_Player.unity

    Scripts/
      Core/
        Bootstrap/
        Events/
        Extensions/
        Utilities/

      Gameplay/
        Player/
        Enemies/
        Combat/
        Health/
        Checkpoints/
        Interactions/

      Infrastructure/
        Input/
        Audio/
        Save/
        SceneLoading/

      Presentation/
        UI/
        Camera/
        Animation/

      Data/
        ScriptableObjects/

    Settings/
      Input/
      RenderPipeline/
      Physics/
      Addressables/

    Tests/
      EditMode/
      PlayMode/

  ThirdParty/
    AssetStore/
      WarriorFreeAsset/
      PixelArtPlatformerVillageProps/
      FreeRetroPixelFontGNF/
      FantasyWoodenGUIFree/
```

---

## Architecture Principles

### 1. Keep Unity integration separate from game logic

Use `MonoBehaviour` mainly as the integration layer with Unity.

Whenever possible, keep business/gameplay rules in small classes or components that are easy to test.

Good examples:

```txt
PlayerInputReader.cs
PlayerMotor.cs
PlayerCombat.cs
PlayerAnimator.cs
HealthComponent.cs
DamageDealer.cs
DamageReceiver.cs
EnemyPatrol.cs
SceneLoader.cs
```

Avoid large generic classes such as:

```txt
GameManager.cs
PlayerManager.cs
EverythingController.cs
MainController.cs
```

A manager is only acceptable if the responsibility is clear, small, and justified.

---

### 2. Prefer component composition

Each class should do one thing.

Examples:

```txt
PlayerInputReader   -> reads input and exposes intent
PlayerMotor         -> applies movement to Rigidbody2D
PlayerCombat        -> handles attack requests and hit detection
PlayerAnimator      -> updates Animator parameters
HealthComponent     -> controls current/max health and health events
DamageDealer        -> defines outgoing damage
DamageReceiver      -> receives and forwards damage to health
EnemyPatrol         -> controls basic enemy patrol behavior
```

---

### 3. Avoid Singletons at the beginning

Do not use Singletons unless explicitly requested.

Prefer:

- Inspector references;
- ScriptableObjects;
- events;
- serialized dependencies;
- composition through prefabs;
- scene-level system objects.

If a Singleton is proposed, explain why it is necessary before implementing it.

---

### 4. Avoid hard scene lookups

Do not use:

```csharp
FindObjectOfType
GameObject.Find
Camera.main in hot paths
```

Do not rely on hardcoded scene object names.

Use serialized fields, dependency assignment in the Inspector, or explicit setup methods.

---

### 5. Use ScriptableObjects for configuration

Gameplay numbers should not be hardcoded.

Use ScriptableObjects for:

```txt
PlayerConfig
EnemyConfig
WeaponConfig
LevelConfig
AudioConfig
```

Examples of configurable values:

- movement speed;
- jump force;
- max health;
- attack damage;
- attack cooldown;
- enemy patrol speed;
- knockback force;
- invulnerability time.

---

### 6. Keep code testable

Pure rules should be testable outside Play Mode when possible.

Good candidates for Edit Mode tests:

- health calculation;
- damage calculation;
- cooldown rules;
- invulnerability rules;
- state transitions;
- checkpoint rules;
- score or currency logic.

---

## Namespace Rules

Use the namespace root:

```csharp
namespace PixelFantasy
{
}
```

Recommended namespaces:

```txt
PixelFantasy.Core
PixelFantasy.Core.Events
PixelFantasy.Gameplay.Player
PixelFantasy.Gameplay.Enemies
PixelFantasy.Gameplay.Combat
PixelFantasy.Gameplay.Health
PixelFantasy.Infrastructure.Input
PixelFantasy.Infrastructure.Audio
PixelFantasy.Infrastructure.SceneLoading
PixelFantasy.Presentation.UI
PixelFantasy.Presentation.Camera
PixelFantasy.Presentation.Animation
PixelFantasy.Data.ScriptableObjects
```

---

## Assembly Definition Guidelines

Use `.asmdef` files to reduce compile time and keep boundaries clear.

Recommended assemblies:

```txt
PixelFantasy.Core.asmdef
PixelFantasy.Gameplay.asmdef
PixelFantasy.Infrastructure.asmdef
PixelFantasy.Presentation.asmdef
PixelFantasy.Tests.EditMode.asmdef
PixelFantasy.Tests.PlayMode.asmdef
```

Suggested dependency direction:

```txt
Presentation -> Gameplay -> Core
Infrastructure -> Core
Gameplay -> Core
Tests -> Core / Gameplay
```

Avoid circular dependencies.

---

## Naming Conventions

### Scripts

```txt
PlayerMotor.cs
PlayerInputReader.cs
PlayerCombat.cs
PlayerAnimator.cs
HealthComponent.cs
DamageDealer.cs
DamageReceiver.cs
EnemyPatrol.cs
SceneLoader.cs
```

### Prefabs

```txt
PF_Player
PF_Enemy_Basic
PF_HUD
PF_GameSystems
PF_MainMenu
PF_GameOver
```

### Scenes

```txt
SCN_Bootstrap
SCN_MainMenu
SCN_Level_01
SCN_GameOver
SCN_Sandbox_Player
```

### ScriptableObjects

```txt
SO_PlayerConfig
SO_EnemyBasicConfig
SO_SwordConfig
SO_Level01Config
```

### Animations

```txt
ANIM_Player_Idle
ANIM_Player_Run
ANIM_Player_Jump
ANIM_Player_Fall
ANIM_Player_Attack
ANIM_Player_Hurt
ANIM_Player_Death
```

### Animator Controllers

```txt
AC_Player
AC_Enemy_Basic
```

---

## Scenes

### SCN_Bootstrap

Responsible for initializing global systems.

Expected objects:

- EventSystem;
- scene loading system;
- optional audio system;
- optional persistent game systems.

### SCN_MainMenu

Expected content:

- title;
- play button;
- options button, optional;
- quit button;
- retro pixel font;
- fantasy wooden UI elements.

### SCN_Level_01

Expected content:

- player;
- camera;
- ground/platforms;
- village props;
- enemies;
- spawn point;
- optional checkpoint;
- HUD;
- level boundaries.

### SCN_GameOver

Can be a separate scene or UI panel.

Expected content:

- game over message;
- retry button;
- return to menu button.

### SCN_Sandbox_Player

Development-only scene for quick gameplay testing.

Expected content:

- player;
- ground;
- camera;
- simple platform layout;
- optional enemy;
- optional HUD.

Use this scene to test movement, jump, attack, collisions, and camera behavior before using the real level.

---

## Prefab Expectations

### PF_Player

Expected components:

```txt
Rigidbody2D
Collider2D
SpriteRenderer
Animator
PlayerInputReader
PlayerMotor
PlayerCombat
PlayerAnimator
HealthComponent
DamageReceiver
```

Recommended Rigidbody2D settings:

```txt
Body Type: Dynamic
Gravity Scale: 3 to 5
Collision Detection: Continuous if needed
Interpolate: Interpolate
Freeze Rotation Z: true
```

### PF_Enemy_Basic

Expected components:

```txt
Rigidbody2D
Collider2D
SpriteRenderer
Animator
EnemyPatrol
EnemyDamageDealer
HealthComponent
DamageReceiver
```

### PF_HUD

Expected components:

```txt
Canvas
HealthView
PauseButton or PauseView if needed
```

### PF_GameSystems

Expected components:

```txt
SceneLoader
GameStateController, if justified
AudioController, if implemented
```

---

## Input System

Use Unity's New Input System.

Initial actions:

```txt
Player
  Move
  Jump
  Attack
  Pause

UI
  Submit
  Cancel
```

Suggested bindings:

```txt
Move    -> A/D, Left/Right arrows, Gamepad Left Stick
Jump    -> Space, Gamepad South Button
Attack  -> J, Mouse Left, Gamepad West Button
Pause   -> Escape, Gamepad Start
Submit  -> Enter, Gamepad South Button
Cancel  -> Escape, Gamepad East Button
```

Rules:

- Gameplay scripts should not directly read keyboard, mouse, or gamepad APIs.
- Input should go through an abstraction layer, such as `PlayerInputReader`.
- Movement logic should receive direction/intention, not raw key codes.

---

## Layers and Tags

### Layers

```txt
Player
Enemy
Ground
Platform
DamageZone
Interactable
Collectable
```

### Tags

```txt
Player
Enemy
GameController
Checkpoint
```

Rules:

- Prefer LayerMask for physics and detection.
- Avoid critical gameplay logic based only on Tags.
- Configure Physics 2D collision matrix intentionally.

---

## Animation Rules

Do not control Animator parameters directly from unrelated gameplay scripts.

Use dedicated animation adapters:

```txt
PlayerAnimator.cs
EnemyAnimator.cs
```

Suggested Player Animator parameters:

```txt
isMoving
isGrounded
verticalVelocity
isAttacking
isDead
```

Movement scripts should expose state. Animation scripts should translate state into Animator parameters.

---

## UI Rules

Use Unity UI + TextMeshPro for the first version.

Rules:

- Keep UI logic separate from gameplay rules.
- HUD observes health/state events.
- HUD does not own health logic.
- Menu buttons call UI controllers or scene loading methods.
- Avoid gameplay logic inside button click handlers.

Recommended UI structure:

```txt
Presentation/UI/
  MainMenu/
    MainMenuView.cs
    MainMenuController.cs

  HUD/
    HealthView.cs
    HUDController.cs

  GameOver/
    GameOverView.cs
    GameOverController.cs
```

---

## Audio Rules

Audio is optional in the first version.

When implemented, avoid playing audio directly from many gameplay scripts.

Prefer:

```txt
AudioController
MusicPlayer
SfxPlayer
AudioEvent
```

Gameplay scripts can request audio events, but should not own global audio behavior.

---

## Save System

Do not implement save system in the first vertical slice unless explicitly requested.

Future structure:

```txt
SaveData
SaveService
ISaveRepository
JsonSaveRepository
```

Possible future saved data:

- current level;
- checkpoint;
- player health;
- collectibles;
- audio settings;
- progress state.

---

## Testing Strategy

Use three levels of testing.

### 1. Manual Play Mode Testing

Primary testing method during early development.

Use:

```txt
SCN_Sandbox_Player
```

Validate:

- movement;
- jump;
- attack;
- collision;
- enemy damage;
- HUD;
- camera follow;
- game over behavior.

### 2. Edit Mode Tests

Use for pure logic and fast validation.

Good candidates:

```txt
Health
Damage
Cooldown
Invulnerability
State transitions
Checkpoint state
```

### 3. Play Mode Tests

Use for runtime/scene behavior.

Good candidates:

```txt
Player spawns correctly
Player moves under simulated input
Enemy damages player
HUD updates after damage
Scene loading works
Game over flow appears
```

Keep Play Mode tests focused because they are slower and more fragile.

---

## Codex Working Rules

When implementing a task, follow this process:

1. Read this `AGENTS.md`.
2. Read the relevant spec file in `docs/specs`.
3. Implement only the requested spec.
4. Keep changes small and reviewable.
5. Do not edit scenes, prefabs, third-party assets, or ProjectSettings unless explicitly requested.
6. Prefer creating scripts and explaining the required Unity Inspector setup.
7. Add tests when the feature contains pure logic.
8. After implementation, summarize:
   - files created or changed;
   - where each file should be placed;
   - which Unity components should be attached;
   - which Inspector fields should be configured;
   - how to test in Play Mode.

---

## Files Codex May Edit Freely

Codex may edit:

```txt
Assets/_Project/Scripts/
Assets/_Project/Tests/
docs/
AGENTS.md
*.asmdef
```

Codex should be careful with:

```txt
Packages/manifest.json
ProjectSettings/
Assets/_Project/Settings/
```

Codex should not edit unless explicitly requested:

```txt
Assets/_Project/Scenes/*.unity
Assets/_Project/Prefabs/*.prefab
Assets/_Project/**/*.asset
Assets/ThirdParty/
Library/
Temp/
Obj/
Build/
Builds/
Logs/
```

For prefabs, scenes, animations, input actions, and serialized assets, prefer explaining the Unity Editor steps instead of editing YAML files directly.

---

## Spec-Driven Workflow

Each feature must be implemented from a small spec.

Recommended flow:

```txt
1. Write spec
2. Validate acceptance criteria
3. Create branch
4. Implement the smallest working version
5. Test in Unity Play Mode
6. Add or update tests
7. Refactor
8. Save prefab/config changes manually in Unity
9. Commit
10. Update documentation
```

Branch examples:

```txt
feature/001-player-movement
feature/002-player-jump
feature/003-player-attack
feature/004-health-system
```

Commit examples:

```txt
feat(player): add horizontal movement
feat(player): add jump behavior
feat(combat): add melee attack detection
feat(health): add reusable health component
```

---

## Definition of Done

A feature is complete only when:

- it satisfies the spec acceptance criteria;
- it does not break existing features;
- it has been tested in Unity Play Mode;
- the Unity Console has no errors;
- new scripts use the correct namespace;
- responsibilities are separated;
- magic numbers are moved to config when appropriate;
- prefabs and Inspector setup are documented;
- tests are added for pure logic when applicable;
- documentation is updated if behavior changed.

---

## Initial Specs

Create these files under:

```txt
docs/specs/
```

### 001-player-movement.md

Goal:

Allow the player to move horizontally in a 2D platformer scene.

Rules:

- Use Rigidbody2D.
- Use the New Input System.
- Movement speed must be configurable.
- Movement logic must not control animation directly.
- Movement logic must not depend on UI.
- Do not use Singletons.
- Do not use FindObjectOfType or GameObject.Find.

Acceptance criteria:

- Pressing right moves the player right.
- Pressing left moves the player left.
- Releasing movement input stops the player.
- Speed can be configured from the Inspector.
- The player does not pass through solid colliders.

---

### 002-player-jump.md

Goal:

Allow the player to jump on 2D platforms.

Rules:

- Use Rigidbody2D.
- Jump force must be configurable.
- Player can only jump while grounded.
- Ground detection must use LayerMask.
- Do not implement double jump unless requested.
- Coyote time is optional for a later spec.

Acceptance criteria:

- Pressing Jump while grounded makes the player jump.
- Pressing Jump in the air does nothing.
- The player returns to the ground through gravity.
- Jump cannot be triggered infinitely.

---

### 003-player-attack.md

Goal:

Allow the player to perform a basic melee attack.

Rules:

- Attack is triggered by input.
- Attack must have cooldown.
- Attack detects enemies in front of the player.
- Damage must be configurable.
- Combat must not depend on a specific enemy class.
- Do not put combat logic inside the movement script.

Acceptance criteria:

- Pressing Attack performs an attack.
- Enemies inside the attack area receive damage.
- Enemies outside the attack area do not receive damage.
- Player cannot attack infinitely without cooldown.

---

### 004-health-system.md

Goal:

Create a reusable health system for player and enemies.

Rules:

- Max health must be configurable.
- Current health cannot go below zero.
- Current health cannot exceed max health.
- The component must emit an event when damaged.
- The component must emit an event when healed.
- The component must emit an event when dead.
- UI must not be required for the health component to work.

Acceptance criteria:

- Taking damage reduces current health.
- Healing increases current health up to max health.
- Reaching zero health triggers death event.
- Player and enemy can reuse the same health component.
- Edit Mode tests validate the core health behavior.

---

### 005-basic-enemy.md

Goal:

Create a basic enemy that patrols and damages the player.

Rules:

- Enemy moves between two patrol points.
- Enemy causes damage to player on contact or detection.
- Enemy health is configurable.
- Enemy damage is configurable.
- Enemy dies when health reaches zero.

Acceptance criteria:

- Enemy patrols between points.
- Player loses health when hit by the enemy.
- Enemy receives damage from player attacks.
- Enemy disappears or plays death behavior when killed.

---

### 006-hud.md

Goal:

Display player health on screen.

Rules:

- HUD observes the player's HealthComponent.
- HUD must not own health logic.
- Use the retro pixel font.
- Use Fantasy Wooden GUI elements when appropriate.
- HUD must be implemented as a prefab.

Acceptance criteria:

- Player health appears when the level starts.
- HUD updates when the player takes damage.
- HUD does not break when player dies.
- UI style matches the pixel fantasy theme.

---

### 007-main-menu.md

Goal:

Create a functional main menu.

Rules:

- Menu must have Play button.
- Menu must have Quit button.
- Use the retro pixel font.
- Use Fantasy Wooden GUI elements.
- Play button loads the first level.

Acceptance criteria:

- Clicking Play loads SCN_Level_01.
- Clicking Quit exits the game in a build.
- Menu works with mouse.
- Menu is visually consistent with the fantasy pixel art style.

---

## Recommended Prompt Template for Codex

Use this prompt when asking Codex to implement a feature:

```txt
You are a senior Unity/C# developer.

Read:
- AGENTS.md
- docs/specs/[SPEC_FILE].md

Context:
- Unity 6.5
- 2D pixel art fantasy platformer
- URP 2D
- New Input System
- Namespace root: PixelFantasy
- Feature-based architecture
- Avoid Singletons
- Avoid FindObjectOfType and GameObject.Find
- Use ScriptableObjects for gameplay configuration
- MonoBehaviours should be small and focused

Task:
Implement [SPEC NAME].

Rules:
- Create only the required scripts/tests.
- Do not edit Unity scenes or prefabs directly.
- Do not edit third-party assets.
- Explain any Inspector setup needed in Unity.
- Add Edit Mode tests for pure logic when appropriate.

After implementation, summarize:
- files created/changed;
- where each component should be attached;
- Inspector fields to configure;
- how to test in Unity Play Mode;
- any risks or assumptions.
```

---

## Development Workflow Recommendation

Keep Unity open while coding.

Use this cycle:

```txt
1. Open Unity Editor.
2. Open the project in VS Code.
3. Ask Codex to implement one small spec.
4. Review the code diff.
5. Let Unity compile.
6. Fix Console errors.
7. Test in SCN_Sandbox_Player.
8. Add/update tests.
9. Commit the feature.
```

Unity is responsible for validating:

- scenes;
- prefabs;
- animations;
- collisions;
- physics;
- layers;
- input actions;
- UI;
- builds.

VS Code + Codex is responsible for:

- C# scripts;
- tests;
- architecture;
- refactors;
- documentation;
- code reviews;
- spec implementation.

---

## Git and Versioning

Recommended files to version:

```txt
Assets/
Packages/
ProjectSettings/
docs/
AGENTS.md
```

Files and folders that should stay out of Git:

```txt
Library/
Temp/
Obj/
Build/
Builds/
Logs/
.vs/
.idea/
```

Use Git LFS for large binary assets when Git is configured:

```txt
*.png
*.psd
*.wav
*.mp3
*.ogg
*.fbx
*.ttf
*.otf
*.asset
```

Current repository note:

```txt
The current workspace contains an invalid/empty .git directory. Initialize or repair Git before relying on commits, branches, or LFS.
```

---

## Technical Decisions

```txt
Engine: Unity 6.5
Language: C#
Render Pipeline: Universal Render Pipeline 2D
Input: Unity New Input System
UI: Unity UI + TextMeshPro
Art Direction: 2D pixel art fantasy
Architecture: Feature-based folders + component composition
Config Data: ScriptableObjects
Tests: Unity Test Framework
Initial Platform: Windows
```

---

## Roadmap

### Milestone 1 — Project Setup

- create Unity project with Unity 6.5;
- configure URP 2D;
- import Asset Store packages;
- organize folders;
- create base scenes;
- configure Git and `.gitignore`;
- create Input Actions;
- create `SCN_Sandbox_Player`;
- create initial player prefab manually in Unity;
- create initial `docs/specs` files.

### Milestone 2 — Player

- implement horizontal movement;
- implement jump;
- connect basic player animations;
- add camera follow;
- validate collision with ground/platforms.

### Milestone 3 — Combat

- implement reusable health system;
- implement player melee attack;
- implement damage receiving;
- implement basic enemy patrol;
- implement enemy death behavior.

### Milestone 4 — UI

- implement HUD;
- implement main menu;
- implement optional pause flow;
- implement game over flow.

### Milestone 5 — Level

- build `SCN_Level_01`;
- compose village/fantasy props;
- place platforms;
- place enemies;
- add optional checkpoints;
- validate level boundaries.

### Milestone 6 — Build

- create Windows build;
- test resolution and UI scale;
- test keyboard/mouse input;
- review Unity Console errors and warnings;
- perform final vertical slice bug pass.

---

## Current Priority

Start with Milestone 1.

### Milestone 1 — Project Setup

Tasks:

- create Unity project;
- configure URP 2D;
- import Asset Store packages;
- organize folders;
- create scenes;
- configure Git;
- create Input Actions;
- create `SCN_Sandbox_Player`;
- create initial player prefab manually in Unity;
- create initial docs/specs files.

Do not start advanced combat, inventory, save system, shops, multiple levels, or complex enemy AI before the first vertical slice is playable.

---

## Important Warnings

### External asset compatibility

Some assets may have been created for older Unity versions.

Before relying on them:

```txt
Import asset
Open test scene
Check warnings/errors
Validate sprites
Validate animations
Validate materials
Validate prefabs
```

### Pixel art scale

Assets from different packs may use different Pixels Per Unit.

Validate scale in a sandbox scene before building the real level.

### UI blurriness

If pixel UI or fonts look blurry:

```txt
Use Filter Mode = Point
Disable unwanted compression
Check Canvas Scaler
Check Pixel Perfect settings
Use TextMeshPro font asset generated from the pixel font
```

### Avoid scope creep

Do not implement:

- inventory;
- shop;
- crafting;
- save system;
- multiple biomes;
- complex AI;
- procedural generation;
- dialogue system;

until the core loop is playable.
