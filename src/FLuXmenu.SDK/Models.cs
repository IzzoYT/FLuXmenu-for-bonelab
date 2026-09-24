namespace FLuXmenu.SDK;

public enum FluxPlatform { Unknown, Quest, PCVR }
public enum FluxNotificationKind { Info, Success, Warning, Error }
public enum FluxItemKind { Button, Toggle, Slider, Choice, DynamicLabel, PageLink, Separator }

public readonly record struct FluxRuntimeInfo(
    bool HostAvailable,
    FluxPlatform Platform,
    bool FusionInstalled,
    bool FusionConnected,
    string HostVersion,
    string SdkVersion);

public sealed record FluxPluginManifest(
    string Id,
    string Name,
    string Version,
    string Author,
    FluxCapability Capabilities,
    bool QuestSupported = true,
    bool PcvrSupported = true,
    bool FusionCompatible = true,
    string? MinimumSdk = null,
    string? Description = null);

public sealed record FluxTarget(
    object Instance,
    string TypeId,
    string DisplayName,
    IReadOnlyDictionary<string, object?>? Metadata = null);

public sealed record FluxContextAction(
    string Id,
    string Label,
    Action<FluxTarget> Execute,
    Func<FluxTarget, bool>? IsEnabled = null,
    string? IconId = null,
    bool CanFavorite = false);

public sealed record FluxNotification(
    string Title,
    string Message,
    FluxNotificationKind Kind = FluxNotificationKind.Info,
    float DurationSeconds = 2.5f);

public sealed record FluxMacroDefinition(string Id, string Name, IReadOnlyList<string> Commands);
