namespace FLuXmenu.SDK;

public static class FluxAPI
{
    internal static IFluxHost? Host { get; private set; }
    public const string SdkVersion = "0.1.0";
    public static bool Available => Host is not null;
    public static FluxRuntimeInfo Runtime => Host?.Runtime ?? new(false, FluxPlatform.Unknown, false, false, "0", SdkVersion);

    public static event Action? MenuOpened;
    public static event Action? MenuClosed;
    public static event Action<FluxPage>? PageOpened;
    public static event Action<FluxItem?>? SelectionChanged;
    public static event Action<FluxItem>? ActionTriggered;
    public static event Action<FluxRuntimeInfo>? RuntimeChanged;

    public static FluxPage CreatePage(string id, string name, string? iconId = null) => new(id, name, iconId);
    public static void RegisterPage(FluxPage page) => RequireHost().RegisterPage(page);
    public static void RegisterPlugin(FluxPlugin plugin) => RequireHost().RegisterPlugin(plugin);
    public static bool RegisterCommand(string id, Action action, string? displayName = null) => RequireHost().RegisterCommand(id, action, displayName);
    public static bool Execute(string id) => RequireHost().ExecuteCommand(id);
    public static void RegisterContext(string targetTypeId, FluxContextAction action) => RequireHost().RegisterContext(targetTypeId, action);
    public static IReadOnlyList<FluxContextAction> GetContextActions(FluxTarget target) => RequireHost().GetContextActions(target);
    public static void Notify(string message) => Notify("FLuXmenu", message);
    public static void Notify(string title, string message, FluxNotificationKind kind = FluxNotificationKind.Info, float duration = 2.5f)
        => RequireHost().Notify(new FluxNotification(title, message, kind, duration));
    public static bool AddFavorite(string commandId) => RequireHost().AddFavorite(commandId);
    public static bool RemoveFavorite(string commandId) => RequireHost().RemoveFavorite(commandId);
    public static IReadOnlyList<string> Favorites => RequireHost().Favorites;
    public static IReadOnlyList<string> Recent => RequireHost().Recent;
    public static void RegisterMacro(FluxMacroDefinition macro) => RequireHost().RegisterMacro(macro);
    public static bool ExecuteMacro(string id) => RequireHost().ExecuteMacro(id);
    public static IFluxPluginStorage Storage(string pluginId) => RequireHost().Storage(pluginId);

    internal static void BindHost(IFluxHost host) => Host = host;
    internal static void UnbindHost(IFluxHost host) { if (ReferenceEquals(Host, host)) Host = null; }

    internal static void RaiseMenuOpened() => MenuOpened?.Invoke();
    internal static void RaiseMenuClosed() => MenuClosed?.Invoke();
    internal static void RaisePageOpened(FluxPage page) => PageOpened?.Invoke(page);
    internal static void RaiseSelectionChanged(FluxItem? item) => SelectionChanged?.Invoke(item);
    internal static void RaiseActionTriggered(FluxItem item) => ActionTriggered?.Invoke(item);
    internal static void RaiseRuntimeChanged(FluxRuntimeInfo info) => RuntimeChanged?.Invoke(info);

    private static IFluxHost RequireHost() => Host ?? throw new InvalidOperationException("FLuXmenu host is not available.");
}

public interface IFluxPluginStorage
{
    T Get<T>(string key, T fallback = default!);
    void Set<T>(string key, T value);
    bool Remove(string key);
    void Save();
}

public interface IFluxHost
{
    FluxRuntimeInfo Runtime { get; }
    IReadOnlyList<string> Favorites { get; }
    IReadOnlyList<string> Recent { get; }
    void RegisterPage(FluxPage page);
    void RegisterPlugin(FluxPlugin plugin);
    bool RegisterCommand(string id, Action action, string? displayName);
    bool ExecuteCommand(string id);
    void RegisterContext(string targetTypeId, FluxContextAction action);
    IReadOnlyList<FluxContextAction> GetContextActions(FluxTarget target);
    void Notify(FluxNotification notification);
    bool AddFavorite(string commandId);
    bool RemoveFavorite(string commandId);
    void RegisterMacro(FluxMacroDefinition macro);
    bool ExecuteMacro(string id);
    IFluxPluginStorage Storage(string pluginId);
}
