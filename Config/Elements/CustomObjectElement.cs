using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader.UI;

namespace AccessoriesPlus.Config.Elements;

public class CustomObjectElement : ObjectElement
{
    private string ID => MemberInfo.Name;

    public override void OnBind()
    {
        base.OnBind();

        // Custom background color
        backgroundColor = Color.Lerp(UICommon.MainPanelBackground, UICommon.DefaultUIBlue, 0.25f);

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
