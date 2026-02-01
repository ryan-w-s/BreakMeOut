# BreakMeOut

A Breakout-style game built with Godot 4.6 (Mono/C#).

## Project Overview

**BreakMeOut** is a game built with Godot 4.6 (Mono/C#).
The player controls a paddle to bounce a ball, which lowers brick health when hit.
Bricks spawn another ball when they break.

## Engine & Tooling

- **Engine**: Godot 4.6-stable (Mono/C#)
- **Language**: C#
- **Platform**: Windows

## Common Commands

### Running the Project
- Through Godot Editor: Press F5 or use "Play" button
- Through MCP: Use `run project` with project path

### Opening the Project
- Through MCP: Use `launch editor` with project path

### Building for Export
- Configure export presets in Godot Editor (Project > Export)
- Export through: Project > Export > Export Project

### Testing
Run tests from the project root:
```bash
dotnet test
```

Tests are located in the `tests/` directory and use standard .NET testing frameworks like NUnit.

## Architecture

This project is in initial setup. As development progresses, typical Godot patterns to follow:

- **Scene organization**: Each major game entity should be its own scene
- **Script attachment**: Scripts are attached to nodes in the scene tree
- **Main scene**: Designate a main scene as the entry point (Project Settings > Application > Run > Main Scene)

## Project Structure (Recommended)

The project root will typically contain:
- Scenes (`.tscn` files)
- C# scripts (`.cs` files)
- Assets (textures, audio, fonts in respective folders)
- `project.godot` - Main project configuration

## Godot MCP Tools Available

This project has Godot MCP server enabled, allowing agents to:
- Create and modify scenes
- Add nodes to scenes
- Load sprites/textures
- Run and debug the project
- Get project metadata

Generally, you should prefer to use the godot-mcp tools if one exists for the task you are doing.

## Notes

- The project uses Godot's unique identifier (UID) system for referencing resources
- Scene files are text-based and can be read/edited directly
- C# scripts compile automatically when the project is run
- Using "run with debug" is the preferred way to run the game because it gives you debug logs back

