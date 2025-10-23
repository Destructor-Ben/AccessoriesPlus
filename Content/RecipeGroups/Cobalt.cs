using TerraUtil.RecipeGroups;

namespace AccessoriesPlus.Content.RecipeGroups;

public class Cobalt : ModRecipeGroup
{
    public override List<int> ValidItems => [ItemID.CobaltBar, ItemID.PalladiumBar];
}
