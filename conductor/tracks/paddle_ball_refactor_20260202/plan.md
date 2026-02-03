# Implementation Plan: Paddle & Ball Refactor

## Phase 1: Paddle Input & Velocity tracking [checkpoint: d163555]
Refactor the paddle to support dual input methods and expose velocity data for ball interaction.

- [x] Task: Update Paddle movement logic. b1e1836
    - [x] Add Mouse support to `Paddle.cs`: follow mouse X-position.
    - [x] Maintain Arrow Key support as an additive offset or override.
    - [x] Clamp position within screen bounds.
- [x] Task: Implement Velocity tracking in `Paddle`. 4625034
    - [x] Calculate horizontal velocity (`X` delta per frame).
    - [x] Expose `GetCurrentVelocity()` or a property for the Ball to read.
- [x] Task: Verify Paddle movement. 4625034
    - [x] Manual verification: Ensure smooth mouse tracking and clamping.
- [x] Task: Conductor - User Manual Verification 'Phase 1: Paddle Input & Velocity tracking' (Protocol in workflow.md)

## Phase 2: Ball "Held" State & Launch Logic [checkpoint: d6b3e59]
Implement the attachment logic and the user-triggered launch mechanism.

- [x] Task: Refactor `Ball.cs` state management. d2b5ce2
    - [x] Add `IsHeld` boolean or a State Enum.
    - [x] In `_Process`, if Held, update position to match the paddle + offset.
- [x] Task: Implement Launch mechanism. ce4149b
    - [x] Check for `ui_accept` (Space) or `mouse_left_click` in `Ball.cs`.
    - [x] On launch: set state to Active, apply upward impulse, and add horizontal bias from Paddle velocity.
- [x] Task: Update `Main.cs` spawn logic. 61050fc
    - [x] Ensure new balls are initialized in the "Held" state and linked to the Paddle instance.
- [x] Task: Test Launch logic. bede65c
    - [x] Write unit tests for state transitions and initial velocity calculation.
- [x] Task: Conductor - User Manual Verification 'Phase 2: Ball "Held" State & Launch Logic' (Protocol in workflow.md)

## Phase 3: Dynamic Physics Influence [checkpoint: 9b84761]
Apply the paddle's movement to the ball's bounce trajectory during active gameplay.

- [x] Task: Modify Ball collision logic. 68c1ff2
    - [x] Detect when colliding with the Paddle.
    - [x] Apply a horizontal bias to the reflected velocity vector based on the Paddle's current velocity.
- [x] Task: Refine "Slight change in angle" math. 68c1ff2
    - [x] Ensure the bias is balanced (not too extreme) to keep gameplay fair.
- [x] Task: Verify dynamic physics. 68c1ff2
    - [x] Manual verification: Hit the ball while moving the paddle quickly and observe trajectory changes.
- [x] Task: Conductor - User Manual Verification 'Phase 3: Dynamic Physics Influence' (Protocol in workflow.md)
