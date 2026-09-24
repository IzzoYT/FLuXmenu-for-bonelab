namespace FLuXmenu.SDK;

public sealed class FluxPage
{
    private readonly List<FluxItem> _items = new();
    public FluxPage(string id, string name, string? iconId = null)
    {
        Id = Guard.Id(id); Name = name; IconId = iconId;
    }

    public string Id { get; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? IconId { get; set; }
    public IReadOnlyList<FluxItem> Items => _items;

    public FluxButton AddButton(string id, string label, Action action, string? iconId = null)
        => Add(new FluxButton(Qualify(id), label, action, iconId));

    public FluxToggle AddToggle(string id, string label, Func<bool> getter, Action<bool> setter, string? iconId = null)
        => Add(new FluxToggle(Qualify(id), label, getter, setter, iconId));

    public FluxSlider AddSlider(string id, string label, double min, double max, double step, Func<double> getter, Action<double> setter, string? iconId = null)
        => Add(new FluxSlider(Qualify(id), label, min, max, step, getter, setter, iconId));

    public FluxChoice AddChoice(string id, string label, IReadOnlyList<string> options, Func<int> getter, Action<int> setter, string? iconId = null)
        => Add(new FluxChoice(Qualify(id), label, options, getter, setter, iconId));

    public FluxDynamicLabel AddDynamicLabel(string id, string label, Func<string> value, string? iconId = null)
        => Add(new FluxDynamicLabel(Qualify(id), label, value, iconId));

    public FluxPage AddPage(string id, string label, string? iconId = null)
    {
        var page = new FluxPage(Qualify(id), label, iconId);
        Add(new FluxPageLink(page.Id, label, page, iconId));
        return page;
    }

    public FluxPageLink LinkPage(string id, string label, FluxPage target, string? iconId = null)
        => Add(new FluxPageLink(Qualify(id), label, target, iconId));

    public FluxItem? Find(string id) => _items.FirstOrDefault(i => string.Equals(i.Id, id, StringComparison.OrdinalIgnoreCase));

    private T Add<T>(T item) where T : FluxItem
    {
        if (_items.Any(x => string.Equals(x.Id, item.Id, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"Duplicate item ID '{item.Id}' in page '{Id}'.");
        _items.Add(item); return item;
    }

    private string Qualify(string id) => id.Contains('.') ? Guard.Id(id) : $"{Id}.{Guard.Id(id)}";
}
