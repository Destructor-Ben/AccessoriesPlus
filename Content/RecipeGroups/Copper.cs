using TerraUtil.RecipeGroups;

namespace AccessoriesPlus.Content.RecipeGroups;

public class Copper : ModRecipeGroup
{
    public override List<int> ValidItems => [ItemID.CopperBar, ItemID.TinBar];
}
