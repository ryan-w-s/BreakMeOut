# BreakMeOut Architecture Plan

## Scene Structure

```
Main (autoload)                # Singleton for game state
├── Menu.tscn                  # Title/start screen
├── Game.tscn                  # Main gameplay scene
│   ├── HUD (CanvasLayer)      # Score, lives, level
│   ├── Paddle.tscn            # Player paddle
│   ├── Ball.tscn              # Ball (instanced)
│   ├── Brick.tscn             # Brick (instanced)
│   └── Walls                  # Boundary colliders
└── GameOver.tscn              # Win/lose screen
```

## Node Hierarchy (Game.tscn)

```
Game (Node2D)
├── Camera2D
├── HUD (CanvasLayer)
│   ├── ScoreLabel
│   ├── LivesLabel
│   └── LevelLabel
├── Walls (StaticBody2D)
│   ├── Top (CollisionShape2D)
│   ├── Left (CollisionShape2D)
│   └── Right (CollisionShape2D)
├── Paddle (Area2D)
│   ├── Sprite2D
│   ├── CollisionShape2D
│   └── Paddle.cs
├── BallContainer (Node2D)
│   └── Ball (CharacterBody2D or RigidBody2D)
│       ├── Sprite2D
│       ├── CollisionShape2D
│       └── Ball.cs
└── BrickGrid (Node2D)
    └── Brick (Area2D)
        ├── Sprite2D
        ├── CollisionShape2D
        └── Brick.cs
```

## Core Scripts

| Script | Responsibility |
|--------|---------------|
| `Main.cs` (autoload) | Game state, score/lives tracking, scene transitions |
| `Paddle.cs` | Mouse/keyboard movement, boundary clamping |
| `Ball.cs` | Velocity, bounce physics, wall collision, death zone |
| `Brick.cs` | Health, hit detection, spawn ball on death |
| `HUD.cs` | Update UI labels |

## Key Signals

```
Paddle.hit_ball(ball)           # Ball hit paddle
Brick.hit(ball)                 # Ball hit brick
Brick.destroyed()               # Brick broken (spawn ball)
Ball.died()                     # Ball fell below paddle
Game.all_bricks_cleared()       # Level complete
```

## Resources

- `Brick.tscn` - PackedScene for instancing bricks
- `Ball.tscn` - PackedScene for spawning new balls
- Colors/themes per brick health tier

## Flow

1. `Menu.tscn` → start button → `Game.tscn`
2. `Game.tscn` generates brick grid
3. Ball death → lose life → respawn ball
4. Lives = 0 → `GameOver.tscn` (lose)
5. All bricks cleared → `GameOver.tscn` (win)
6. Any state → `Menu.tscn` (restart)
