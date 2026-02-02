# Implementation Plan: Fix Level Select Screen

## Phase 1: Diagnostics & Fixes
Identify the root causes and apply fixes.

- [x] Task: Investigate `LevelSelect.cs` and `ProgressionService.cs`.
    - [x] Review directory scanning logic (`GetLevelFiles`).
    - [x] Review signal connection logic in `LevelSelect._Ready` vs `.tscn`.
- [x] Task: Fix Back Button Connection.
    - [x] Ensure the signal is connected in code or in the scene file.
- [x] Task: Fix Level Loading/Display.
    - [x] Verify `res://levels/` path access.
    - [x] Debug `GridContainer` referencing.
- [x] Task: Verify Fixes.
    - [x] Run the project and manually verify the menu works.
