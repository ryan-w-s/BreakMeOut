# Specification: Level Selection and Progression

## Overview
Implement a level selection system that allows players to choose from available levels, progress through them sequentially, and persist their progress between sessions. The game loop will be updated to handle level transitions and modular data loading.

## Functional Requirements

### 1. Level Selection UI
- Create a new `LevelSelect.tscn` scene.
- Display a grid of levels (Level 1, Level 2, etc.) based on the files found in the `res://levels/` directory.
- Use visual indicators to differentiate between **Unlocked** (clickable) and **Locked** (disabled) levels.
- Level 1 must be unlocked by default.

### 2. Level Loading Logic
- Modularize `Main.cs` and `GridGenerator.cs` to load level data from a file path rather than a hardcoded default.
- Utilize the `GameManager` (Autoload) to store the `SelectedLevelPath` during the transition from the Level Select screen to the Game scene.

### 3. Progression & Unlocking
- **Win Condition:** A level is considered "Cleared" when all breakable bricks are destroyed.
- Upon winning, the next level in the sequence (if it exists) is unlocked.
- The player should be presented with an option to return to the Level Select screen or proceed to the next level.

### 4. Data Persistence
- Implement a save/load system using a local file (e.g., `user://savegame.json`).
- Store a list of unlocked level IDs or the highest unlocked level index.
- Progress must be saved immediately upon clearing a level and loaded when the game starts.

## Non-Functional Requirements
- **Scalability:** The system should automatically detect new `.json` files in the `levels/` folder.
- **Robustness:** Handle cases where save files are corrupted or level files are missing.

## Acceptance Criteria
- [ ] Level Select screen is accessible from the Main Menu.
- [ ] Level 2 is initially locked and cannot be started.
- [ ] Clearing Level 1 unlocks Level 2.
- [ ] Closing and reopening the game keeps Level 2 unlocked.
- [ ] The Main scene correctly spawns the brick layout defined in the selected JSON file.

## Out of Scope
- Star ratings or high scores per level.
- Animated level transition cutscenes.
- Cloud saving.
