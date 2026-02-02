# Implementation Plan: Fix Level Select Screen

## Phase 1: Diagnostics & Fixes
Identify the root causes and apply fixes.

- [~] Task: Investigate `LevelSelect.cs` and `ProgressionService.cs`.
    - [ ] Review directory scanning logic (`GetLevelFiles`).
    - [ ] Review signal connection logic in `LevelSelect._Ready` vs `.tscn`.
- [x] Task: Fix Back Button Connection.
    - [x] Ensure the signal is connected in code or in the scene file.
- [~] Task: Fix Level Loading/Display.
    - [ ] Verify `res://levels/` path access.
    - [ ] Debug `GridContainer` referencing.
- [ ] Task: Verify Fixes.
    - [ ] Run the project and manually verify the menu works.
