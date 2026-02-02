# Specification: Fix Level Select Screen (Attempt 2)

## Overview
The user reports that the Level Select screen is still non-functional (empty grid, broken back button) despite previous fixes. This suggests that the previous changes were either not applied correctly, not saved, or the root cause was misidentified.

## Functional Requirements

### 1. Robust Level Discovery
- The game must reliably find level files in `res://levels/` regardless of export mode or editor mode.
- **Hypothesis:** `DirAccess` might be failing silently or returning nothing.
- **Action:** Add aggressive debug logging to `ProgressionService` and `LevelSelect`. Verify `res://levels/` path string.

### 2. Functional Navigation
- The Back button MUST return to the Main Menu.
- **Hypothesis:** The signal connection in `.tscn` might be malformed or lost.
- **Action:** Verify `LevelSelect.tscn` text content manually. Re-create the connection in code (`_Ready`) if the scene file is unreliable.

## Acceptance Criteria
- [ ] `LevelSelect.cs` prints "Found X levels" to the console.
- [ ] The Back button prints "Back pressed" and changes the scene.
- [ ] Visible buttons appear in the Level Select grid.
