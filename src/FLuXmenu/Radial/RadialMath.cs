namespace FLuXmenu.Radial;

public readonly record struct FluxVec2(float X, float Y)
{
    public float Magnitude => MathF.Sqrt(X * X + Y * Y);
}

public static class RadialMath
{
    public static int SelectSegment(FluxVec2 axis, int segmentCount, float deadZone, float rotationOffsetDegrees = -90f)
    {
        if (segmentCount <= 0 || axis.Magnitude < Math.Clamp(deadZone, 0f, 0.95f)) return -1;
        var angle = MathF.Atan2(axis.Y, axis.X) * 180f / MathF.PI;
        angle = Normalize(angle - rotationOffsetDegrees);
        var slice = 360f / segmentCount;
        return (int)MathF.Floor((angle + slice * 0.5f) / slice) % segmentCount;
    }

    public static float Normalize(float degrees)
    {
        degrees %= 360f;
        if (degrees < 0) degrees += 360f;
        return degrees;
    }
}
