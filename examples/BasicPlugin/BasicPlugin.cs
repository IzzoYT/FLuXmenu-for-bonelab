using FLuXmenu.SDK;

namespace BasicPlugin;

public sealed class BasicTools : FluxPlugin
{
    private bool _enabled = true;
    private double _strength = 1;
    private int _mode;

    public override string Id => "nova.basictools";
    public override string Name => "Basic Tools";
    public override string Author => "Nova";
    public override string Version => "1.0.0";
    public override FluxCapability Capabilities => FluxCapability.RadialPages | FluxCapability.Notifications | FluxCapability.Persistence;

    public override void OnFluxReady()
    {
        var store = FluxAPI.Storage(Id);
        _enabled = store.Get("enabled", true);
        _strength = store.Get("strength", 1d);

        var page = FluxAPI.CreatePage("nova.basictools.main", "Basic Tools", "tools");
        page.AddButton("hello", "Hello", () => FluxAPI.Notify(Name, "Hello from Basic Tools!"));
        page.AddToggle("enabled", "Enabled", () => _enabled, value => { _enabled = value; store.Set("enabled", value); store.Save(); });
        page.AddSlider("strength", "Strength", 0, 3, .1, () => _strength, value => { _strength = value; store.Set("strength", value); });
        page.AddChoice("mode", "Mode", new[] { "Normal", "Cinematic", "Debug" }, () => _mode, value => _mode = value);
        page.AddDynamicLabel("status", "Status", () => _enabled ? $"Ready {_strength:0.0}x" : "Disabled");
        FluxAPI.RegisterPage(page);
    }
}
