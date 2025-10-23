namespace AccessoriesPlus.Content;

public partial class AccessoryItem
{
    private static Asset<Texture2D> OriginalBOBTexture;
    private static Asset<Texture2D> OriginalBOBBalloonTexture;
    private static Asset<Texture2D> OriginalBOHBTexture;
    private static Asset<Texture2D> OriginalBOHBBalloonTexture;

    private static void ReplaceBOBTexture()
    {
        OriginalBOBTexture = TextureAssets.Item[ItemID.BundleofBalloons];
        OriginalBOBBalloonTexture = TextureAssets.Item[ItemID.HorseshoeBundle];
        OriginalBOHBTexture = TextureAssets.AccBalloon[ArmorIDs.Balloon.BundleofBalloons];
        OriginalBOHBBalloonTexture = TextureAssets.AccBalloon[ArmorIDs.Balloon.HorseshoeBundle];

        if (ServerConfig.Instance.ImprovedHorseshoeBundle)
        {
            TextureAssets.Item[ItemID.BundleofBalloons] = Util.GetTexture("Items/BundleofBalloons");
            TextureAssets.Item[ItemID.HorseshoeBundle] = Util.GetTexture("Items/BundleofHorseshoeBalloons");
            TextureAssets.AccBalloon[ArmorIDs.Balloon.BundleofBalloons] = Util.GetTexture("Items/BundleofBalloons_Balloon");
            TextureAssets.AccBalloon[ArmorIDs.Balloon.HorseshoeBundle] = Util.GetTexture("Items/BundleofHorseshoeBalloons_Balloon");
        }
    }

    private static void UndoReplaceBOBTexture()
    {
        TextureAssets.Item[ItemID.BundleofBalloons] = OriginalBOBTexture;
        TextureAssets.Item[ItemID.HorseshoeBundle] = OriginalBOBBalloonTexture;
        TextureAssets.AccBalloon[ArmorIDs.Balloon.BundleofBalloons] = OriginalBOHBTexture;
        TextureAssets.AccBalloon[ArmorIDs.Balloon.HorseshoeBundle] = OriginalBOHBBalloonTexture;

        OriginalBOBTexture = null;
        OriginalBOBBalloonTexture = null;
        OriginalBOHBTexture = null;
        OriginalBOHBBalloonTexture = null;
    }

    private static void ModifyAccessoryStats(Item item)
    {
        switch (item.type)
        {
            case ItemID.AnkhShield:
                if (ServerConfig.Instance.ImprovedAnkhShield)
                {
                    item.StatsModifiedBy.Add(AccessoriesPlusMod.Instance);
                    item.defense = 12;
                }

                break;
            default:
                break;
        }
    }

    private static void ModifyAccessoryEffects(Item item, Player player, bool hideVisual)
    {
        switch (item.type)
        {
            case ItemID.HandOfCreation:
                // Hand of creation
                if (ServerConfig.Instance.ImprovedHandOfCreation)
                {
                    player.CopyVanillaEquipEffects(ItemID.Toolbelt, hideVisual);
                    player.CopyVanillaEquipEffects(ItemID.Toolbox, hideVisual);
                }

                break;
            case ItemID.TerrasparkBoots:
                // Terraspark boots
                if (ServerConfig.Instance.ImprovedTerrasparkBoots)
                {
                    player.CopyVanillaEquipEffects(ItemID.AmphibianBoots, hideVisual);
                    // Amphibian boots override this
                    player.accRunSpeed = 6.75f;
                }

                break;
            case ItemID.AnkhShield:
                // Ankh shield
                if (ServerConfig.Instance.ImprovedAnkhShield)
                {
                    player.CopyVanillaEquipEffects(ItemID.HandWarmer, hideVisual);
                    player.CopyVanillaEquipEffects(ItemID.HeroShield, hideVisual);
                    player.CopyVanillaEquipEffects(ItemID.FrozenShield, hideVisual);
                }

                break;
            case ItemID.AnkhCharm:
                // Ankh charm
                if (ServerConfig.Instance.ImprovedAnkhShield)
                    player.CopyVanillaEquipEffects(ItemID.HandWarmer, hideVisual);

                break;
            case ItemID.BundleofBalloons:
            case ItemID.HorseshoeBundle:
                // Bundle of horseshoe balloons
                if (ServerConfig.Instance.ImprovedHorseshoeBundle)
                {
                    player.CopyVanillaEquipEffects(ItemID.FartInABalloon, hideVisual);
                    player.CopyVanillaEquipEffects(ItemID.SharkronBalloon, hideVisual);
                }

                break;
            default:
                break;
        }
    }

    private static void ModifyAccessoryTooltips(Item item, List<TooltipLine> tooltips)
    {
        switch (item.type)
        {
            case ItemID.HandOfCreation:
                // Hand of creation
                if (ServerConfig.Instance.ImprovedHandOfCreation)
                {
                    tooltips.InsertTooltips("Tooltip2", after: true, Util.GetTooltipLine("HandOfCreation0"), Util.GetTooltipLine("HandOfCreation1"));
                    tooltips.RemoveTooltips("Tooltip2");
                }

                break;
            case ItemID.TerrasparkBoots:
                // Terraspark boots
                if (ServerConfig.Instance.ImprovedTerrasparkBoots)
                    tooltips.InsertTooltips("Tooltip4", after: true, Util.GetTooltipLine("TerrasparkBoots0"), Util.GetTooltipLine("TerrasparkBoots1"));

                break;
            case ItemID.AnkhShield:
                // Ankh shield
                if (ServerConfig.Instance.ImprovedAnkhShield)
                    tooltips.InsertTooltips("Tooltip1", after: true, Util.GetTooltipLine("AnkhShield0"), Util.GetTooltipLine("AnkhShield1"), Util.GetTooltipLine("AnkhShield2"));

                break;
            case ItemID.BundleofBalloons:
            case ItemID.HorseshoeBundle:
                // Bundle of horseshoe balloons
                if (ServerConfig.Instance.ImprovedHorseshoeBundle)
                {
                    tooltips.InsertTooltips("Tooltip0", after: true, Util.GetTooltipLine("BundleOfBalloons0"));
                    tooltips.RemoveTooltips("Tooltip0");
                }

                break;
            case ItemID.Radar:
                if (PDAConfig.Instance.RadarHighlightDanger)
                {
                    tooltips.InsertTooltips("Tooltip0", after: true, Util.GetTooltipLine("Radar0"));
                    tooltips.RemoveTooltips("Tooltip0");
                }

                break;
            default:
                break;
        }
    }
}
