## PlayerInput + PlayerCamera: High-Level Design & Extension Guide
Pairs extremely well with [my Photon Unity Network Scaffold](https://github.com/havenfricke/PUN2_Net_Singleton).
This repository is meant for fast iteration.  
The two scripts shown here form a base interaction pattern that other gameplay scripts will follow.

---

## Generating the Required C# Input Actions Class

This system depends on a generated C# class from Unity’s Input System. This class provides the strongly-typed actions used by PlayerInput.

### How to Generate It

1. Open the Input Actions asset  
   - In the Project window, double-click your .inputactions file (for example: InputSystem_Actions.inputactions).

2. Enable C# class generation  
   - In the Inspector, check **Generate C# Class**.
   - Choose a namespace if desired.

3. Apply and save  
   - Click **Apply**.
   - Unity will generate a .cs file next to the input actions asset.

---

### The Big Picture

The system is split into two clear layers:

### 1. PlayerInput - the Input Hub
- Central place where all player input is received
- Talks directly to Unity’s Input System
- Does not contain gameplay logic
- Calls methods on other scripts when input happens

### 2. PlayerCamera - Example Feature Module
- One example of a feature script
- Receives clean data from PlayerInput
- Handles its own logic and update timing
- Knows nothing about input bindings or devices

Think of it like this:

Player Input Devices -> PlayerInput -> Feature Scripts (Camera, Movement, Combat, etc.)

---

## Input Maps and Switching (Unity 6)

Uses two independent action maps that the `PlayerInput` script can switch between at runtime.

### Player Action Map
Gameplay controls:
- Look (Vector2)
- Move (Vector2)
- Jump
- Crouch
- Sprint
- Attack
- Interact
- Previous
- Next
- **Pause** (add this to your action map)

### UI Action Map
Unity’s default UI navigation actions:
- Navigate (Vector2)
- Submit
- Cancel
- Point (Vector2)
- Click
- RightClick
- MiddleClick
- ScrollWheel (Vector2)
- TrackedDevicePosition
- TrackedDeviceOrientation
- **Resume** (add this to your action map)

Both maps must exist in the generated `InputSystem_Actions.cs` file.

---

## How Player/UI Switching Works

The `PlayerInput` script handles all input routing and toggles between action maps using two actions:

- **Pause** → switches from Player → UI  
- **Resume** → switches from UI → Player  

Switching steps:
1. Pause/Resume action fires  
2. `inputSwitchGate` is set  
3. `Update()` calls `SwitchInputType()`  
4. The correct action map is enabled  
5. The other map is disabled  
6. Cursor lock state updates  
7. `inputSwitchGate` resets  

This prevents mid-frame conflicts and ensures clean transitions.

---

### What This Class Does

- Defines action maps like Player
- Exposes actions like Look, Move, Jump, etc.
- Allows PlayerInput to subscribe to input events using code
- Keeps input strongly typed and compile-safe
- Avoids string-based lookups
- Makes input routing consistent across the project

Once generated, this class is referenced only by PlayerInput.  
Feature scripts (like PlayerCamera) never depend on it directly.

### How These Two Scripts Work Together

- PlayerInput listens for actions like Look
- When input occurs, it forwards the data
- PlayerCamera receives that data through a method call
- PlayerCamera applies camera behavior every frame

Important detail:
- PlayerCamera does not pull input
- PlayerInput pushes input into it

This keeps responsibilities clean and predictable.

---

### Why This Exists as a Repo Pattern

This setup is meant to be:
- Easy to drop into a new project
- Easy to expand without refactoring
- Easy to reason about during iteration

You can add or remove feature scripts without changing how input works overall.

---

### Extensibility: The Intended Pattern

PlayerCamera is not special.  
It is simply the first example of a pattern.

Future scripts will look similar:

- PlayerMovement
- PlayerCombat
- PlayerInteraction
- PlayerInventory
- etc.

Each of these scripts will:
- Encapsulate one responsibility
- Expose simple public methods
- Be called from PlayerInput

---

### The Rule of Communication

Only one direction is allowed:

PlayerInput -> Feature Script


Feature scripts:
- Do not reference the Input System
- Do not subscribe to input events
- Do not know where input came from

They only respond to method calls.

---

### Why This Scales Well

- Input changes do not affect gameplay code
- Gameplay code can be reused or removed easily
- Multiplayer, AI, or replay systems can call the same methods later
- Keeps scripts small and focused

This pattern also mirrors common professional setups:
- Input Layer
- Gameplay Layer
- Presentation Layer

---

### Mental Model to Keep

PlayerInput is the brain stem.  
Feature scripts are muscles.

PlayerInput decides what happens.  
Feature scripts decide how it happens.

---

### Summary

- PlayerInput is the single entry point and sealed class for player input
- PlayerCamera is an example of a modular feature
- New features follow the same structure
- Input is routed, not pulled
- This repo is a starting point, not a finished system

If you follow this pattern, the project stays flexible as it grows.
