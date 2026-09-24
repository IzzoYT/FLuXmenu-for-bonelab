# BONELAB Game Bindings

The repository deliberately does not hard-code guessed SLZ private offsets or version-specific internals.

Implement one concrete `IGameRuntime` for the BONELAB patch/mod-loader environment you actually test. Typical responsibilities:

- obtain the left/right controller transforms
- map a deliberate input chord/button to menu open/release/back
- project hand/controller motion into a stable 2D radial selection vector
- issue controller haptics
- create/update the world-space radial renderer
- classify raycast targets (`weapon`, `npc`, `prop`, `vehicle`, etc.)
- call supported public/game APIs for healing, respawn, level reload and time scale
- populate optional spawn/inventory/camera integrations

Put fragile reflection or Harmony hooks in the adapter/integration project, not in `FLuXmenu.SDK`.
