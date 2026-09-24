using FLuXmenu.SDK;
using FLuXmenu.Radial;

namespace FLuXmenu.Runtime;

public interface IGameRuntime
{
    FluxPlatform Platform { get; }
    bool FusionInstalled { get; }
    bool FusionConnected { get; }
    float FramesPerSecond { get; }
    float FrameTimeMs { get; }
    string SceneName { get; }
    FluxVec2 ReadMenuAxis();
    bool MenuPressedThisFrame();
    bool MenuReleasedThisFrame();
    bool BackPressedThisFrame();
    void PulseHaptics(float amplitude, float seconds = 0.025f);
    void ShowRadial(FluxPage page, IReadOnlyList<FluxItem> visibleItems);
    void UpdateRadialSelection(FluxItem? item);
    void HideRadial();
    FluxTarget? RaycastContextTarget();

    // Game operations used by built-in actions. Return false if unavailable on the current game build.
    bool HealPlayer();
    bool RespawnPlayer();
    bool ReloadLevel();
    bool ReturnToMenu();
    bool SetTimeScale(float value);
    bool RemoveLastSpawned();
    bool ClearFluxSpawned();
}
