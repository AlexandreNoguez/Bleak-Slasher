# 005 — Basic Enemy

## Goal

Create a basic enemy that patrols and damages the player.

## Rules

- Enemy moves between two patrol points.
- Enemy causes damage to player on contact or detection.
- Enemy health is configurable.
- Enemy damage is configurable.
- Enemy dies when health reaches zero.

## Acceptance Criteria

- Enemy patrols between points.
- Player loses health when hit by the enemy.
- Enemy receives damage from player attacks.
- Enemy disappears or plays death behavior when killed.

## Unity Setup Notes

- Attach enemy behavior to `PF_Enemy_Basic`.
- Configure patrol start/end references in the Inspector.
- Configure collision layers so enemy contact can damage the player.
- Validate in `SCN_Sandbox_Player` before using `SCN_Level_01`.
