# Specification: Fix Level Select Screen

## Overview
The Level Select screen is currently non-functional. No levels are displayed in the grid, and the "Back" button does not navigate back to the Main Menu. This track will diagnose and fix these issues.

## Functional Requirements

### 1. Level Population
- The `LevelSelect` script must correctly identify `.json` files in `res://levels/`.
- Buttons must be instantiated and added to the `GridContainer` for each valid level file.
- **Root Cause Hypothesis:** The path resolution in `ProgressionService.GetLevelFiles` or `LevelSelect.PopulateLevels` might be incorrect for the exported/running project environment, or the `GridContainer` reference is null.

### 2. Navigation
- The "Back" button must successfully change the scene to `res://scenes/ui/MainMenu.tscn`.
- **Root Cause Hypothesis:** The signal connection might be missing in the `.tscn` file or the method name string is mismatched.

## Acceptance Criteria
- [ ] Launching the game and clicking "Select Level" shows a grid of buttons (at least Level 1 and Level 2).
- [ ] Clicking the "Back" button returns to the Main Menu.
- [ ] No errors are printed to the console when opening the Level Select screen.
