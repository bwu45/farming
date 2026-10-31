# 2D Farming Game

A top-down 2D farming game built with Unity 6 and C#.

> **Status:** Active development. Plowing and planting are complete. Watering, crop growth, harvesting, and selling are in progress. Map redesign coming soon. 

---

## Overview

A farming simulation in which players till land, plant crops, and tend them over in-game days. The project emphasizes maintainable architecture: game rules are implemented as engine-independent C# and covered by unit tests, while Unity components handle presentation and input.

## Learning Goals

This is my first Unity project. I’m using it to apply what I’m learning in my software engineering coursework and to practice game development the way it’s done on professional teams, not just to make something that runs.

- **Gameplay programming** in C# and Unity, from movement and animation to interconnected systems like inventory, farming, and time
- **Architecture** that keeps game rules independent of the engine, so they stay easy to test and change
- **Test-driven development**, writing unit tests before the code they cover
- **Professional workflow**: issue tracking, feature branches, pull requests, and clear commit history

Each system started from a tutorial concept or from scratch, and I've documented where the tutorial ends and my own work begins in [Project History](#project-history).

## Features

- Four-directional movement with walk, idle, and tool animations
- 21-slot inventory with an integrated 7-slot toolbar (number keys or scroll wheel to select)
- Drag-and-drop item management, including dropping items into the world
- Collectible, stackable item pickups
- Tool-based tile interaction: plowing with the hoe
- Data-driven crops (Wheat, Tomato) planted on tilled soil, one per tile
- Day cycle with an on-screen counter

## Controls

| Input | Action |
|---|---|
| `WASD` / Arrow keys | Move |
| `1`–`7` / Scroll wheel | Select toolbar slot |
| `Space` / `F` | Use selected item |
| `Q` | Drop one of the selected item |
| `Tab` | Toggle inventory |
| `N` | Advance day *(debug)* |

## Architecture

### Separation of logic and presentation

Core gameplay rules live in a dedicated assembly, `Farming.Logic`, configured with **No Engine References**. This enforces at compile time that domain logic has no dependency on Unity, allowing it to be tested quickly and in isolation.

| Domain (`Farming.Logic`) | Presentation (Unity) |
|---|---|
| `Crop` — growth state, watering, harvest readiness | `CropView` — renders the current growth stage |
| `Farm` — crop placement, one crop per tile | `CropManager` — tile validation, spawning, view tracking |
| `DayCycle` — day progression and change events | `DayManager`, `DayUI` — input and display |
| `Wallet` — currency | — |

Unity components act as adapters between the engine and the domain layer (for example, converting tilemap cells to plain coordinates) and do not implement game rules directly.

### Data-driven content

Crops are defined as `CropData` ScriptableObjects specifying growth sprites, growth duration, seed and harvest items, and sell price. New crops are added through assets alone, with no code changes.

### Event-driven UI

UI updates are driven by C# events (`Inventory.OnChanged`, `Player.OnSelectedChanged`, `DayCycle.OnDayAdvanced`) rather than per-frame polling. Subscribers detach on destruction to prevent stale references.

### Inventory design

The inventory separates data (`Inventory`) from presentation (`InventoryUI`, `SlotUI`, `ToolbarUI`). The toolbar is a view over the first seven inventory slots, so a single swap operation handles all transfers between the toolbar and backpack without synchronization logic. Drag and drop is implemented with Unity's pointer and drag interfaces on runtime-instantiated slots.

## Testing

Domain logic is covered by NUnit edit-mode tests located in `Assets/Tests`. Planting rules were developed test-first, including edge cases such as negative tile coordinates used by the game's map.

To run the suite: **Window → General → Test Runner → EditMode → Run All**.

## Getting Started

### Prerequisites

- Unity **6000.6.4f1**
- [Git LFS](https://git-lfs.com) (binary assets are stored with LFS)

### Setup

```sh
git lfs install
git clone https://github.com/bwu45/farming.git
```

Open the project in Unity Hub, load `Assets/Scenes/SampleScene`, and press **Play**.

## Project Structure

```
Assets/
├── Data/Crops/            CropData assets
├── Items/                 ItemData assets
├── Prefabs/               Item pickups, crop, and inventory slot prefabs
├── Scenes/                Game scenes
├── Scripts/               MonoBehaviours: player, managers, UI, views
│   ├── Logic/             Farming.Logic assembly (engine-independent rules)
│   └── ScriptableObjects/ ItemData and CropData definitions
└── Tests/                 Edit-mode unit tests
```

## Development Workflow

- Work is tracked as GitHub issues on the project board.
- Each feature is developed on a branch named after its issue (e.g., `feature/7-plant-seeds`).
- Changes reach `main` only through pull requests describing the change and how to verify it.
- All tests must pass before merging.

## Roadmap

- [x] Tile plowing
- [x] Crop planting
- [ ] Watering and daily growth
- [ ] Harvesting
- [ ] Selling and economy
- [ ] Crop quality system
- [ ] WebGL build on itch.io
- [ ] Map redesign

## Project History

The initial foundation (project setup, movement, the first map, and collectables) was built following this youtube tutorial series: [JacquelynneHei's Unity farming RPG tutorial series](https://youtube.com/playlist?list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&si=fVRpRY529CVxI8Qg). The end of that phase is marked by the [`tutorialish`](../../tree/tutorialish) tag.

All subsequent systems were designed and implemented independently, including:

- The inventory and toolbar architecture, with tutorial concepts adapted to it
- Drag and drop, including a fix for a double-drop defect in which a single drag both moved an item to the toolbar and spawned a copy in the world
- Tool-gated plowing driven by the selected item's data
- Four-directional tool and idle animations using 2D blend trees
- The day cycle, crop growth model, farm placement rules, and their unit tests
- Data-driven crop definitions and item pickups
- Dropping the held item

## Credits

- **Art:** [Sprout Lands](https://cupnooble.itch.io/) (Basic pack) by Cup Nooble. Used for non-commercial purposes with attribution, under the creator's license. The asset pack itself may not be redistributed or resold.
- **Tutorial foundation:** [JacquelynneHei](https://youtube.com/playlist?list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&si=fVRpRY529CVxI8Qg)

## Author

**Bryant Wu** · Computer Science, University of California, Riverside · [LinkedIn](https://www.linkedin.com/in/bryantw1/) · Really cool guy · midterms are killing me 😭😭
