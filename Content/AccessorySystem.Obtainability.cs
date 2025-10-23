namespace AccessoriesPlus.Content;

public partial class AccessorySystem
{
    /* TODO: add back
        // Adding recipes
        private static void AddObtainabilityRecipes()
        {
            // Hand warmer recipe
            if (ServerConfig.Instance.ObtainabilityPresents)
            {
                Recipe.Create(ItemID.HandWarmer)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.Silk, 10)
                      .AddIngredient(ItemID.FlinxFur, 5)
                      .Register();
            }

            // More accessory recipes
            if (ServerConfig.Instance.ObtainabilityRecipes)
            {
                #region Hand of Creation

                Recipe.Create(ItemID.PortableStool)
                      .AddTile(TileID.Sawmill)
                      .AddRecipeGroup(RecipeGroupID.Wood, 15)
                      .Register();

                Recipe.Create(ItemID.AncientChisel)
                      .AddTile(TileID.Anvils)
                      .AddIngredient(ItemID.Sandstone, 5)
                      .AddRecipeGroup<Silver>(5)
                      .AddIngredient(ItemID.Bone, 5)
                      .Register();

                #endregion

                #region PDA

                Recipe.Create(ItemID.MetalDetector)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.Wire, 10)
                      .AddRecipeGroup(RecipeGroupID.IronBar, 5)
                      .AddIngredient(ItemID.SpelunkerGlowstick)
                      .Register();

                Recipe.Create(ItemID.Compass)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddRecipeGroup(RecipeGroupID.IronBar, 5)
                      .AddRecipeGroup<Copper>(5)
                      .Register();

                Recipe.Create(ItemID.DepthMeter)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.Wire, 10)
                      .AddRecipeGroup(RecipeGroupID.IronBar, 5)
                      .Register();

                Recipe.Create(ItemID.Radar)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.Wire, 10)
                      .AddRecipeGroup(RecipeGroupID.IronBar, 5)
                      .Register();

                Recipe.Create(ItemID.TallyCounter)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.Wire, 10)
                      .AddRecipeGroup(RecipeGroupID.IronBar, 5)
                      .Register();

                #endregion

                #region Terraspark Boots

                Recipe.Create(ItemID.HermesBoots)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.Silk, 10)
                      .AddIngredient(ItemID.Leather, 5)
                      .AddIngredient(ItemID.SwiftnessPotion)
                      .Register();

                Recipe.Create(ItemID.FlurryBoots)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.IceBlock, 10)
                      .AddIngredient(ItemID.HermesBoots)
                      .Register();

                Recipe.Create(ItemID.SandBoots)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.SandBlock, 10)
                      .AddIngredient(ItemID.HermesBoots)
                      .Register();

                Recipe.Create(ItemID.SailfishBoots)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.Bass, 2)
                      .AddIngredient(ItemID.HermesBoots)
                      .Register();

                Recipe.Create(ItemID.IceSkates)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.FlurryBoots)
                      .AddRecipeGroup(RecipeGroupID.IronBar, 5)
                      .Register();

                Recipe.Create(ItemID.WaterWalkingBoots)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.SailfishBoots)
                      .AddIngredient(ItemID.WaterWalkingPotion)
                      .Register();

                Recipe.Create(ItemID.Aglet)
                      .AddTile(TileID.Anvils)
                      .AddRecipeGroup<Copper>(5)
                      .Register();

                Recipe.Create(ItemID.AnkletoftheWind)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.JungleSpores, 7)
                      .AddIngredient(ItemID.Cloud, 10)
                      .AddIngredient(ItemID.PinkGel, 5)
                      .Register();

                Recipe.Create(ItemID.FrogLeg)
                      .AddIngredient(ItemID.Frog)
                      .Register();

                Recipe.Create(ItemID.FrogLeg)
                      .AddIngredient(ItemID.GoldFrog)
                      .Register();

                Recipe.Create(ItemID.ObsidianRose)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.Obsidian, 10)
                      .AddIngredient(ItemID.HellstoneBar, 10)
                      .AddIngredient(ItemID.Fireblossom, 3)
                      .Register();

                Recipe.Create(ItemID.LavaCharm)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.HellstoneBar, 10)
                      .AddIngredient(ItemID.LavaBucket)
                      .Register();

                Recipe.Create(ItemID.Leather)
                      .AddTile(TileID.WorkBenches)
                      .AddIngredient(ItemID.RottenChunk, 2)
                      .Register();

                Recipe.Create(ItemID.Leather)
                      .AddTile(TileID.WorkBenches)
                      .AddIngredient(ItemID.Vertebrae, 2)
                      .Register();

                #endregion

                #region Ankh Shield

                Recipe.Create(ItemID.CobaltShield)
                      .AddTile(TileID.Anvils)
                      .AddRecipeGroup<Cobalt>(10)
                      .Register();

                Recipe.Create(ItemID.FrozenTurtleShell)
                      .AddTile(TileID.CrystalBall)
                      .AddIngredient(ItemID.TurtleShell)
                      .AddIngredient(ItemID.IceBlock, 10)
                      .Register();

                #endregion

                #region Bundle of Horseshoe Balloons

                Recipe.Create(ItemID.LuckyHorseshoe)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddRecipeGroup<Gold>(8)
                      .AddIngredient(ItemID.Cloud, 10)
                      .Register();

                Recipe.Create(ItemID.ShinyRedBalloon)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.WhiteString)
                      .AddIngredient(ItemID.Cloud, 10)
                      .AddIngredient(ItemID.Gel, 10)
                      .Register();

                Recipe.Create(ItemID.CloudinaBottle)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.Bottle)
                      .AddIngredient(ItemID.Cloud, 10)
                      .AddIngredient(ItemID.Feather, 5)
                      .Register();

                Recipe.Create(ItemID.TsunamiInABottle)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.CloudinaBottle)
                      .AddIngredient(ItemID.BottledWater)
                      .AddIngredient(ItemID.Feather, 5)
                      .Register();

                Recipe.Create(ItemID.BlizzardinaBottle)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.CloudinaBottle)
                      .AddIngredient(ItemID.SnowBlock, 10)
                      .AddIngredient(ItemID.Feather, 5)
                      .Register();

                Recipe.Create(ItemID.SandstorminaBottle)
                      .AddTile(TileID.TinkerersWorkbench)
                      .AddIngredient(ItemID.CloudinaBottle)
                      .AddIngredient(ItemID.SandBlock, 10)
                      .AddIngredient(ItemID.Feather, 5)
                      .Register();

                #endregion
            }
        }

        // Removing existing recipes
        private static void RemoveObtainabilityRecipes()
        {
            // More accessory recipes
            if (ServerConfig.Instance.ObtainabilityRecipes)
            {
                Util.RemoveRecipesForItem(ItemID.BlizzardinaBottle);
                Util.RemoveRecipesForItem(ItemID.SandstorminaBottle);
                Util.RemoveRecipesForItem(ItemID.Leather);
            }
        }
        */
}
