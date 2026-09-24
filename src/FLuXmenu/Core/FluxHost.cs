using FLuXmenu.Config;
using FLuXmenu.SDK;
using FLuXmenu.Runtime;
using FLuXmenu.Macros;

namespace FLuXmenu.Core;

public sealed class FluxHost : IFluxHost, IDisposable
{
    private readonly Dictionary<string, FluxPage> _pages = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, FluxPlugin> _plugins = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, PluginStorage> _storage = new(StringComparer.OrdinalIgnoreCase);
    private readonly Queue<FluxNotification> _notifications = new();
    private readonly ContextRegistry _contexts = new();
    private readonly string _dataRoot;
    private readonly FluxConfig _config;
    private readonly IGameRuntime _runtime;

    public FluxHost(string dataRoot, FluxConfig config, IGameRuntime runtime)
    {
        _dataRoot = dataRoot; _config = config; _runtime = runtime;
        Commands = new CommandRegistry();
        Macros = new MacroManager(Commands);
        Runtime = BuildRuntime();
    }

    public CommandRegistry Commands { get; }
    public MacroManager Macros { get; }
    public FluxRuntimeInfo Runtime { get; private set; }
    public IReadOnlyList<string> Favorites => _config.Favorites;
    public IReadOnlyList<string> Recent => _config.Recent;
    public IReadOnlyCollection<FluxPage> Pages => _pages.Values;
    public IReadOnlyCollection<FluxPlugin> Plugins => _plugins.Values;
    public FluxNotification? NextNotification => _notifications.TryDequeue(out var n) ? n : null;

    public void RegisterPage(FluxPage page)
    {
        if (_pages.ContainsKey(page.Id)) throw new InvalidOperationException($"Duplicate FLuX page '{page.Id}'.");
        _pages[page.Id] = page;
        RegisterPageCommands(page);
    }

    public void RegisterPlugin(FluxPlugin plugin)
    {
        if (_plugins.ContainsKey(plugin.Id)) throw new InvalidOperationException($"Duplicate FLuX plugin '{plugin.Id}'.");
        if (_runtime.Platform == FluxPlatform.Quest && !plugin.QuestSupported) return;
        if (_runtime.Platform == FluxPlatform.PCVR && !plugin.PcvrSupported) return;
        _plugins[plugin.Id] = plugin;
        try { plugin.OnFluxReady(); }
        catch (Exception ex) { Notify(new FluxNotification(plugin.Name, ex.Message, FluxNotificationKind.Error, 4)); }
    }

    public bool RegisterCommand(string id, Action action, string? displayName) => Commands.Register(id, Isolate(id, action), displayName);
    public bool ExecuteCommand(string id)
    {
        var ok = Commands.Execute(id, ex => Notify(new FluxNotification("Command Error", $"{id}: {ex.Message}", FluxNotificationKind.Error, 4)));
        if (ok) TrackRecent(id);
        return ok;
    }
    public void RegisterContext(string targetTypeId, FluxContextAction action) => _contexts.Register(targetTypeId, action);
    public IReadOnlyList<FluxContextAction> GetContextActions(FluxTarget target) => _contexts.Resolve(target);
    public void Notify(FluxNotification notification) => _notifications.Enqueue(notification);

    public bool AddFavorite(string commandId)
    {
        if (!Commands.Contains(commandId) || _config.Favorites.Contains(commandId, StringComparer.OrdinalIgnoreCase)) return false;
        _config.Favorites.Add(commandId); return true;
    }

    public bool RemoveFavorite(string commandId)
    {
        var item = _config.Favorites.FirstOrDefault(x => x.Equals(commandId, StringComparison.OrdinalIgnoreCase));
        return item is not null && _config.Favorites.Remove(item);
    }

    public void RegisterMacro(FluxMacroDefinition macro) => Macros.Register(new FluxMacro(macro.Id, macro.Name, macro.Commands));
    public bool ExecuteMacro(string id) => Macros.Execute(id);

    public IFluxPluginStorage Storage(string pluginId)
    {
        if (!_storage.TryGetValue(pluginId, out var storage)) _storage[pluginId] = storage = new PluginStorage(_dataRoot, pluginId);
        return storage;
    }

    public FluxPage? GetPage(string id) => _pages.TryGetValue(id, out var p) ? p : null;
    public void RefreshRuntime()
    {
        var next = BuildRuntime();
        if (next == Runtime) return;
        Runtime = next; FluxAPI.RaiseRuntimeChanged(next);
    }

    public void Dispose()
    {
        foreach (var plugin in _plugins.Values)
        {
            try { plugin.Dispose(); } catch { }
        }
        foreach (var storage in _storage.Values) { try { storage.Save(); } catch { } }
    }

    private void TrackRecent(string id)
    {
        _config.Recent.RemoveAll(x => x.Equals(id, StringComparison.OrdinalIgnoreCase));
        _config.Recent.Insert(0, id);
        if (_config.Recent.Count > 12) _config.Recent.RemoveRange(12, _config.Recent.Count - 12);
    }

    private FluxRuntimeInfo BuildRuntime() => new(true, _runtime.Platform, _runtime.FusionInstalled, _runtime.FusionConnected, "0.1.0", FluxAPI.SdkVersion);

    private void RegisterPageCommands(FluxPage page)
    {
        foreach (var item in page.Items)
        {
            switch (item)
            {
                case FluxButton b: Commands.Register(b.Id, Isolate(b.Id, b.Action), b.Label, page.Id); break;
                case FluxToggle t: Commands.Register(t.Id, Isolate(t.Id, () => t.Setter(!t.Getter())), t.Label, page.Id); break;
                case FluxChoice c: Commands.Register(c.Id, Isolate(c.Id, () => { var n = c.Getter() + 1; c.Setter(n >= c.Options.Count ? 0 : n); }), c.Label, page.Id); break;
                case FluxPageLink l: RegisterPageCommands(l.Page); break;
            }
        }
    }

    private Action Isolate(string source, Action action) => () =>
    {
        try { action(); }
        catch (Exception ex)
        {
            Notify(new FluxNotification("Plugin Error", $"{source}: {ex.Message}", FluxNotificationKind.Error, 4));
            throw;
        }
    };
}
