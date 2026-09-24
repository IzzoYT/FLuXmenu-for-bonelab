using FLuXmenu.Core;

namespace FLuXmenu.Macros;

public sealed record FluxMacro(string Id, string Name, IReadOnlyList<string> Commands);

public sealed class MacroManager
{
    private readonly CommandRegistry _commands;
    private readonly Dictionary<string, FluxMacro> _macros = new(StringComparer.OrdinalIgnoreCase);
    public MacroManager(CommandRegistry commands) => _commands = commands;
    public void Register(FluxMacro macro) => _macros[macro.Id] = macro;
    public bool Execute(string id)
    {
        if (!_macros.TryGetValue(id, out var macro)) return false;
        var ok = true;
        foreach (var command in macro.Commands) ok &= _commands.Execute(command);
        return ok;
    }
}
