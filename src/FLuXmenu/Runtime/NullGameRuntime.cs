using FLuXmenu.SDK;
using FLuXmenu.Radial;

namespace FLuXmenu.Runtime;

public sealed class NullGameRuntime : IGameRuntime
{
    public FluxPlatform Platform => FluxPlatform.Unknown;
    public bool FusionInstalled => false;
    public bool FusionConnected => false;
    public float FramesPerSecond => 0;
    public float FrameTimeMs => 0;
    public string SceneName => "Unknown";
    public FluxVec2 ReadMenuAxis() => default;
    public bool MenuPressedThisFrame() => false;
    public bool MenuReleasedThisFrame() => false;
    public bool BackPressedThisFrame() => false;
    public void PulseHaptics(float amplitude, float seconds = 0.025f) { }
    public void ShowRadial(FluxPage page, IReadOnlyList<FluxItem> visibleItems) { }
    public void UpdateRadialSelection(FluxItem? item) { }
    public void HideRadial() { }
    public FluxTarget? RaycastContextTarget() => null;
    public bool HealPlayer() => false;
    public bool RespawnPlayer() => false;
    public bool ReloadLevel() => false;
    public bool ReturnToMenu() => false;
    public bool SetTimeScale(float value) => false;
    public bool RemoveLastSpawned() => false;
    public bool ClearFluxSpawned() => false;
}
