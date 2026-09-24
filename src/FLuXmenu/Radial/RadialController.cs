using FLuXmenu.SDK;

namespace FLuXmenu.Radial;

public sealed class RadialController
{
    private readonly Stack<FluxPage> _history = new();
    private int _selected = -1;

    public bool IsOpen { get; private set; }
    public FluxPage? CurrentPage { get; private set; }
    public FluxItem? SelectedItem { get; private set; }
    public event Action<FluxPage>? Opened;
    public event Action? Closed;
    public event Action<FluxItem?>? SelectionChanged;
    public event Action<FluxItem>? Activated;

    public void Open(FluxPage page)
    {
        _history.Clear();
        CurrentPage = page;
        IsOpen = true;
        SetSelection(-1);
        Opened?.Invoke(page);
    }

    public void UpdateSelection(FluxVec2 axis, float deadZone)
    {
        if (!IsOpen || CurrentPage is null) return;
        var visible = VisibleItems(CurrentPage);
        var index = RadialMath.SelectSegment(axis, visible.Count, deadZone);
        if (index == _selected) return;
        _selected = index;
        SetSelection(index >= 0 && index < visible.Count ? visible[index] : null);
    }

    public void Release()
    {
        if (!IsOpen) return;
        if (SelectedItem is null) { Close(); return; }
        Activate(SelectedItem);
    }

    public void Activate(FluxItem item)
    {
        if (!item.IsEnabled) return;
        try
        {
            switch (item)
            {
                case FluxButton button: button.Action(); break;
                case FluxToggle toggle: toggle.Setter(!toggle.Getter()); break;
                case FluxChoice choice:
                    var i = Math.Clamp(choice.Getter() + 1, 0, choice.Options.Count);
                    choice.Setter(i >= choice.Options.Count ? 0 : i); break;
                case FluxPageLink link:
                    if (CurrentPage is not null) _history.Push(CurrentPage);
                    CurrentPage = link.Page;
                    SetSelection(-1);
                    Opened?.Invoke(link.Page);
                    return;
                case FluxDynamicLabel: return;
                case FluxSlider: return; // sliders are adjusted by a secondary gesture/axis in the runtime adapter.
            }
            Activated?.Invoke(item);
        }
        finally
        {
            if (item is not FluxPageLink) Close();
        }
    }

    public bool Back()
    {
        if (!IsOpen) return false;
        if (_history.Count == 0) { Close(); return true; }
        CurrentPage = _history.Pop();
        SetSelection(-1);
        Opened?.Invoke(CurrentPage);
        return true;
    }

    public void Close()
    {
        if (!IsOpen) return;
        IsOpen = false;
        CurrentPage = null;
        _history.Clear();
        SetSelection(-1);
        Closed?.Invoke();
    }

    public static IReadOnlyList<FluxItem> VisibleItems(FluxPage page) => page.Items.Where(i => i.IsVisible).ToArray();

    private void SetSelection(int index)
    {
        _selected = index;
        FluxItem? next = null;
        if (CurrentPage is not null && index >= 0)
        {
            var items = VisibleItems(CurrentPage);
            if (index < items.Count) next = items[index];
        }
        if (ReferenceEquals(next, SelectedItem)) return;
        SelectedItem = next;
        SelectionChanged?.Invoke(next);
    }
}
