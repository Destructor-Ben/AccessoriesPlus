namespace AccessoriesPlus.Utilities;

public static class MathUtils
{
    public static float Round(float value, float nearest = 1f)
    {
        return MathF.Round(value / nearest) * nearest;
    }
}
