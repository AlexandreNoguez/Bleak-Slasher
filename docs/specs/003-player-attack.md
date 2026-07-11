# 003 — Player Attack

## Goal

Allow the player to perform a basic melee attack.

## Rules

- Attack is triggered by input.
- Attack must have cooldown.
- Attack detects enemies in front of the player.
- Damage must be configurable.
- Combat must not depend on a specific enemy class.
- Do not put combat logic inside the movement script.

## Acceptance Criteria

- Pressing Attack performs an attack.
- Enemies inside the attack area receive damage.
- Enemies outside the attack area do not receive damage.
- Player cannot attack infinitely without cooldown.

## Unity Setup Notes

- Attach combat behavior to `PF_Player`.
- Assign enemy detection layers using `LayerMask`.
- Configure attack origin, range, size, damage, and cooldown.
- Validate with at least one basic enemy or test damage receiver in `SCN_Sandbox_Player`.
