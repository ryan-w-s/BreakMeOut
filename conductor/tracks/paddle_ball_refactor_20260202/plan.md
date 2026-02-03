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

## Phase 2: Ball "Held" State & Launch Logic
Implement the attachment logic and the user-triggered launch mechanism.

- [x] Task: Refactor `Ball.cs` state management. d2b5ce2
    - [x] Add `IsHeld` boolean or a State Enum.
    - [x] In `_Process`, if Held, update position to match the paddle + offset.
- [x] Task: Implement Launch mechanism. ce4149b
    - [x] Check for `ui_accept` (Space) or `mouse_left_click` in `Ball.cs`.
    - [x] On launch: set state to Active, apply upward impulse, and add horizontal bias from Paddle velocity.
- [ ] Task: Update `Main.cs` spawn logic.
    - [ ] Ensure new balls are initialized in the "Held" state and linked to the Paddle instance.
- [ ] Task: Test Launch logic.
    - [ ] Write unit tests for state transitions and initial velocity calculation.
- [ ] Task: Conductor - User Manual Verification 'Phase 2: Ball "Held" State & Launch Logic' (Protocol in workflow.md)

## Phase 3: Dynamic Physics Influence
Apply the paddle's movement to the ball's bounce trajectory during active gameplay.

- [ ] Task: Modify Ball collision logic.
    - [ ] Detect when colliding with the Paddle.
    - [ ] Apply a horizontal bias to the reflected velocity vector based on the Paddle's current velocity.
- [ ] Task: Refine "Slight change in angle" math.
    - [ ] Ensure the bias is balanced (not too extreme) to keep gameplay fair.
- [ ] Task: Verify dynamic physics.
    - [ ] Manual verification: Hit the ball while moving the paddle quickly and observe trajectory changes.
- [ ] Task: Conductor - User Manual Verification 'Phase 3: Dynamic Physics Influence' (Protocol in workflow.md)
