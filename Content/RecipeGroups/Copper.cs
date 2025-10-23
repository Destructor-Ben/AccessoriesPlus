using AccessoriesPlus.Utilities.StuffToMoveToTerraUtil;

namespace AccessoriesPlus.Content.RecipeGroups;

public class Copper : ModRecipeGroup
{
    public override List<int> ValidItems =>
    [
        ItemID.CopperBar,
        ItemID.TinBar,
    ];
}
