using AccessoriesPlus.Content.InfoDisplays;
using AccessoriesPlus.Utilities;
using AccessoriesPlus.Utilities.StuffToMoveToTerraUtil;
using Terraria.UI;
using Terraria.UI.Chat;
using PDAConfig = AccessoriesPlus.Config.SubConfigs.PDAConfig;

namespace AccessoriesPlus.Content.UI;

public class UIPDA : Interface
{
    public static UIPDA Instance => ModContent.GetInstance<UIPDA>();

    public override InterfaceScaleType ScaleType => InterfaceScaleType.Game; // TODO: remove this once I fix the UI scaling

    private const float MaxArrowDistanceFromPlayer = 300f;

    // TODO: move this to config
    private static Dictionary<int, int> TileToItem = new()
    {
        // Vanilla
        [TileID.FossilOre] = ItemID.FossilOre,
        [TileID.DesertFossil] = ItemID.DesertFossil,
        [TileID.Copper] = ItemID.CopperOre,
        [TileID.Tin] = ItemID.TinOre,
        [TileID.Iron] = ItemID.IronOre,
        [TileID.Lead] = ItemID.LeadOre,
        [TileID.Silver] = ItemID.SilverOre,
        [TileID.Tungsten] = ItemID.TungstenOre,
        [TileID.Gold] = ItemID.GoldOre,
        [TileID.Platinum] = ItemID.PlatinumOre,
        [TileID.Demonite] = ItemID.DemoniteOre,
        [TileID.Crimtane] = ItemID.CrimtaneOre,
        [TileID.Meteorite] = ItemID.Meteorite,
        [TileID.Heart] = ItemID.LifeCrystal,
        [TileID.LifeCrystalBoulder] = ItemID.LifeCrystal,
        [TileID.ManaCrystal] = ItemID.ManaCrystal,
        [TileID.Cobalt] = ItemID.CobaltOre,
        [TileID.Palladium] = ItemID.PalladiumOre,
        [TileID.Mythril] = ItemID.MythrilOre,
        [TileID.Orichalcum] = ItemID.OrichalcumOre,
        [TileID.Adamantite] = ItemID.AdamantiteOre,
        [TileID.Titanium] = ItemID.TitaniumOre,
        [TileID.Chlorophyte] = ItemID.ChlorophyteOre,
        [TileID.GlowTulip] = ItemID.GlowTulip,
        [TileID.LifeFruit] = ItemID.LifeFruit,

        // Ones that I add
        [TileID.Hellstone] = ItemID.Hellstone,
        [TileID.Amethyst] = ItemID.Amethyst,
        [TileID.Topaz] = ItemID.Topaz,
        [TileID.Sapphire] = ItemID.Sapphire,
        [TileID.Emerald] = ItemID.Emerald,
        [TileID.Ruby] = ItemID.Ruby,
        [TileID.Diamond] = ItemID.Diamond,
        [TileID.AmberStoneBlock] = ItemID.Amber,
        [TileID.TreeAmethyst] = ItemID.GemTreeAmethystSeed,
        [TileID.TreeTopaz] = ItemID.GemTreeTopazSeed,
        [TileID.TreeSapphire] = ItemID.GemTreeSapphireSeed,
        [TileID.TreeEmerald] = ItemID.GemTreeEmeraldSeed,
        [TileID.TreeRuby] = ItemID.GemTreeRubySeed,
        [TileID.TreeDiamond] = ItemID.GemTreeDiamondSeed,
        [TileID.TreeAmber] = ItemID.GemTreeAmberSeed,
    };

    /*
    // TODO: reimplement this?
    // This isn't safe, but shouldn't matter so long as beehives and other tiles aren't detected by the metal detector
    private static MethodInfo ItemDropQueryMethod = typeof(WorldGen).GetMethod("KillTile_GetItemDrops", BindingFlags.NonPublic | BindingFlags.Static);

    private static int GetPrimaryTileDrop(int x, int y)
    {
        object[] parameters = new object[] { x, y, Main.tile[x, y], null, null, null, null, true };
        ItemDropQueryMethod.Invoke(null, parameters);
        return (int)parameters[3];
    }
    */

    public override int GetLayerInsertIndex(List<GameInterfaceLayer> layers)
    {
        return layers.FindIndex(l => l.Name == "Vanilla: Entity Health Bars") + 1;
    }

    // TODO: make UI elememnts for the arrows and rework them
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        // Lifeform analyzer
        if (PDAConfig.Instance.LifeformAnalyzerArrows && InfoDisplayUtils.IsActive(InfoDisplay.LifeformAnalyzer))
        {
            foreach (var npc in ModContent.GetInstance<LifeformAnalyzerTweaks>().LifeformAnalyzerNPCs)
            {
                if (npc.active)
                    DrawNPCArrow(spriteBatch, npc);
            }
        }

        // Metal detector
        // TODO: better system for blobbing framed tiles together, and also making frameImportant tiles have their position centered
        if (PDAConfig.Instance.MetalDetectorArrows && InfoDisplayUtils.IsActive(InfoDisplay.MetalDetector))
        {
            var trackedTiles = new List<(Tile, Point)>();

            // Looping over the tiles
            for (int x = Main.LocalPlayer.Center.ToTileCoordinates().X - Main.buffScanAreaWidth / 2; x < Main.LocalPlayer.Center.ToTileCoordinates().X + Main.buffScanAreaWidth / 2; x++)
            {
                for (int y = Main.LocalPlayer.Center.ToTileCoordinates().Y - Main.buffScanAreaHeight / 2; y < Main.LocalPlayer.Center.ToTileCoordinates().Y + Main.buffScanAreaHeight / 2; y++)
                {
                    // Bounds check
                    if (x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
                        continue;

                    var tile = Main.tile[x, y];
                    if (SceneMetrics.IsValidForOreFinder(tile))
                    {
                        // Adding the tile if there isn't one already
                        if (!trackedTiles.Where(t => t.Item1.TileType == tile.TileType).Any())
                        {
                            trackedTiles.Add((tile, new Point(x, y)));
                            continue;
                        }

                        // Removing the exitsing tile if this one is closer
                        var tilesToRemove = new List<(Tile, Point)>();
                        var tilesToAdd = new List<(Tile, Point)>();

                        foreach (var trackedTile in trackedTiles)
                        {
                            if (trackedTile.Item1.TileType == tile.TileType && trackedTile.Item2.ToWorldCoordinates().Distance(Main.LocalPlayer.Center) >= new Point(x, y).ToWorldCoordinates().Distance(Main.LocalPlayer.Center))
                            {
                                tilesToRemove.Add(trackedTile);
                                tilesToAdd.Add((tile, new Point(x, y)));
                            }
                        }

                        // Actually doing the removing and adding
                        foreach (var tileToAdd in tilesToAdd)
                            trackedTiles.Add(tileToAdd);

                        foreach (var tileToRemove in tilesToRemove)
                            trackedTiles.Remove(tileToRemove);
                    }
                }
            }

            // Drawing
            foreach (var tile in trackedTiles)
                DrawTileArrow(spriteBatch, tile.Item1, tile.Item2.X, tile.Item2.Y);
        }
    }

    // Gets the preview texture for a tile
    private static Asset<Texture2D> GetTileTexture(Tile tile)
    {
        int type = tile.TileType;
        Asset<Texture2D> tex = null;

        // Basic tiles
        int itemID = TileToItem.GetValueOrDefault(type, -1);

        // Chests
        int chestStyle = tile.TileFrameX / 36;
        if (type is TileID.Containers or TileID.FakeContainers)
            itemID = Chest.chestItemSpawn[chestStyle];
        else if (type is TileID.Containers2 or TileID.FakeContainers)
            itemID = Chest.chestItemSpawn2[chestStyle];

        // Gelatin crystal
        if (type == TileID.Crystals && tile.TileFrameX >= 324)
            itemID = ItemID.QueenSlimeCrystal;

        // Strange plants
        if (type == TileID.DyePlants)
        {
            int plantStyle = tile.TileFrameX / 34;
            itemID = 1107 + plantStyle;
            if (plantStyle is >= 8 and <= 11)
                itemID = 3385 + plantStyle - 8;
        }

        // Exposed gems
        if (type == TileID.ExposedGems)
        {
            switch (tile.TileFrameX / 18)
            {
                case 0:
                    itemID = ItemID.Amethyst;
                    break;
                case 1:
                    itemID = ItemID.Topaz;
                    break;
                case 2:
                    itemID = ItemID.Sapphire;
                    break;
                case 3:
                    itemID = ItemID.Emerald;
                    break;
                case 4:
                    itemID = ItemID.Ruby;
                    break;
                case 5:
                    itemID = ItemID.Diamond;
                    break;
                case 6:
                    itemID = ItemID.Amber;
                    break;
            }
        }

        // TODO: Modded tiles

        if (tex == null && itemID != -1)
        {
            Main.instance.LoadItem(itemID);
            tex = TextureAssets.Item[itemID];
        }

        return tex ?? TextureAssets.NpcHead[0];
    }

    // Draws an arrow to the npc
    private static void DrawNPCArrow(SpriteBatch sb, NPC target)
    {
        // Getting position
        (var position, float rotation) = GetArrowPositionAndRotation(target.Center);
        var npc = new NPC();
        npc.SetDefaults(target.type);
        npc.IsABestiaryIconDummy = true;
        npc.Center = position;

        // Drawing the background and arrow
        DrawArrow(sb, position, rotation, npc.GivenOrTypeName, target.Center);

        // Drawing an NPC
        Main.instance.DrawNPCDirect(sb, npc, false, Vector2.Zero);
    }

    // Draws an arrow to the tile
    private static void DrawTileArrow(SpriteBatch sb, Tile target, int x, int y)
    {
        // Getting data
        (var position, float rotation) = GetArrowPositionAndRotation(new Point(x, y).ToWorldCoordinates());
        var tex = GetTileTexture(target);

        // Drawing the background and arrow
        string name = MetalDetectorTweaks.GetTileName(target.TileType, new Point(x, y));
        DrawArrow(sb, position, rotation, name, new Point(x, y).ToWorldCoordinates());

        // Drawing the tile
        if (target.TileType != TileID.Pots)
        {
            sb.Draw(
                tex.Value,
                position,
                null,
                Color.White,
                0f,
                tex.Size() / 2,
                1f,
                SpriteEffects.None,
                0f
            );

            return;
        }

        // Pots are handled differently
        tex = TextureAssets.Tile[TileID.Pots];
        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = -1; j <= 1; j += 2)
            {
                int frameY = target.TileFrameY;
                if (frameY % (18 * 2) == 0)
                    frameY += 18;

                var source = tex.Frame(3 * 2, 37 * 2, i == -1 ? 0 : 1, (j == -1 ? 0 : 1) + (int)MathUtils.Round(frameY, 18) / 18 - 1, 0, 0);
                var destination = new Rectangle((int)(position.X - 8 + i * 8), (int)(position.Y - 8 + j * 8), 18, 18);
                sb.Draw(tex.Value, destination, source, Color.White);
            }
        }
    }

    // Calculating the position and rotation for an arrow
    // TODO: make this work with multiple UI scales: Utils.ToScreenPosition
    private static (Vector2, float) GetArrowPositionAndRotation(Vector2 target)
    {
        // Scaling
        float uiScale = Main.UIScale;
        var zoom = Main.GameViewMatrix.Zoom - Vector2.One;

        // Getting position and rotation
        var position = target - Main.LocalPlayer.Center;
        float rotation = position.ToRotation();

        // Changing distance from player
        if (position.Length() >= MaxArrowDistanceFromPlayer)
        {
            position.Normalize();
            position *= MaxArrowDistanceFromPlayer;
        }

        // Moving to the center of the screen
        position += Main.LocalPlayer.Center - Main.screenPosition;

        return (position, rotation);
    }

    // Draws the arrow and background
    private static void DrawArrow(SpriteBatch sb, Vector2 position, float rotation, string name, Vector2 targetPos)
    {
        // Arrow
        var font = FontAssets.DeathText.Value;
        string arrowText = "->";
        float scale = 1f;
        var arrowPos = Vector2.Lerp(Main.LocalPlayer.Center - Main.screenPosition, position, 0.5f);
        var origin = ChatManager.GetStringSize(font, arrowText, scale * Vector2.One) / 3f; // 3f looks better than 2f because the font is weird or something
        ChatManager.DrawColorCodedStringWithShadow(sb, font, arrowText, arrowPos, Color.White, rotation, origin, scale * Vector2.One);

        // Background
        var container = Utils.CenteredRectangle(position, Vector2.One * 50f);
        Utils.DrawInvBG(sb, container);

        // Mouse text
        if (!container.Contains(new Point(Main.mouseX, Main.mouseY)))
            return;

        int distance = (int)MathUtils.Round(targetPos.Distance(Main.LocalPlayer.Center) / 16f);
        string distanceText = Mods.AccessoriesPlus.InfoDisplays.TilesDistance.GetTextValue(distance);

        UIUtils.ResetMouseText();
        UIUtils.MouseText(name + " - " + distanceText, true);
    }
}
