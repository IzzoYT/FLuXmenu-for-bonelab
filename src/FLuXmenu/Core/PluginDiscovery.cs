using FLuXmenu.SDK;

namespace FLuXmenu.Core;

public static class PluginDiscovery
{
    public static IEnumerable<IFluxPluginProvider> FindProviders(Action<string>? log = null)
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            Type[] types;
            try { types = assembly.GetTypes(); }
            catch { continue; }
            foreach (var type in types)
            {
                if (type.IsAbstract || type.IsInterface || !typeof(IFluxPluginProvider).IsAssignableFrom(type)) continue;
                try
                {
                    if (Activator.CreateInstance(type) is IFluxPluginProvider provider) yield return provider;
                }
                catch (Exception ex) { log?.Invoke($"Provider {type.FullName} failed: {ex.Message}"); }
            }
        }
    }
}
