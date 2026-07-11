# 002 — Player Jump

## Goal

Allow the player to jump on 2D platforms.

## Rules

- Use `Rigidbody2D`.
- Jump force must be configurable.
- Player can only jump while grounded.
- Ground detection must use `LayerMask`.
- Do not implement double jump unless requested.
- Coyote time is optional for a later spec.

## Acceptance Criteria

- Pressing Jump while grounded makes the player jump.
- Pressing Jump in the air does nothing.
- The player returns to the ground through gravity.
- Jump cannot be triggered infinitely.

## Unity Setup Notes

- Attach jump behavior to `PF_Player`.
- Configure a ground check transform or collider.
- Assign the `Ground` and/or `Platform` layers in the ground mask.
- Validate in `SCN_Sandbox_Player`.
