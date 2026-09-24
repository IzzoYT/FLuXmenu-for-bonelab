using System.Text.Json;
using FLuXmenu.SDK;

namespace FLuXmenu.Core;

public sealed class PluginStorage : IFluxPluginStorage
{
    private readonly string _path;
    private readonly Dictionary<string, JsonElement> _values = new(StringComparer.OrdinalIgnoreCase);

    public PluginStorage(string root, string pluginId)
    {
        _path = Path.Combine(root, "Plugins", Sanitize(pluginId) + ".json");
        Load();
    }

    public T Get<T>(string key, T fallback = default!)
    {
        if (!_values.TryGetValue(key, out var value)) return fallback;
        try { return value.Deserialize<T>() ?? fallback; } catch { return fallback; }
    }

    public void Set<T>(string key, T value) => _values[key] = JsonSerializer.SerializeToElement(value);
    public bool Remove(string key) => _values.Remove(key);

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
        File.WriteAllText(_path, JsonSerializer.Serialize(_values, new JsonSerializerOptions { WriteIndented = true }));
    }

    private void Load()
    {
        try
        {
            if (!File.Exists(_path)) return;
            var data = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(File.ReadAllText(_path));
            if (data is null) return;
            foreach (var pair in data) _values[pair.Key] = pair.Value;
        }
        catch { }
    }

    private static string Sanitize(string input) => string.Concat(input.Select(c => char.IsLetterOrDigit(c) || c is '.' or '-' or '_' ? c : '_'));
}
