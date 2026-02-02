# Implementation Plan: Fix Level Select Screen (Attempt 2)

## Phase 1: Deep Diagnosis & Robust Fixes

- [x] Task: Verify and Fix Signal Connection.
    - [x] Read `scenes/ui/LevelSelect.tscn` source.
    - [x] Move signal connection to C# code in `_Ready` for reliability.
- [x] Task: Debug Level Loading.
    - [x] Add explicit logging to `ProgressionService.GetLevelFiles`.
    - [x] Ensure `res://levels/` directory access is correct.
- [x] Task: Manual Verification.
    - [x] Run project, check logs, verify UI behavior.
