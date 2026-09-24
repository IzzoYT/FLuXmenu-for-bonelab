namespace FLuXmenu.Core;

public sealed record FluxCommand(string Id, Action Action, string DisplayName, string Source = "core");

public sealed class CommandRegistry
{
    private readonly Dictionary<string, FluxCommand> _commands = new(StringComparer.OrdinalIgnoreCase);
    public IReadOnlyCollection<FluxCommand> Commands => _commands.Values;

    public bool Register(string id, Action action, string? displayName = null, string source = "sdk")
    {
        if (string.IsNullOrWhiteSpace(id) || action is null) return false;
        if (_commands.ContainsKey(id)) return false;
        _commands[id] = new FluxCommand(id, action, displayName ?? id, source);
        return true;
    }

    public bool Execute(string id, Action<Exception>? onError = null)
    {
        if (!_commands.TryGetValue(id, out var command)) return false;
        try { command.Action(); return true; }
        catch (Exception ex) { onError?.Invoke(ex); return false; }
    }

    public bool Contains(string id) => _commands.ContainsKey(id);
    public FluxCommand? Get(string id) => _commands.TryGetValue(id, out var value) ? value : null;
}
