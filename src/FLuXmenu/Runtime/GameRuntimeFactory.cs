namespace FLuXmenu.Runtime;

public static class GameRuntimeFactory
{
    public static IGameRuntime Create()
    {
        // Intentionally isolated. Add exact BONELAB/SLZ input, rig, physics and UI bindings here
        // for the game patch and loader versions you target. Keeping these bindings behind one
        // interface prevents the public SDK from breaking when game internals change.
        return new NullGameRuntime();
    }
}
