using Terraria.Map;

namespace AccessoriesPlus.Content;

public partial class AccessoryInfoDisplay : GlobalInfoDisplay
{
    private static Dictionary<int, (short, bool)> ModifiedTiles;

    // TODO: increasing priorities for gems
    private const short GemPriority = 235;
    private const short HellstonePriority = 450;

    private static void LoadMetalDetector()
    {
        ModifiedTiles = new();
    }

    private static void UnloadMetalDetector()
    {
        // Resetting tileOreFinderPriority values
        if (ModifiedTiles is not null)
        {
            foreach (var tile in ModifiedTiles)
            {
                int type = tile.Key;
                short priority = tile.Value.Item1;
                bool spelunkable = tile.Value.Item2;

                Main.tileOreFinderPriority[type] = priority;
                Main.tileSpelunker[type] = spelunkable;
            }
        }

        ModifiedTiles = null;
    }

    private static void SetSpelunkableTiles()
    {
        if (PDAConfig.Instance.TrackHellstone)
            MakeTileSpelunkable(TileID.Hellstone, HellstonePriority);

        if (PDAConfig.Instance.TrackGems)
        {
            MakeTileSpelunkable(TileID.ExposedGems, GemPriority);

            MakeTileSpelunkable(TileID.Amethyst, GemPriority);
            MakeTileSpelunkable(TileID.Topaz, GemPriority);
            MakeTileSpelunkable(TileID.Sapphire, GemPriority);
            MakeTileSpelunkable(TileID.Emerald, GemPriority);
            MakeTileSpelunkable(TileID.Ruby, GemPriority);
            MakeTileSpelunkable(TileID.Diamond, GemPriority);
            MakeTileSpelunkable(TileID.AmberStoneBlock, GemPriority);

            MakeTileSpelunkable(TileID.TreeAmethyst, GemPriority);
            MakeTileSpelunkable(TileID.TreeTopaz, GemPriority);
            MakeTileSpelunkable(TileID.TreeSapphire, GemPriority);
            MakeTileSpelunkable(TileID.TreeEmerald, GemPriority);
            MakeTileSpelunkable(TileID.TreeRuby, GemPriority);
            MakeTileSpelunkable(TileID.TreeDiamond, GemPriority);
            MakeTileSpelunkable(TileID.TreeAmber, GemPriority);
        }

        static void MakeTileSpelunkable(int type, short priority)
        {
            // Storing original values
            short metalDetectorValue = Main.tileOreFinderPriority[type];
            bool isSpelunkable = Main.tileSpelunker[type];
            ModifiedTiles.Add(type, (metalDetectorValue, isSpelunkable));

            // Modifying values
            Main.tileOreFinderPriority[type] = priority;
            Main.tileSpelunker[type] = true;
        }
    }

    private static void ModifyMetalDetector(InfoDisplay currentDisplay, ref string displayValue, ref Color displayColor, ref Color displayShadowColor)
    {
        if (currentDisplay != InfoDisplay.MetalDetector || Main.SceneMetrics.bestOre <= 0)
            return;

        // Changing display value
        // TODO: search similar to lifeform analyzer
        if (PDAConfig.Instance.MetalDetectorDistanceInfo)
        {
            string tileName = GetTileName(Main.SceneMetrics.bestOre, Main.SceneMetrics.ClosestOrePosition);
            int distance = (int)Util.Round(Main.SceneMetrics.ClosestOrePosition.Value.ToWorldCoordinates().Distance(Main.LocalPlayer.Center) / 16f);
            displayValue = Util.GetTextValue("InfoDisplays.FoundTreasure", tileName, distance);
        }
    }

    public static string GetTileName(int type, Point? position)
    {
        int baseOption = 0;
        int tileType = type;
        if (position.HasValue)
        {
            var value = position.Value;
            var tileSafely = Framing.GetTileSafely(value);
            if (tileSafely.HasTile)
            {
                MapHelper.GetTileBaseOption(value.X, value.Y, tileSafely.TileType, tileSafely, ref baseOption);
                tileType = tileSafely.TileType;
                if (TileID.Sets.BasicChest[tileType] || TileID.Sets.BasicChestFake[tileType])
                    baseOption = 0;
            }
        }

        string name = Lang.GetMapObjectName(MapHelper.TileToLookup(tileType, baseOption));

        if (tileType is TileID.Hellstone)
            name = Lang.GetItemNameValue(ItemID.Hellstone);

        if (TileID.Sets.CountsAsGemTree[tileType])
            name = Util.GetTextValue("InfoDisplays.GemTree");

        if (string.IsNullOrEmpty(name))
            name = TileID.Search.GetName(tileType);

        return name;
    }
}
