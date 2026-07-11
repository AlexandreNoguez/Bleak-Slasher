# 004 — Health System

## Goal

Create a reusable health system for player and enemies.

## Rules

- Max health must be configurable.
- Current health cannot go below zero.
- Current health cannot exceed max health.
- The component must emit an event when damaged.
- The component must emit an event when healed.
- The component must emit an event when dead.
- UI must not be required for the health component to work.

## Acceptance Criteria

- Taking damage reduces current health.
- Healing increases current health up to max health.
- Reaching zero health triggers death event.
- Player and enemy can reuse the same health component.
- Edit Mode tests validate the core health behavior.

## Unity Setup Notes

- Attach `HealthComponent` to `PF_Player` and `PF_Enemy_Basic`.
- Use separate damage receiver components so combat does not depend on concrete enemy/player classes.
- HUD should observe health events instead of owning health logic.
