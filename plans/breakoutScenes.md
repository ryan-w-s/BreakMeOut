# BreakMeOut Architecture Plan

## Folder Structure
- **Scenes/**: Contains all `.tscn` files.
- **Scripts/**: Contains all `.cs` source files.
- **Assets/**:
  - **Sprites/**: Visual assets.
  - **Audio/**: Sound effects and music.
  - **Fonts/**: UI fonts.
- **Resources/**: ScriptableObjects/Resources (e.g., Level configurations).

## Core Scenes & Nodes

### 1. Game Entities
- **Paddle.tscn** (`CharacterBody2D`)
  - *CollisionShape2D*: Rectangular.
  - *Sprite2D*: Visual representation.
  - *Script*: `Paddle.cs` (Handles input and movement).

- **Ball.tscn** (`CharacterBody2D`)
  - *CollisionShape2D*: Circular.
  - *Sprite2D*: Visual representation.
  - *Script*: `Ball.cs` (Handles velocity, bouncing logic, and collision response).
  - *Note*: `CharacterBody2D` is preferred over `RigidBody2D` for precise arcade physics control.

- **Brick.tscn** (`StaticBody2D`)
  - *CollisionShape2D*: Rectangular.
  - *Sprite2D*: Visual representation.
  - *Script*: `Brick.cs` (Handles health, points, and destruction logic).

- **Wall.tscn** (`StaticBody2D`)
  - *CollisionShape2D*: Boundary definitions.
  - *Script*: Optional, mostly for physics layers.

- **DeathZone.tscn** (`Area2D`)
  - *CollisionShape2D*: Placed at the bottom of the screen.
  - *Script*: `DeathZone.cs` (Detects when ball is lost).

### 2. Levels
- **Level_01.tscn** (extends `LevelBase.tscn` or generic `Node2D`)
  - Root: `Node2D`
  - Children: `Wall` instances, `Brick` layout container.

### 3. User Interface (UI)
- **MainMenu.tscn** (`Control`)
  - Buttons: Play, Quit.
  - Script: `MainMenu.cs`.

- **HUD.tscn** (`CanvasLayer` or `Control`)
  - Labels: Score, Lives.
  - Script: `HUD.cs`.

- **GameOverScreen.tscn** (`Control`)
  - Labels: "Game Over", Final Score.
  - Buttons: Restart, Main Menu.
  - Script: `GameOverScreen.cs`.

### 4. Main Game Loop
- **Main.tscn** (`Node`)
  - Acts as the Director/Manager.
  - Children:
    - `UI_Layer` (CanvasLayer)
    - `Level_Container` (Node2D) - Where current level is instanced.
    - `Paddle` (if persistent across levels) or instantiated per level.
  - Script: `MainGame.cs` (Manages game state, level transitions, pausing).

## Global Managers (Autoloads)
- **GameManager.cs** (`Node`)
  - Tracks global state: `Score`, `Lives`, `CurrentLevelIndex`.
  - Handles persistence (saving/loading high scores).
  - Defined in Project Settings > Autoload.
