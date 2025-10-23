using AccessoriesPlus.Utilities.StuffToMoveToTerraUtil;

namespace AccessoriesPlus.Content.RecipeGroups;

public class Gold : ModRecipeGroup
{
    public override List<int> ValidItems =>
    [
        ItemID.GoldBar,
        ItemID.PlatinumBar,
    ];
}
