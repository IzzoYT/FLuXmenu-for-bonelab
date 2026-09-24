namespace FLuXmenu.Profiles;

public sealed record FluxProfile(string Name, HashSet<string> HiddenPages, HashSet<string> HiddenItems);

public sealed class ProfileManager
{
    private readonly Dictionary<string, FluxProfile> _profiles = new(StringComparer.OrdinalIgnoreCase);
    public FluxProfile Active { get; private set; }

    public ProfileManager()
    {
        Add(new("Default", new(), new()));
        Add(new("Gameplay", new(StringComparer.OrdinalIgnoreCase) { "flux.developer" }, new()));
        Add(new("Developer", new(), new()));
        Add(new("Fusion", new(), new()));
        Add(new("Minimal", new(StringComparer.OrdinalIgnoreCase) { "flux.developer", "flux.camera", "flux.inventory" }, new()));
        Active = _profiles["Default"];
    }

    public IEnumerable<FluxProfile> Profiles => _profiles.Values;
    public void Add(FluxProfile profile) => _profiles[profile.Name] = profile;
    public bool Activate(string name)
    {
        if (!_profiles.TryGetValue(name, out var p)) return false;
        Active = p; return true;
    }
}
