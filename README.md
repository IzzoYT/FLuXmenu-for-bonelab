# FLuXmenu-for-bonelab
FLuXmenu — A modular radial utility framework for BONELAB.
**A modular radial utility framework for BONELAB.**

FLuXmenu is a fast, lightweight radial menu system built specifically for BONELAB.

Instead of using a traditional wrist menu or floating desktop-style interface, FLuXmenu uses a VR-native radial interaction system inspired by weapon wheels and quick-select interfaces.

Hold the configured button, move toward an option, and release to select.

FLuXmenu uses a minimal **black-and-white visual style**, smooth animations, haptic feedback, nested radial pages, built-in gameplay utilities, and a public SDK that allows other BONELAB code mods to integrate directly into the menu.

## Core Features

* Black-and-white radial interface
* Fast controller-based navigation
* Nested radial menus
* Favorites wheel
* Recent actions
* Player utilities
* Spawn utilities
* Time controls
* Developer tools
* Quest and PCVR support
* Optional LabFusion integration
* Custom profiles
* Configurable controls
* Haptic feedback
* Custom icons
* Plugin system
* Public SDK

## FLuXmenu SDK

FLuXmenu is designed to act as a shared UI framework for BONELAB code mods.

Other mods can register their own:

* Pages
* Actions
* Toggles
* Sliders
* Selectors
* Dynamic labels
* Notifications
* Submenus
* Settings
* Icons
* Context actions

without modifying FLuXmenu itself.

```csharp
using FLuXmenu.SDK;

public class ExamplePlugin : FluxPlugin
{
    public override string Name => "Example Tools";
    public override string Author => "Nova";

    public override void OnInitialize()
    {
        var page = FluxAPI.CreatePage(
            "example.main",
            "Example"
        );

        page.AddButton(
            "hello",
            "Hello",
            () =>
            {
                FluxAPI.Notify("Hello from FLuXmenu!");
            }
        );

        page.AddToggle(
            "feature",
            "Feature",
            false,
            enabled =>
            {
                MelonLogger.Msg($"Feature: {enabled}");
            }
        );

        FluxAPI.RegisterPage(page);
    }
}
```

FLuXmenu handles the radial rendering, input, navigation, haptics, menu placement, configuration, and platform differences automatically.

## Design Philosophy

FLuXmenu is built around four goals:

**Fast**
Common actions should be accessible almost instantly.

**VR Native**
The interface should behave like a VR interaction system, not a desktop UI attached to your hand.

**Extensible**
Other mods should be able to use FLuXmenu through a simple SDK.

**Lightweight**
The system should remain practical for standalone Quest hardware.

## Visual Style

FLuXmenu uses a strictly monochrome interface.

* Matte black backgrounds
* White outlines
* White icons
* White text
* Inverted white selection segments
* Black icons on selected segments
* Minimal animations
* No unnecessary visual clutter

**Black. White. FLuX.**
