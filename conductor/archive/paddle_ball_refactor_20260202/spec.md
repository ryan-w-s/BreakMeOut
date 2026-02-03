# Specification: Paddle & Ball Refactor

## Overview
Refactor the paddle and ball mechanics to provide more control and precision. This includes mouse-based paddle movement, a "held" ball state for controlled launches, and dynamic ball angles influenced by paddle velocity.

## Functional Requirements

### 1. Paddle Movement Refactor
- **Dual Input:** Support both arrow keys (existing) and mouse movement.
- **Mouse Control:** The paddle's X-position should strictly follow the mouse cursor's X-position within the game window.
- **Clamping:** Ensure the paddle remains within screen bounds regardless of mouse position.
- **Velocity Tracking:** The paddle must calculate and expose its current horizontal velocity (direction and speed) for the ball physics.

### 2. Ball "Held" State & Launching
- **Initial Spawn:** When a level loads or a new ball is spawned after a life loss, the ball starts in a **Held** state.
- **Attachment:** In the Held state, the ball remains at a fixed vertical offset directly above the paddle's center, moving horizontally with it.
- **Launch Trigger:** Pressing `Spacebar` or `Left Mouse Click` transitions the ball from Held to **Active** (launching it).
- **Launch Direction:** The ball launches upwards. If the paddle is moving during launch, apply a horizontal bias based on the paddle's velocity.

### 3. Dynamic Ball Physics (Angle Influence)
- **Paddle Bounce:** When the ball bounces off the paddle during normal gameplay, the resulting angle should be influenced by the paddle's current horizontal movement direction.
- **Logic:**
    - Paddle moving Left: Add a slight leftward bias to the bounce angle.
    - Paddle moving Right: Add a slight rightward bias to the bounce angle.
    - Paddle Stationary: Standard reflection logic.

## Acceptance Criteria
- [ ] The ball stays attached to the paddle on level start and after losing a life.
- [ ] The ball launches only when Space or Left Click is pressed.
- [ ] The paddle perfectly tracks the mouse's horizontal position.
- [ ] Moving the paddle quickly while the ball hits it visibly alters the return trajectory.
- [ ] Arrow keys still function as a secondary control method.

## Out of Scope
- Gamepad support (unless covered by existing Godot Input Map).
- Multiple balls held simultaneously (power-ups remain as is).
- Changes to brick health or destruction logic.
