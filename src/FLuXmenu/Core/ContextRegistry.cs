using FLuXmenu.SDK;

namespace FLuXmenu.Core;

public sealed class ContextRegistry
{
    private readonly Dictionary<string, List<FluxContextAction>> _actions = new(StringComparer.OrdinalIgnoreCase);

    public void Register(string typeId, FluxContextAction action)
    {
        if (!_actions.TryGetValue(typeId, out var list)) _actions[typeId] = list = new List<FluxContextAction>();
        if (list.Any(x => x.Id.Equals(action.Id, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"Duplicate context action '{action.Id}' for '{typeId}'.");
        list.Add(action);
    }

    public IReadOnlyList<FluxContextAction> Resolve(FluxTarget target)
    {
        var result = new List<FluxContextAction>();
        if (_actions.TryGetValue("*", out var any)) result.AddRange(any);
        if (_actions.TryGetValue(target.TypeId, out var exact)) result.AddRange(exact);
        return result.Where(a => SafeEnabled(a, target)).ToArray();
    }

    private static bool SafeEnabled(FluxContextAction a, FluxTarget t)
    {
        if (a.IsEnabled is null) return true;
        try { return a.IsEnabled(t); } catch { return false; }
    }
}
