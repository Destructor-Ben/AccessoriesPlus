namespace AccessoriesPlus.Utilities;

public static class MathUtils
{
    /// <summary>
    /// A scalar to convert velocity in pixels per tick to miles per hour.
    /// </summary>
    public const float PPTToMPH = 216000f / 42240f;

    /// <summary>
    /// A scalar to convert acceleration in pixels per tick per tick to miles per hour per second.
    /// </summary>
    public const float PPTPTToMPHPS = PPTToMPH * 60f; // TODO: this isn't correct? should be divide, but then it doesn't work

    public static float Round(float value, float nearest = 1f)
    {
        return MathF.Round(value / nearest) * nearest;
    }
}
