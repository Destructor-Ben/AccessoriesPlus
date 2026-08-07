using System.Globalization;

namespace AccessoriesPlus.Utilities;

public static class MathUtils
{
    public const float PixelsPerTick2MilesPerHour = 216000f / 42240f;
    public const float PixelsPerTickPerTick2MilesPerHourPerSecond = PixelsPerTick2MilesPerHour * 60f;

    public static float Round(float value, float nearest = 1f)
    {
        return MathF.Round(value / nearest) * nearest;
    }

    public static string ToNiceString(this float f, int decimalPlaces)
    {
        return f.ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture);
    }
}
