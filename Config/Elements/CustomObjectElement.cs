using Terraria.ModLoader.Config.UI;

namespace AccessoriesPlus.Config.Elements;

// TODO: use a hack to keep the expanded state when saving/reloading
public class CustomObjectElement : ObjectElement
{
    public override void OnBind()
    {
        base.OnBind();

        // Undo the weird additions to the text display function
        TextDisplayFunction = () => Label;

        // Shift expand button over
        expandButton.Left.Set(-4, 0f);
        expandButton.HAlign = 1f;

        // Set expanded to false by default
        expanded = false;
        RemoveChild(dataList);
        expandButton.HoverText = Language.GetTextValue("tModLoader.ModConfigExpand");
        expandButton.SetImage(CollapsedTexture);
    }
}
