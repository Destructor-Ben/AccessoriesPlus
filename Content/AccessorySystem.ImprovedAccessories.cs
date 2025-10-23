using AccessoriesPlus.Content.RecipeGroups;

namespace AccessoriesPlus.Content;

public partial class AccessorySystem
{
    // Adding recipes
    private static void AddImprovedRecipies()
    {
        // Hand of creation
        if (ServerConfig.Instance.ImprovedHandOfCreation)
        {
            Recipe.Create(ItemID.HandOfCreation)
                  .AddTile(TileID.TinkerersWorkbench)
                  .AddIngredient(ItemID.ArchitectGizmoPack)
                  .AddIngredient(ItemID.AncientChisel)
                  .AddIngredient(ItemID.TreasureMagnet)
                  .AddIngredient(ItemID.PortableStool)
                  .AddIngredient(ItemID.Toolbelt)
                  .AddIngredient(ItemID.Toolbox)
                  .Register();
        }

        // Terraspark boots
        if (ServerConfig.Instance.ImprovedTerrasparkBoots)
        {
            Recipe.Create(ItemID.TerrasparkBoots)
                  .AddTile(TileID.TinkerersWorkbench)
                  .AddIngredient(ItemID.FrostsparkBoots)
                  .AddIngredient(ItemID.LavaWaders)
                  .AddIngredient(ItemID.AmphibianBoots)
                  .Register();
        }

        // Ankh shield
        if (ServerConfig.Instance.ImprovedAnkhShield)
        {
            Recipe.Create(ItemID.AnkhShield)
                  .AddTile(TileID.TinkerersWorkbench)
                  .AddIngredient(ItemID.AnkhCharm)
                  .AddIngredient(ItemID.ObsidianShield)
                  .AddIngredient(ItemID.FrozenShield)
                  .AddIngredient(ItemID.HeroShield)
                  .DisableDecraft()
                  .Register();

            Recipe.Create(ItemID.AnkhShield)
                  .AddTile(TileID.TinkerersWorkbench)
                  .AddIngredient(ItemID.AnkhCharm)
                  .AddIngredient(ItemID.ObsidianShield)
                  .AddIngredient(ItemID.FrozenTurtleShell)
                  .AddIngredient(ItemID.HeroShield)
                  .Register();

            Recipe.Create(ItemID.AnkhShield)
                  .AddTile(TileID.TinkerersWorkbench)
                  .AddIngredient(ItemID.AnkhCharm)
                  .AddIngredient(ItemID.ObsidianShield)
                  .AddIngredient(ItemID.FrozenShield)
                  .AddIngredient(ItemID.FleshKnuckles)
                  .DisableDecraft()
                  .Register();

            Recipe.Create(ItemID.AnkhCharm)
                  .AddTile(TileID.TinkerersWorkbench)
                  .AddIngredient(ItemID.ArmorBracing)
                  .AddIngredient(ItemID.MedicatedBandage)
                  .AddIngredient(ItemID.ThePlan)
                  .AddIngredient(ItemID.CountercurseMantra)
                  .AddIngredient(ItemID.ReflectiveShades)
                  .AddIngredient(ItemID.HandWarmer)
                  .Register();
        }

        // Bundle of horseshoe balloons
        if (ServerConfig.Instance.ImprovedHorseshoeBundle)
        {
            Recipe.Create(ItemID.HorseshoeBundle)
                  .AddTile(TileID.TinkerersWorkbench)
                  .AddIngredient(ItemID.BundleofBalloons)
                  .AddIngredient(ItemID.LuckyHorseshoe)
                  .Register();

            Recipe.Create(ItemID.BundleofBalloons)
                  .AddTile(TileID.TinkerersWorkbench)
                  .AddRecipeGroup(RecipeGroupID.CloudBalloons)
                  .AddRecipeGroup(RecipeGroupID.BlizzardBalloons)
                  .AddRecipeGroup(RecipeGroupID.SandstormBalloons)
                  .AddRecipeGroup<FartBalloons>()
                  .AddRecipeGroup<SharkronBalloons>()
                  .Register();
        }
    }

    // Removing recipes not from this mod
    private static void RemoveImprovedRecipies()
    {
        // Hand of creation
        if (ServerConfig.Instance.ImprovedHandOfCreation)
            Util.RemoveRecipesForItem(ItemID.HandOfCreation);

        // Terraspark boots
        if (ServerConfig.Instance.ImprovedTerrasparkBoots)
            Util.RemoveRecipesForItem(ItemID.TerrasparkBoots);

        // Ankh shield
        if (ServerConfig.Instance.ImprovedAnkhShield)
        {
            Util.RemoveRecipesForItem(ItemID.AnkhShield);
            Util.RemoveRecipesForItem(ItemID.AnkhCharm);
        }

        // Bundle of horseshoe balloons
        if (ServerConfig.Instance.ImprovedHorseshoeBundle)
        {
            Util.RemoveRecipesForItem(ItemID.BundleofBalloons);
            Util.RemoveRecipesForItem(ItemID.HorseshoeBundle);
        }
    }
}
