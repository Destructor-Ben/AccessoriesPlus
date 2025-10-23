namespace AccessoriesPlus.Utilities;

public static class InfoDisplayUtils
{
    public static bool IsActive(InfoDisplay display)
    {
        return display.Active() && !Main.LocalPlayer.hideInfo[display.Type];
    }
}
