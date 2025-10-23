namespace AccessoriesPlus.Content;

public partial class AccessoryItem
{
    /* TODO: add back
        private static void ModifyShimmerCrafting()
        {
            // Shimmer transmutations
            // TODO: this will need to be reverted?
            if (!ServerConfig.Instance.ObtainabilityShimmer)
                return;

            // Hand of creation
            ItemID.Sets.ShimmerTransformToItem[ItemID.BrickLayer] = ItemID.ExtendoGrip;
            ItemID.Sets.ShimmerTransformToItem[ItemID.ExtendoGrip] = ItemID.PaintSprayer;
            ItemID.Sets.ShimmerTransformToItem[ItemID.PaintSprayer] = ItemID.PortableCementMixer;
            ItemID.Sets.ShimmerTransformToItem[ItemID.PortableCementMixer] = ItemID.BrickLayer;

            // Travelling merchant accessories
            ItemID.Sets.ShimmerTransformToItem[ItemID.Stopwatch] = ItemID.LifeformAnalyzer;
            ItemID.Sets.ShimmerTransformToItem[ItemID.LifeformAnalyzer] = ItemID.DPSMeter;
            ItemID.Sets.ShimmerTransformToItem[ItemID.DPSMeter] = ItemID.Stopwatch;

            // Shiny red balloon to balloon pufferfish
            ItemID.Sets.ShimmerTransformToItem[ItemID.ShinyRedBalloon] = ItemID.BalloonPufferfish;

            // Corruption and crimson counterparts
            ItemID.Sets.ShimmerTransformToItem[ItemID.PutridScent] = ItemID.FleshKnuckles;
            ItemID.Sets.ShimmerTransformToItem[ItemID.FleshKnuckles] = ItemID.PutridScent;

            ItemID.Sets.ShimmerTransformToItem[ItemID.BandofStarpower] = ItemID.PanicNecklace;
            ItemID.Sets.ShimmerTransformToItem[ItemID.PanicNecklace] = ItemID.BandofStarpower;

            ItemID.Sets.ShimmerTransformToItem[ItemID.WormScarf] = ItemID.BrainOfConfusion;
            ItemID.Sets.ShimmerTransformToItem[ItemID.BrainOfConfusion] = ItemID.WormScarf;
        }

        // Removing certain drops from presents
        private static void ModifyPresentLoot(Item item, ItemLoot itemLoot)
        {
            if (!ServerConfig.Instance.ObtainabilityPresents || item.type != ItemID.Present)
                return;

            foreach (var rule in itemLoot.Get())
            {
                if (rule is SequentialRulesNotScalingWithLuckRule drop)
                {
                    var modifiedRules = drop.rules.ToList();

                    // Toolbox
                    modifiedRules.Remove(modifiedRules.First(r => r is CommonDrop { itemId: ItemID.Toolbox }));
                    // Hand warmer
                    modifiedRules.Remove(modifiedRules.First(r => r is CommonDrop { itemId: ItemID.HandWarmer }));

                    drop.rules = modifiedRules.ToArray();
                }
            }
        }
        */
}
