# Implementation Plan: Level Selection and Progression

## Phase 1: Persistence & State Management [checkpoint: 6f34972]
Implement the core logic for tracking level progress, unlocking levels, and saving/loading data.

- [x] Task: Update `GameManager` with level state and logic.
    - [x] Add `CurrentLevelPath` and `UnlockedLevels` (List of strings) properties.
    - [x] Add `UnlockLevel(string path)` method.
- [x] Task: Implement TDD for Progression Persistence.
    - [x] Write tests in `ProgressionManagerTests.cs` (new) for saving and loading unlocked levels.
    - [x] Implement `ProgressionService` utility to handle `user://savegame.json` I/O.
- [x] Task: Integrate Persistence into `GameManager`.
    - [x] Call `LoadProgress` on `GameManager._Ready`.
    - [x] Call `SaveProgress` when a level is unlocked.
- [x] Task: Conductor - User Manual Verification 'Phase 1: Persistence & State Management' (Protocol in workflow.md)

## Phase 2: Modular Level Loading
Update the game scenes to load specific levels based on the `GameManager` state.

- [x] Task: Update `GridGenerator` to be modular.
    - [x] Modify `GenerateGrid` to prioritize `GameManager.CurrentLevelPath` if set.
- [x] Task: Refactor `Main.cs` for clean initialization.
    - [x] Ensure `GameManager.ResetGame()` is called appropriately.
- [~] Task: Verify Level Loading with tests.
    - [x] Add integration test (if feasible) or unit test in `GridGenerator` logic to verify path prioritization. (Manual verification planned)
- [~] Task: Conductor - User Manual Verification 'Phase 2: Modular Level Loading' (Protocol in workflow.md)

## Phase 3: Level Selection UI
Create the visual interface for players to select levels.

- [ ] Task: Create `LevelSelect.tscn` and `LevelSelect.cs`.
    - [ ] Implement directory scanning for `res://levels/*.json`.
    - [ ] Generate UI buttons dynamically for each level found.
    - [ ] Implement locked/unlocked visual states for buttons.
- [ ] Task: Integrate Level Select into Main Menu.
    - [ ] Add "Select Level" button to `MainMenu.tscn`.
    - [ ] Connect signal to switch to `LevelSelect.tscn`.
- [ ] Task: Implement Navigation Logic.
    - [ ] Button click sets `GameManager.CurrentLevelPath` and switches to `Main.tscn`.
- [ ] Task: Conductor - User Manual Verification 'Phase 3: Level Selection UI' (Protocol in workflow.md)

## Phase 4: Win State & Progression Flow
Close the loop by unlocking the next level when the player wins.

- [ ] Task: Implement Unlock Logic on Win.
    - [ ] Update `GameManager.BrickDestroyed` or `OnGameOver` to identify the next level and unlock it.
- [ ] Task: Update `GameOver` UI.
    - [ ] Add "Next Level" button that only appears on victory if a next level exists.
    - [ ] Add "Level Select" button to return to the selection screen.
- [ ] Task: Verify end-to-end flow.
    - [ ] Manual verification of clearing Level 1 -> Unlocking Level 2 -> Level 2 appearing as unlocked in UI.
- [ ] Task: Conductor - User Manual Verification 'Phase 4: Win State & Progression Flow' (Protocol in workflow.md)
