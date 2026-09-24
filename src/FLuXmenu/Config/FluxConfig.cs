using System.Text.Json;

namespace FLuXmenu.Config;

public enum FluxOpenMode { HoldRelease, Toggle }
public enum FluxHand { Left, Right }

public sealed class FluxConfig
{
    public FluxHand MenuHand { get; set; } = FluxHand.Left;
    public FluxOpenMode OpenMode { get; set; } = FluxOpenMode.HoldRelease;
    public float RadiusMeters { get; set; } = 0.16f;
    public float DeadZone { get; set; } = 0.28f;
    public float SelectionStickiness { get; set; } = 0.08f;
    public float Opacity { get; set; } = 0.86f;
    public float UiScale { get; set; } = 1f;
    public float HapticStrength { get; set; } = 0.55f;
    public float AnimationSpeed { get; set; } = 1f;
    public bool Haptics { get; set; } = true;
    public bool Sounds { get; set; } = true;
    public bool IconOnly { get; set; }
    public bool PerformanceGovernor { get; set; } = true;
    public string ActiveProfile { get; set; } = "Default";
    public List<string> Favorites { get; set; } = new();
    public List<string> Recent { get; set; } = new();

    public static FluxConfig Load(string path)
    {
        try
        {
            if (!File.Exists(path)) return new FluxConfig();
            return JsonSerializer.Deserialize<FluxConfig>(File.ReadAllText(path), JsonOptions) ?? new FluxConfig();
        }
        catch { return new FluxConfig(); }
    }

    public void Save(string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllText(path, JsonSerializer.Serialize(this, JsonOptions));
    }

    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
}
