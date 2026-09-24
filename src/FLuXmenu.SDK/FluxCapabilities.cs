namespace FLuXmenu.SDK;

[Flags]
public enum FluxCapability
{
    None = 0,
    RadialPages = 1 << 0,
    Favorites = 1 << 1,
    Notifications = 1 << 2,
    ContextActions = 1 << 3,
    DynamicValues = 1 << 4,
    Spawnables = 1 << 5,
    PlayerActions = 1 << 6,
    Fusion = 1 << 7,
    Camera = 1 << 8,
    Debug = 1 << 9,
    Persistence = 1 << 10,
    CustomIcons = 1 << 11,
    Commands = 1 << 12,
    Macros = 1 << 13,
    Settings = 1 << 14,
}
