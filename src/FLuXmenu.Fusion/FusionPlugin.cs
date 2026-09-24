using FLuXmenu.SDK;

namespace FLuXmenu.Fusion;

public sealed class FusionPlugin : FluxPlugin
{
    public override string Id => "flux.fusion";
    public override string Name => "Fusion Integration";
    public override string Author => "FLuXmenu";
    public override string Version => "0.1.0";
    public override FluxCapability Capabilities => FluxCapability.RadialPages | FluxCapability.Fusion | FluxCapability.DynamicValues;

    public override void OnFluxReady()
    {
        if (!FluxAPI.Runtime.FusionInstalled) return;
        var page = FluxAPI.CreatePage("flux.fusion.main", "Fusion", "fusion");
        page.AddDynamicLabel("status", "Status", () => FluxAPI.Runtime.FusionConnected ? "Connected" : "Installed");
        page.AddDynamicLabel("note", "Adapter", () => "Bind exact LabFusion APIs for player/session actions");
        FluxAPI.RegisterPage(page);
    }
}
