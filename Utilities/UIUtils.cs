using Terraria.ModLoader.UI;

namespace AccessoriesPlus.Utilities;

public static class UIUtils
{
    public static void MouseText(string text, bool tooltip = false)
    {
        if (tooltip)
            UICommon.TooltipMouseText(text);
        else
            Main.instance.MouseText(text);
    }

    /// <summary>
    /// Resets all mouse text for this frame.
    /// </summary>
    /// TODO: this probablt doesn't work properly, also do Main.instance.MouseText("") and set hoverItemName and HoverItem
    public static void ResetMouseText()
    {
        Main.LocalPlayer.cursorItemIconEnabled = false;
        Main.LocalPlayer.cursorItemIconID = ItemID.None;
        Main.LocalPlayer.cursorItemIconText = string.Empty;
        Main.LocalPlayer.cursorItemIconPush = 0;
        Main.signHover = -1;
        Main.mouseText = false;
    }
}
