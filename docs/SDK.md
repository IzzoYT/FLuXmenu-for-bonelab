# FLuXmenu SDK

Reference `FLuXmenu.SDK.dll` with `Private=false` so your package does not ship a duplicate SDK assembly.

## Stable IDs

Treat plugin, page, item and command IDs as compatibility keys. Once published, do not reuse an old ID for a different action.

## Plugin lifecycle

Create a `FluxPlugin` and register it with `FluxAPI.RegisterPlugin`, or expose it through an `IFluxPluginProvider` that the host discovers.

## Pages

Create pages with `FluxAPI.CreatePage`, add controls, and register the root page with `FluxAPI.RegisterPage`.

Supported primitives in the scaffold:

- button
- toggle
- slider
- choice
- dynamic label
- nested page/page link

## Commands

Every actionable built-in/page item can have a stable command ID. SDK plugins may also register standalone commands with `FluxAPI.RegisterCommand` and execute commands with `FluxAPI.Execute`.

## Favorites

Call `FluxAPI.AddFavorite(commandId)` or `RemoveFavorite`. The host persists favorites in its configuration. The final favorites-page renderer should resolve those IDs into live command entries.

## Context actions

Register actions by target type ID such as `weapon`, `npc`, `vehicle` or `*`. A concrete BONELAB runtime adapter supplies the `FluxTarget` from its raycast/object classifier.

## Storage

`FluxAPI.Storage(pluginId)` provides simple JSON-backed key/value storage. Plugins should avoid writing into FLuXmenu's core config directly.

## Error isolation

The host wraps plugin command execution. Plugin callbacks should still keep work short and should not manipulate Unity objects from background threads.
