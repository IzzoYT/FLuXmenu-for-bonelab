namespace FLuXmenu.SDK;

public abstract class FluxItem
{
    protected FluxItem(string id, string label, FluxItemKind kind, string? iconId = null)
    {
        Id = Guard.Id(id);
        Label = label ?? throw new ArgumentNullException(nameof(label));
        Kind = kind;
        IconId = iconId;
    }

    public string Id { get; }
    public string Label { get; set; }
    public string? Description { get; set; }
    public string? IconId { get; set; }
    public FluxItemKind Kind { get; }
    public Func<bool>? VisibleWhen { get; set; }
    public Func<bool>? EnabledWhen { get; set; }
    public bool CanFavorite { get; set; } = true;
    public bool IsVisible => SafeBool(VisibleWhen, true);
    public bool IsEnabled => SafeBool(EnabledWhen, true);

    private static bool SafeBool(Func<bool>? getter, bool fallback)
    {
        if (getter is null) return fallback;
        try { return getter(); } catch { return false; }
    }
}

public sealed class FluxButton : FluxItem
{
    internal FluxButton(string id, string label, Action action, string? iconId = null)
        : base(id, label, FluxItemKind.Button, iconId) => Action = action;
    public Action Action { get; }
}

public sealed class FluxToggle : FluxItem
{
    internal FluxToggle(string id, string label, Func<bool> getter, Action<bool> setter, string? iconId = null)
        : base(id, label, FluxItemKind.Toggle, iconId) { Getter = getter; Setter = setter; }
    public Func<bool> Getter { get; }
    public Action<bool> Setter { get; }
}

public sealed class FluxSlider : FluxItem
{
    internal FluxSlider(string id, string label, double min, double max, double step, Func<double> getter, Action<double> setter, string? iconId = null)
        : base(id, label, FluxItemKind.Slider, iconId)
    {
        if (max <= min) throw new ArgumentOutOfRangeException(nameof(max));
        Min = min; Max = max; Step = step <= 0 ? (max - min) / 100d : step; Getter = getter; Setter = setter;
    }
    public double Min { get; }
    public double Max { get; }
    public double Step { get; }
    public Func<double> Getter { get; }
    public Action<double> Setter { get; }
}

public sealed class FluxChoice : FluxItem
{
    internal FluxChoice(string id, string label, IReadOnlyList<string> options, Func<int> getter, Action<int> setter, string? iconId = null)
        : base(id, label, FluxItemKind.Choice, iconId)
    {
        if (options.Count == 0) throw new ArgumentException("At least one option is required.", nameof(options));
        Options = options; Getter = getter; Setter = setter;
    }
    public IReadOnlyList<string> Options { get; }
    public Func<int> Getter { get; }
    public Action<int> Setter { get; }
}

public sealed class FluxDynamicLabel : FluxItem
{
    internal FluxDynamicLabel(string id, string label, Func<string> value, string? iconId = null)
        : base(id, label, FluxItemKind.DynamicLabel, iconId) { Value = value; CanFavorite = false; }
    public Func<string> Value { get; }
}

public sealed class FluxPageLink : FluxItem
{
    internal FluxPageLink(string id, string label, FluxPage page, string? iconId = null)
        : base(id, label, FluxItemKind.PageLink, iconId) { Page = page; CanFavorite = false; }
    public FluxPage Page { get; }
}

internal static class Guard
{
    internal static string Id(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("IDs cannot be empty.", nameof(id));
        if (id.Any(char.IsWhiteSpace)) throw new ArgumentException("IDs cannot contain whitespace.", nameof(id));
        return id;
    }
}
