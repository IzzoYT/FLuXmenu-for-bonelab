#if FLUX_NO_MELON
namespace MelonLoader;
public abstract class MelonMod
{
    public virtual void OnInitializeMelon() { }
    public virtual void OnUpdate() { }
    public virtual void OnDeinitializeMelon() { }
}
public static class MelonLogger
{
    public static void Msg(string message) => Console.WriteLine(message);
    public static void Warning(string message) => Console.WriteLine("WARN: " + message);
    public static void Error(string message) => Console.Error.WriteLine(message);
}
#endif
