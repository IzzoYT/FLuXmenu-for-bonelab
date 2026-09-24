using FLuXmenu.Builtins;
using FLuXmenu.Config;
using FLuXmenu.Core;
using FLuXmenu.Radial;
using FLuXmenu.Runtime;
using FLuXmenu.SDK;
using MelonLoader;

namespace FLuXmenu;

public sealed class FLuXmenuMod : MelonMod
{
    private FluxConfig _config = null!;
    private FluxHost _host = null!;
    private IGameRuntime _game = null!;
    private RadialController _radial = null!;
    private FluxPage _home = null!;
    private string _dataRoot = null!;
    private int _frameCounter;

    public override void OnInitializeMelon()
    {
        _dataRoot = Path.Combine(Environment.CurrentDirectory, "UserData", "FLuXmenu");
        _config = FluxConfig.Load(Path.Combine(_dataRoot, "config.json"));
        _game = GameRuntimeFactory.Create();
        _host = new FluxHost(_dataRoot, _config, _game);
        FluxAPI.BindHost(_host);

        _home = BuiltinPages.CreateHome(_host, _game);
        _host.RegisterPage(_home);

        _radial = new RadialController();
        _radial.Opened += page =>
        {
            _game.ShowRadial(page, RadialController.VisibleItems(page));
            FluxAPI.RaisePageOpened(page);
        };
        _radial.Closed += () => { _game.HideRadial(); FluxAPI.RaiseMenuClosed(); };
        _radial.SelectionChanged += item =>
        {
            _game.UpdateRadialSelection(item);
            if (_config.Haptics && item is not null) _game.PulseHaptics(_config.HapticStrength);
            FluxAPI.RaiseSelectionChanged(item);
        };
        _radial.Activated += FluxAPI.RaiseActionTriggered;

        DiscoverPlugins();
        MelonLogger.Msg("FLuXmenu 0.1.0 initialized. Black. White. FLuX.");
    }

    public override void OnUpdate()
    {
        if (_game.MenuPressedThisFrame() && !_radial.IsOpen)
        {
            _radial.Open(_home);
            FluxAPI.RaiseMenuOpened();
        }

        if (_radial.IsOpen)
        {
            if (_game.BackPressedThisFrame()) _radial.Back();
            else
            {
                _radial.UpdateSelection(_game.ReadMenuAxis(), _config.DeadZone);
                if (_game.MenuReleasedThisFrame()) _radial.Release();
            }
        }

        if (++_frameCounter % 120 == 0) _host.RefreshRuntime();
        DrainNotifications();
    }

    public override void OnDeinitializeMelon()
    {
        try { _config.Save(Path.Combine(_dataRoot, "config.json")); } catch { }
        _host?.Dispose();
        if (_host is not null) FluxAPI.UnbindHost(_host);
    }

    private void DiscoverPlugins()
    {
        foreach (var provider in PluginDiscovery.FindProviders(MelonLogger.Warning))
        {
            try { _host.RegisterPlugin(provider.CreateFluxPlugin()); }
            catch (Exception ex) { MelonLogger.Error($"FLuX plugin discovery failed: {ex}"); }
        }
    }

    private void DrainNotifications()
    {
        // The final Unity UI backend should render these as monochrome toast cards.
        // Logging them now gives every SDK caller deterministic behavior during early development.
        while (_host.NextNotification is { } note)
            MelonLogger.Msg($"[FLuX:{note.Kind}] {note.Title}: {note.Message}");
    }
}
