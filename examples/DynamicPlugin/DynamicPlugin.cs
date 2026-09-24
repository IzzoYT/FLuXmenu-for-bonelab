using FLuXmenu.SDK;

namespace DynamicPlugin;

public sealed class DynamicPlugin : FluxPlugin
{
    private readonly DateTime _started = DateTime.UtcNow;
    public override string Id => "nova.dynamic";
    public override string Name => "Dynamic Values";
    public override FluxCapability Capabilities => FluxCapability.RadialPages | FluxCapability.DynamicValues;

    public override void OnFluxReady()
    {
        var page = FluxAPI.CreatePage("nova.dynamic.main", "Dynamic");
        page.AddDynamicLabel("uptime", "Uptime", () => (DateTime.UtcNow - _started).ToString(@"hh\:mm\:ss"));
        page.AddDynamicLabel("platform", "Platform", () => FluxAPI.Runtime.Platform.ToString());
        page.AddDynamicLabel("fusion", "Fusion", () => FluxAPI.Runtime.FusionConnected ? "Connected" : "Offline");
        FluxAPI.RegisterPage(page);
    }
}
