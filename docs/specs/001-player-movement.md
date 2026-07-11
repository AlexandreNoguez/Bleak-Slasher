# 001 — Player Movement

## Goal

Allow the player to move horizontally in a 2D platformer scene.

## Rules

- Use `Rigidbody2D`.
- Use the New Input System.
- Movement speed must be configurable.
- Movement logic must not control animation directly.
- Movement logic must not depend on UI.
- Do not use Singletons.
- Do not use `FindObjectOfType` or `GameObject.Find`.

## Acceptance criteria

- Pressing right moves the player right.
- Pressing left moves the player left.
- Releasing movement input stops the player horizontally.
- Speed can be configured through a `PlayerMovementConfig` asset assigned in the Inspector.
- The player does not pass through solid colliders when its `Rigidbody2D` and colliders are configured correctly.

