# Implementation Plan: Fix Level Select Screen (Attempt 2)

## Phase 1: Deep Diagnosis & Robust Fixes

- [x] Task: Verify and Fix Signal Connection.
    - [x] Read `scenes/ui/LevelSelect.tscn` source.
    - [x] Move signal connection to C# code in `_Ready` for reliability.
- [~] Task: Debug Level Loading.
    - [ ] Add explicit logging to `ProgressionService.GetLevelFiles`.
    - [ ] Ensure `res://levels/` directory access is correct.
- [ ] Task: Manual Verification.
    - [ ] Run project, check logs, verify UI behavior.
