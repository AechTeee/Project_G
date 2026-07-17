# Project G - 2D Action Platformer

A collaborative 2D side-scrolling action platformer built in Unity. This project was developed as a team-based exercise to establish clean coding practices, version control workflows (Git), and modular gameplay mechanics.

---

## 🎮 Game Overview

**Project G** puts the player in control of a Knight navigating through hazard-filled zones and battling bandit enemies. 

* **Core Gameplay Loop:** Navigate platforming obstacles ➔ Defeat enemies using melee or ranged combat ➔ Pass checkpoints ➔ Reach the portal to clear the level.
* **Key Mechanics:** Variable-height jumps, wall sliding/jumping, projectile shooting (arrows) with object pooling, and a shared health/damage system featuring invulnerability frames (i-frames).

---

## 🛠️ Tech Stack & Requirements

* **Game Engine:** Unity `2022.3.46f1` (LTS)
* **Scripting Language:** C#
* **Project Template:** Unity 2D Template
* **Render Pipeline:** Built-in Render Pipeline (Linear Color Space)
* **Target Resolution:** 1920x1080 (16:9)

---

## 📁 Repository Structure

```text
Assets/
├── Animation/       # Animator Controllers and Animation clips (Player, Bandit, Checkpoint)
├── Character/       # Prefabs for Player and Enemy Mob (Bandit)
├── Map/             # Tilemap assets, palettes, and level layouts
├── Scenes/          # MainMenu (Index 0), SampleScene (Index 1), Map2 (Index 2)
├── Script/          # All C# codebase scripts
├── Sprites/         # Sprite sheets (Knight, Bandit, projectiles, UI elements)
└── UI/              # Source interface design assets (.aseprite, .png, .gif)
```

---

## 💻 Codebase & Architecture

The game utilizes a decoupled, component-based architecture where entities share core behavior modules:

* **Player Controls:** 
  * `PlayerMovement.cs`: Handles velocity calculations, jumping mechanics, wall interactions, and animation triggers.
  * `PlayerAttack.cs`: Coordinates melee attacks (LMB) and ranged projectile pooling (RMB).
* **AI & Enemy Logic:**
  * `EnemyPatrol.cs`: Moves enemies between boundaries, handles wall/edge detection, and manages idle states.
  * `EnemyController.cs` (labeled `AnemyController`): Handles field-of-view BoxCasting, range checks, and trigger events for attacking the player.
* **Core Systems:**
  * `Health.cs`: A shared script attached to both players and enemies that manages damage state, sprite flashing, i-frames (via collision layer ignoring), and death triggers.
  * `Projectile.cs`: Moves individual arrows and registers damage callbacks to the target's `Health` component.
  * `PlayerRespawn.cs`: Respawn mechanics logic tied to checkpoints.
  * `SpikesDamage.cs` / `LevelMove.cs`: Environmental hazards and transition portals.
  * `UIManager.cs` / `PauseMenu.cs` / `MainMenu.cs`: Handles game state transitions, pause events, and Game Over overlays.

---

## ⌨️ Controls

| Action | Control Bindings |
|---|---|
| **Move Left / Right** | `A` / `D` or `Left` / `Right` Arrow keys |
| **Jump** | `Spacebar` (Hold down for higher jumps) |
| **Melee Attack** | `Left Mouse Button (LMB)` |
| **Ranged Attack** | `Right Mouse Button (RMB)` |
| **Pause / Resume** | `Escape (ESC)` |

---

## 🚀 Getting Started

### Prerequisites
* Unity Hub installed.
* Unity Editor version **`2022.3.46f1`** installed.

### Setup Instructions
1. **Clone the repository:**
   ```bash
   git clone <repository-url>
   ```
2. **Open the project:**
   * Launch Unity Hub.
   * Click **Add** ➔ **Add project from disk**.
   * Select the `Project_G` root folder.
   * Ensure the Editor version is set to `2022.3.46f1`.
3. **Run the Game:**
   * Open the project in Unity.
   * In the Project panel, go to `Assets/Scenes/` and double-click `MainMenu.unity`.
   * Press the **Play** button at the top of the Unity Editor.

---

## ⚠️ Known Issues & Technical Debt

This project was built during a collaborative learning cycle and contains several areas identified for refactoring:

1. **Disabled Checkpoints:** The checkpoint detection trigger code inside `PlayerRespawn.cs` is currently commented out. As a result, dying will immediately trigger the Game Over screen.
2. **Missing SoundManager:** `Health.cs` and `PlayerAttack.cs` call `SoundManager.instance`, but the script is not present in the codebase. Ensure you disable these audio calls or implement a `SoundManager` class to avoid `NullReferenceException` errors.
3. **Class/Method Spelling Typos:**
   * The enemy controller script is named `AnemyController` instead of `EnemyController`.
   * S傷害 methods are named `TakeDamge` instead of `TakeDamage` (used consistently but misspelled).
4. **Hardcoded Health Calculations:** `HealthBar.cs` divides the player health by a hardcoded value of `10` instead of dynamically referencing `Health.startingHealth`.
5. **Camera Transition Exception:** The `MoveToNewRoom()` method inside `CameraController.cs` throws a `NotImplementedException` when called during player respawning.
