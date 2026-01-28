# Breakout Game Architecture

## Core Concept
A classic Breakout-style arcade game where the player controls a paddle to bounce a ball into a wall of destructible bricks.

## Project Structure
We will organize the project by file type to keep it clean.

```text
res://
├── assets/
│   ├── sprites/      # Textures for paddle, ball, bricks
│   ├── audio/        # SFX and music
│   └── fonts/        # UI fonts
├── scenes/
│   ├── actors/       # Dynamic entities (Paddle, Ball)
│   ├── environment/  # Static entities (Brick, Walls)
│   ├── ui/           # Menus, HUD
│   └── levels/       # Game levels
└── scripts/
    ├── actors/
    ├── managers/
    └── ui/
```

## Key Scenes & Nodes

### 1. Paddle (`scenes/actors/Paddle.tscn`)
*   **Root**: `CharacterBody2D`
*   **Children**:
    *   `Sprite2D`: Visual representation.
    *   `CollisionShape2D`: Rectangular collider.
*   **Script**: `Paddle.cs`
    *   Handles Input (Left/Right).
    *   Movement logic (clamped to screen bounds).

### 2. Ball (`scenes/actors/Ball.tscn`)
*   **Root**: `CharacterBody2D` (Preferred over RigidBody for precise arcade control)
*   **Children**:
    *   `Sprite2D`: Ball image.
    *   `CollisionShape2D`: Circular collider.
*   **Script**: `Ball.cs`
    *   `Vector2 direction`: Stores current movement vector.
    *   `float speed`: Constant movement speed.
    *   Uses `MoveAndCollide` to detect hits.
    *   On collision: Reflects direction based on normal. If hitting Paddle, can adjust angle based on hit position.

### 3. Brick (`scenes/environment/Brick.tscn`)
*   **Root**: `StaticBody2D`
*   **Groups**: `Bricks`
*   **Children**:
    *   `Sprite2D`: Brick image.
    *   `CollisionShape2D`: Rectangular collider.
*   **Script**: `Brick.cs`
    *   `int Health`: Number of hits to destroy (default 1).
    *   `Hit()` method: Called by Ball when collided. Decrements health. Dies if <= 0.
    *   Spawns powerups or secondary balls (as per user request: "Bricks spawn another ball when they break").

### 4. Main Game (`scenes/Main.tscn`)
*   **Root**: `Node`
*   **Children**:
    *   `GameManager`: (Script-only node or purely logical)
    *   `LevelContainer`: Node2D to hold the current level.
    *   `HUD`: CanvasLayer for Score/Lives.

## Game States (GameManager)
A global `GameManager.cs` (Autoload or Main node) to handle:
*   **Score**: Points for breaking bricks.
*   **Lives**: Reset ball when lost. Game Over when 0.
*   **State Machine**:
    *   `Menu`: Start screen.
    *   `Playing`: Active gameplay.
    *   `GameOver`: Win/Loss screens.

## Implementation Steps
1.  **Setup**: Create project folders.
2.  **Paddle**: Implement movement and bounds.
3.  **Ball**: Implement basic movement and bouncing off screen edges.
4.  **Collision**: Add ball-paddle interaction.
5.  **Bricks**: Create static bricks, implement destruction.
6.  **Game Loop**: Add "Ball Spawn on Brick Break" mechanic.
7.  **UI**: Add Score and Lives.
