# Specification - Brick Layout & Serialization

## Overview
This track focuses on two core improvements:
1.  **High-Density Brick Layout:** Refining the brick placement logic to achieve a packed grid with 5px spacing, optimized for a 1280x720 resolution.
2.  **Level Serialization System:** Creating a data structure and file format to store level layouts as 2D integer arrays (representing brick health) and refactoring the game to load these files.

## Requirements
- **Brick Spacing:** Bricks must be separated by exactly 5px.
- **Grid Layout:** Bricks should fill the available horizontal space on a 720p screen, leaving exactly one row/column gap on the top and sides.
- **Data Format:** Levels stored as a 2D array of integers (e.g., in a JSON format).
- **Dynamic Loading:** The `Main` scene must be able to instantiate bricks based on a loaded level file.

## Technical Design
- **Brick Dimensions:** To be calculated based on the 1280px width, accounting for 5px gaps and side margins.
- **Serialization:** Use `System.Text.Json` or Godot's built-in JSON parsing to read/write 2D arrays.
- **Level Manager:** A new or updated component responsible for reading level files and spawning `Brick` instances.
