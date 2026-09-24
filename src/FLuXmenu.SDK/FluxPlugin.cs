namespace FLuXmenu.SDK;

public interface IFluxPluginProvider
{
    FluxPlugin CreateFluxPlugin();
}

public abstract class FluxPlugin : IDisposable
{
    public abstract string Id { get; }
    public abstract string Name { get; }
    public virtual string Author => "Unknown";
    public virtual string Version => "1.0.0";
    public virtual string Description => string.Empty;
    public virtual FluxCapability Capabilities => FluxCapability.RadialPages;
    public virtual bool QuestSupported => true;
    public virtual bool PcvrSupported => true;
    public virtual bool FusionCompatible => true;

    public virtual void OnFluxReady() { }
    public virtual void OnFluxShutdown() { }
    public virtual void Dispose() => OnFluxShutdown();

    public FluxPluginManifest ToManifest() => new(
        Id, Name, Version, Author, Capabilities,
        QuestSupported, PcvrSupported, FusionCompatible,
        FluxAPI.SdkVersion, Description);
}
