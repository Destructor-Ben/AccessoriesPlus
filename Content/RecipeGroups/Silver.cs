using AccessoriesPlus.Utilities.StuffToMoveToTerraUtil;

namespace AccessoriesPlus.Content.RecipeGroups;

public class Silver : ModRecipeGroup
{
    public override List<int> ValidItems =>
    [
        ItemID.SilverBar,
        ItemID.TungstenBar,
    ];
}
