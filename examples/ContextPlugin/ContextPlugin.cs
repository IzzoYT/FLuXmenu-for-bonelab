using FLuXmenu.SDK;

namespace ContextPlugin;

public sealed class ContextTools : FluxPlugin
{
    public override string Id => "nova.contexttools";
    public override string Name => "Context Tools";
    public override FluxCapability Capabilities => FluxCapability.ContextActions | FluxCapability.Notifications;

    public override void OnFluxReady()
    {
        FluxAPI.RegisterContext("weapon", new FluxContextAction(
            "nova.context.weapon.info",
            "Weapon Info",
            target => FluxAPI.Notify("Context", $"Target: {target.DisplayName}"),
            IconId: "info"));

        FluxAPI.RegisterContext("npc", new FluxContextAction(
            "nova.context.npc.info",
            "NPC Info",
            target => FluxAPI.Notify("Context", $"NPC: {target.DisplayName}"),
            IconId: "person"));
    }
}
