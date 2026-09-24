using FLuXmenu.Core;
using FLuXmenu.Runtime;
using FLuXmenu.SDK;

namespace FLuXmenu.Builtins;

public static class BuiltinPages
{
    public static FluxPage CreateHome(FluxHost host, IGameRuntime game)
    {
        var home = new FluxPage("flux.home", "FLuXmenu", "flux");
        home.Description = "Black. White. FLuX.";

        var player = home.AddPage("player", "Player", "person");
        player.AddButton("heal", "Heal", () => Require(game.HealPlayer(), "Heal is unavailable on this game binding."), "heart");
        player.AddButton("respawn", "Respawn", () => Require(game.RespawnPlayer(), "Respawn is unavailable."), "respawn");
        player.AddButton("reload", "Reload Level", () => Require(game.ReloadLevel(), "Reload is unavailable."), "reload");
        player.AddButton("menu", "Return to Menu", () => Require(game.ReturnToMenu(), "Return-to-menu is unavailable."), "home");
        player.AddDynamicLabel("scene", "Scene", () => game.SceneName, "scene");

        var time = home.AddPage("time", "Time", "clock");
        AddTime(time, game, "normal", "1.0x", 1f);
        AddTime(time, game, "075", "0.75x", .75f);
        AddTime(time, game, "050", "0.50x", .50f);
        AddTime(time, game, "025", "0.25x", .25f);
        AddTime(time, game, "010", "0.10x", .10f);
        AddTime(time, game, "freeze", "Freeze", 0f);

        var spawn = home.AddPage("spawn", "Spawn", "cube");
        spawn.AddButton("last.remove", "Remove Last", () => Require(game.RemoveLastSpawned(), "Spawn tracking is unavailable."));
        spawn.AddButton("clear", "Clear FLuX Spawns", () => Require(game.ClearFluxSpawned(), "Spawn tracking is unavailable."));
        spawn.AddDynamicLabel("hint", "Favorites", () => "Game binding can populate spawnables dynamically");

        var inventory = home.AddPage("inventory", "Inventory", "inventory");
        inventory.AddDynamicLabel("status", "Status", () => "Inventory adapter not bound");

        var camera = home.AddPage("camera", "Camera", "camera");
        camera.AddDynamicLabel("status", "Status", () => "Camera plugins can register here through the SDK");

        var dev = home.AddPage("developer", "Developer", "terminal");
        dev.AddDynamicLabel("fps", "FPS", () => $"{game.FramesPerSecond:0}");
        dev.AddDynamicLabel("frametime", "Frame Time", () => $"{game.FrameTimeMs:0.0} ms");
        dev.AddDynamicLabel("scene", "Scene", () => game.SceneName);
        dev.AddDynamicLabel("platform", "Platform", () => host.Runtime.Platform.ToString());
        dev.AddDynamicLabel("plugins", "Plugins", () => host.Plugins.Count.ToString());
        dev.AddDynamicLabel("commands", "Commands", () => host.Commands.Commands.Count.ToString());

        var plugins = home.AddPage("plugins", "Plugins", "plugin");
        plugins.AddDynamicLabel("loaded", "Loaded", () => host.Plugins.Count.ToString());

        var favorites = home.AddPage("favorites", "Favorites", "star");
        favorites.AddDynamicLabel("count", "Pinned", () => host.Favorites.Count.ToString());

        var settings = home.AddPage("settings", "Settings", "gear");
        settings.AddDynamicLabel("sdk", "SDK", () => FluxAPI.SdkVersion);
        settings.AddDynamicLabel("host", "Host", () => host.Runtime.HostVersion);
        settings.AddDynamicLabel("fusion", "Fusion", () => host.Runtime.FusionInstalled ? (host.Runtime.FusionConnected ? "Connected" : "Installed") : "Not installed");

        return home;
    }

    private static void AddTime(FluxPage page, IGameRuntime game, string id, string label, float value)
        => page.AddButton(id, label, () => Require(game.SetTimeScale(value), "Time scale is unavailable."), "clock");

    private static void Require(bool result, string message)
    {
        if (!result) throw new InvalidOperationException(message);
    }
}
