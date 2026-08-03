using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader.UI;
using Terraria.UI.Chat;

namespace AccessoriesPlus.Config.Elements;

public class CustomObjectElement : ObjectElement
{
    private string ID => MemberInfo.Name;

    public override void OnBind()
    {
        base.OnBind();

        // Shrink child elements
        dataList.Width.Pixels *= 2;

        // Undo the weird additions to the text display function
        TextDisplayFunction = () => Label;

        // Shift expand button over
        expandButton.Left.Set(-4, 0f);
        expandButton.HAlign = 1f;

        // Set expanded
        expanded = CustomObjectElementSystem.Instance.GetExpandedOrDefault(ID);

        expandButton.OnLeftClick += (_, _) =>
        {
            CustomObjectElementSystem.Instance.ExpandedState[ID] = !CustomObjectElementSystem.Instance.ExpandedState[ID];
        };

        Append(expandButton); // Stops flicker, will get removed a frame later anyway
        RemoveChild(dataList);

        if (expanded)
        {
            Append(dataList);

            expandButton.HoverText = Language.GetTextValue("tModLoader.ModConfigCollapse");
            expandButton.SetImage(ExpandedTexture);
        }
        else
        {
            expandButton.HoverText = Language.GetTextValue("tModLoader.ModConfigExpand");
            expandButton.SetImage(CollapsedTexture);
        }
    }

    // Copied and modified from ConfigElement
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        var dimensions = base.GetDimensions();
        float settingsWidth = dimensions.Width + 1f;
        var vector = new Vector2(dimensions.X, dimensions.Y);
        var baseScale = new Vector2(0.8f);
        var color = Color.White;

        if (!MemberInfo.CanWrite)
            color = Color.Gray;

        // Modified: Changed the pan
        var panelColor = IsMouseHovering ? backgroundColor : backgroundColor.MultiplyRGBA(new Color(180, 180, 180));
        if (expanded)
            panelColor = UICommon.MainPanelBackground;

        var position = vector;

        if (Flashing) {
            float ratio = Utils.Turn01ToCyclic010(((Interface.modConfig.UpdateCount % flashRate) / (float)flashRate)) * 0.5f + 0.5f;
            panelColor = Color.Lerp(panelColor, Color.White, MathF.Pow(ratio, 2));
        }

        DrawPanel2(spriteBatch, position, TextureAssets.SettingsPanel.Value, settingsWidth, dimensions.Height, panelColor);

        if (DrawLabel) {
            position.X += 8f;
            position.Y += 8f;

            string label = TextDisplayFunction();
            if (ReloadRequired && ValueChanged) {
                label += " - [c/FF0000:" + Language.GetTextValue("tModLoader.ModReloadRequired") + "]";
            }

            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, label, position, color, 0f, Vector2.Zero, baseScale, settingsWidth, 2f);
        }

        if (!IsMouseHovering || TooltipFunction == null)
            return;

        string tooltip = TooltipFunction();

        if (ShowReloadRequiredTooltip) {
            tooltip += string.IsNullOrEmpty(tooltip) ? "" : "\n";
            tooltip += $"[c/{Color.Orange.Hex3()}:" + Language.GetTextValue("tModLoader.ModReloadRequiredMemberTooltip") + "]";
        }

        UIModConfig.Tooltip = tooltip;
    }
}

public class CustomObjectElementSystem : ModSystem
{
    public static CustomObjectElementSystem Instance => ModContent.GetInstance<CustomObjectElementSystem>();

    // Maps a string id of a CustomObjectElement to it's expanded state
    // This is to allow the expanded state to persist across DoMenuModeState
    // Can't be bothered to reset the expanded state on enter/exit config, not really worth it anyway
    public Dictionary<string, bool> ExpandedState = new();

    public bool GetExpandedOrDefault(string id)
    {
        ExpandedState.TryAdd(id, false);
        return ExpandedState[id];
    }
}
