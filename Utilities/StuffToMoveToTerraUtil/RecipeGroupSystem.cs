namespace AccessoriesPlus.Utilities.StuffToMoveToTerraUtil;

public class RecipeGroupSystem : TerraUtilLoader<ModRecipeGroup>
{
    public override void AddRecipeGroups()
    {
        foreach (var modGroup in Content)
        {
            var group = new RecipeGroup(() => Mod.GetLocalization($"RecipeGroups.{modGroup.Name}").Value, modGroup.ValidItems.ToArray()) { IconicItemId = modGroup.ItemIconID };

            RecipeGroup.RegisterGroup(Mod.Name + ":" + modGroup.Name, group);
            modGroup.Group = group;
        }
    }
}
