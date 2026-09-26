FLuXmenu
A spatial utility interface for BONELAB.
FLuXmenu is an all-in-one BONELAB code mod built around a fast, immersive radial interface designed specifically for VR.
Instead of traditional flat debug menus, FLuX places its tools directly into the game world through a wrist-mounted radial, contextual AUX interface, holographic information systems, weapon inspection, Combat Replay, Fusion utilities, spatial panels, and interactive UI elements.
The goal is to make FLuX feel less like a mod menu and more like an operating system built into your BONELAB rig.
Current Development Version: FLuX 5.4.x

✦ FLuX Spatial Interface
The main FLuX interface is a monochrome radial menu that appears directly from the player's wrist.
The interface uses a custom black-and-white visual language with animated wedges, holographic effects, contextual information, particle systems, and spatial controls.
Main Radial
The main wheel provides quick access to:
- Inspector
- Quick Scan
- Combat Replay
- Outline
- Favorites
- Hologram
- Settings
- Close
FLuX supports nested radial pages without replacing the entire interface. Submenus deploy around the main wheel while keeping the CORE visible.
✦ New FLuX Visual Identity
FLuX now has its own custom FLuX MENU wordmark instead of the original plain text header.
The logo is:
- Embedded directly inside the mod
- Rendered with transparency
- Displayed using an unlit holographic material
- Integrated into the radial startup sequence
- Independent of BONELAB world lighting
- Packaged without requiring external image files
The UI remains completely focused around FLuX's black, graphite and white visual identity.
◉ FLuX CORE
At the center of the interface is the FLuX CORE.
The CORE acts as the visual heart of the system rather than simply being a menu decoration.
Different FLuX systems can change its behavior:
- Scanner activity
- Replay activity
- Pointer activity
- Notifications
- Loading
- Errors
- System state
The CORE includes animated reactor-style rings and contextual visual feedback.
FLuX is also being designed around three interface states:
Sleep → Aware → Active
The long-term goal is for the entire radial interface to physically unfold from the small FLuX standby puck and collapse back into it when closed.
✦ Dynamic AUX
FLuX includes a secondary AUX interface designed for the opposite arm.
AUX provides fast access to important actions without opening or navigating the entire main radial.
AUX Console
The redesigned AUX interface uses a compact unified control layout instead of disconnected floating buttons.
Default controls include:
- Scan
- Replay
- Favorite
- Outline
- Fusion
- Settings
AUX dynamically changes depending on what FLuX is currently doing.
Context modes include:
- Quick Control
- Inspect Control
- Replay Control
- Hologram Control
- System Control
- Fusion Link
The AUX particle halo also adapts to the expanded console instead of remaining attached to a single floating point.
✦ Weapon Inspector
FLuX can automatically recognize grabbed weapons and display holographic information beside them.
The inspector supports:
- Automatic weapon inspection
- Firearms
- Melee weapons
- Modded weapons
- Weapon information panels
- Adjustable text size
- Multiple fonts
- Panel locking
- Configurable display duration
- Fade animations
- Holographic flicker
- Scan animations
- Scan audio
- Adjustable scan volume
- Swoosh entrance animations
Re-grabbing the same weapon from another grip does not unnecessarily trigger another scan.
✦ Two-Hand Inspection
FLuX can detect when a weapon is being held with both hands.
The support-hand secondary control can toggle weapon information without requiring the player to release the weapon.
Two-Hand Inspection can be enabled or disabled independently.
When disabled, normal automatic inspection continues to function.
✦ Quick Scan
Point at something and quickly request information without navigating through several menu pages.
Quick Scan integrates with the FLuX pointer and spatial interface.
The system is designed to become the foundation for more advanced contextual object interaction.
✦ Weapon Outlines
Scanned weapons can receive a lightweight holographic outline.
FLuX uses an inverted-hull style technique rather than a giant overlay around the object.
The system supports:
- MeshRenderer objects
- SkinnedMeshRenderer objects
- Moving weapons
- Multiple weapon meshes
- Filtering of collider/proxy/bounds meshes
The outline follows the weapon instead of remaining behind in world space.
✦ Holographic Deck
Weapon information can be accompanied by a procedural holographic platform.
Features include:
- Circular holographic deck
- Animated rotation
- Downward projection cone
- Hologram flickering
- Scan effects
- Configurable behavior
- Runtime-generated geometry
No external model is required.
✦ Combat Replay
FLuX can capture the moments leading up to an NPC kill and reconstruct them as a spatial replay.
The replay system can preserve roughly the previous several seconds of combat information.
Replay Systems
- Player hologram
- NPC hologram
- Slow motion
- Hand paths
- Weapon paths
- Bullet trajectory
- Impact markers
- Weapon information
- Hit velocity
- Kill distance
- Kill statistics
- Kill pulse
- Replay intro/outro
- Multiple hologram styles
The Replay radial provides dedicated controls for:
Play Last · Bullet Path · Impact · Kill Stats · NPC Holo · Kill Pulse
✦ Opposite-Hand Laser Pointer
FLuX can be controlled using a VR laser projected from the opposite hand.
The pointer supports:
- Radial selection
- AUX selection
- Fusion lobby selection
- Adjustable length
- Adjustable thickness
- Pointer smoothing
- Configurable pointer axis
- Selection snapping
- Endpoint cursor
- Hover feedback
- Thumbstick fallback
This allows FLuX to behave more like a real spatial interface instead of requiring traditional menu navigation.
✦ Magnetic Radial
Radial wedges react dynamically to the player's selection.
When approaching an option:
- The selected wedge expands
- Neighboring elements react
- Labels become more prominent
- The pointer visually locks toward the control
- Submenus deploy from their parent system
This gives the radial a physical response instead of behaving like a static wheel.
✦ Wedge Folding
FLuX submenus are designed to feel connected to their parent control.
Instead of simply spawning another menu, secondary controls visually deploy and fold outward from the existing radial structure.
✦ Menu Inertia
Decorative interface components react to wrist movement.
Fast movement causes outer rings, particles and decorative components to slightly lag behind the primary controls.
Interactive elements remain stable enough to select while the surrounding holographic system reacts dynamically.
✦ Particle Halo
The FLuX interface contains a procedural particle halo generated entirely at runtime.
Particles can respond to:
- Menu state
- Hovering
- AUX state
- Scanner activity
- Interface transitions
No external particle textures are required.
✦ CORE Reactor
The center of FLuX contains independently animated reactor rings.
Different systems can modify reactor movement rather than simply changing its color.
Examples include:
Scanner — sweeping motion
Replay — reverse rotation
Loading — accelerating rings
Errors — unstable movement
This preserves FLuX's monochrome identity while still communicating system state.
✦ Dynamic Action
FLuX contains a contextual action control capable of changing according to the current system or interaction.
Rather than dedicating permanent buttons to every possible command, Dynamic Action provides relevant actions when they are needed.
✦ Context Lens
The Context Lens provides additional spatial information about the current FLuX context.
It is part of FLuX's move toward contextual interfaces where information appears according to what the player is interacting with rather than remaining permanently visible.
✦ Constellation Interface
FLuX includes an experimental spatial Constellation system.
Information can be represented as interconnected nodes rather than another traditional list.
The system provides the foundation for future:
- Player networks
- Plugin relationships
- Object relationships
- Context navigation
- Multiplayer visualization
✦ Memory Deck
FLuX can maintain recent interface/context information inside its Memory Deck.
This allows recent interactions to be surfaced spatially instead of disappearing immediately after an action.
✦ Spatial Panels
FLuX includes support for spawning floating system panels in front of the player.
These panels provide the foundation for future movable tools, diagnostics and spatial workspaces.
✦ Repositionable Interface
The radial isn't required to remain permanently attached to its original wrist position.
While FLuX is open, the opposite hand can be used to reposition the interface for the current session.
This makes it possible to move FLuX into a more comfortable viewing or interaction position.
✦ Fusion Integration
FLuX 5.x introduces optional integration with BONELAB Fusion.
Fusion is detected at runtime, meaning FLuX itself does not require Fusion as a hard dependency.
If Fusion is unavailable, the rest of FLuX can continue operating normally.
✦ Fusion Lobby Space
Fusion now has its own section inside AUX.
FLuX can request available Fusion lobbies and represent them as spatial lobby cards.
Lobby information can include:
- Lobby name
- Host
- Current level
- Player count
- Maximum players
- Fusion/lobby version information
Up to several discovered lobbies can be displayed directly in VR.
◉ Fusion Void Doors
One of FLuX's most experimental multiplayer features is Void Doors.
Instead of selecting a lobby from a normal flat list:
1. Open Fusion from AUX.
2. Browse available Fusion lobbies.
3. Point at a lobby card.
4. Grab the card.
5. Physically throw it into the world.
6. The card transforms into a full-size Void Door.
7. Walk through the portal to trigger Fusion's lobby join action.
Void Doors feature:
- Full-size spatial portals
- Black void surface
- White elliptical border
- Procedural particle halo
- Breathing animation
- Rotational fluctuation
- Animated border thickness
- Lobby identification
- Physical portal crossing detection
- Fusion's own lobby join system
The entire portal is generated at runtime.
✦ Fusion AUX
When AUX enters Fusion mode, its controls transform into:
Lobbies · Refresh · Status · Portals · Back · Close
This keeps multiplayer functionality inside the existing FLuX interface rather than introducing another unrelated menu.
✦ Notification HUD
FLuX contains its own lightweight notification system.
Examples include:
OUTLINE ENABLED
WEAPON FAVORITED
REPLAY SAVED
NPC SELECTED
FUSION // LOBBY SPACE
VOID DOOR // ENTERING
Notifications use the same visual language as the rest of FLuX.
✦ Startup Sequence
FLuX includes a short system initialization sequence:
FLuX CORE
INPUT
SCANNER
REPLAY
ONLINE

The boot sequence can be disabled for players who prefer immediate access.
✦ Bottom System Dock
The main interface also includes a compact lower system dock:
STORE   PLUGINS   MISC   EXTRA   SETTINGS

The dock provides dedicated space for FLuX's growing system architecture without overcrowding the primary radial.
Some dock systems are currently interface entry points for functionality planned for future releases.
✦ Subsystem Rail
Developer and system functionality can be represented through FLuX's subsystem rail.
Current interface categories include:
SDK
PLUGINS
EVENTS
COMPAT
DIAG

This forms part of the foundation for FLuX's future extension architecture.
✦ UI Performance Modes
FLuX's interface architecture includes different UI quality levels:
- Minimal
- Standard
- Cinematic
The goal is to allow heavier visual effects without forcing them onto lower-performance standalone VR hardware.
✦ Runtime-Generated UI
A large portion of FLuX's interface is generated directly at runtime.
This includes systems such as:
- Radial geometry
- AUX controls
- Particle halos
- Holographic decks
- Pointer
- Reactor rings
- Constellation nodes
- Spatial panels
- Fusion Void Doors
The custom FLuX wordmark is embedded directly into the assembly, keeping installation simple.
✦ Designed for VR
FLuX isn't intended to feel like a desktop menu floating inside VR.
Its interface is built around:
- Hands
- Wrist positioning
- Pointing
- Grabbing
- Throwing
- Physical proximity
- Spatial depth
- Context
- Haptics
- Animation
- World-space interaction
The objective is for tools to feel like they exist inside BONELAB's world.
Requirements
FLuX is designed as a BONELAB code mod using:
- MelonLoader / compatible BONELAB code-mod environment
- BoneLib
- BONELAB
- Fusion is optional and only required for Fusion-specific functionality
Exact compatibility may vary between BONELAB, BoneLib, LemonLoader/MelonLoader and Fusion versions while FLuX is under active development.
Development Status
FLuX is currently under active development.
Some experimental interfaces provide foundations/hooks for systems that are still being expanded. Features such as a full
