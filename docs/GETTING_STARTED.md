# Getting Started

## 1. Prepare references

Create a `references` directory at the repository root and add the assemblies from your own BONELAB modding environment. `MelonLoader.dll` is the first required reference for the host project. Add SLZ/Unity/BoneLib/LabFusion references only to the projects that actually use them.

## 2. Bind the game runtime

`src/FLuXmenu/Runtime/IGameRuntime.cs` is the intentionally narrow bridge between FLuXmenu and BONELAB internals.

Replace `NullGameRuntime` in `GameRuntimeFactory` with a concrete implementation for your target patch. The adapter is responsible for:

- reading the configured open/release input
- returning the 2D hand/controller selection axis
- haptics
- positioning/rendering the radial UI
- context raycasts
- built-in player/time/spawn operations

Keeping this code isolated makes SDK plugins much less likely to break when game internals change.

## 3. Implement the visual backend

Use `docs/UI_SPEC.md` as the behavior contract. The visual backend should be a Unity world-space UI or mesh-based radial attached to the selected hand/controller anchor. It should never require plugin authors to know about Unity layout internals.

## 4. Build and test

Test the interaction with 4, 6 and 8 segments, both hands, low/high controller movement, menu cancellation and nested page navigation. Test Quest separately because fill-rate and per-frame allocations matter much more there.
