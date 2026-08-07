namespace AccessoriesPlus.Content.StatTooltips;

public record struct VerticalWingStats(
    float AscentWhenFalling,
    float AscentWhenRising,
    float MaxCanAscendMultiplier,
    float MaxAscentMultiplier,
    float ConstantAscend
);

public class WingStatsCalculator : ModSystem
{
    // TODO: temp, use for testing the calculations in the future
    // TODO: run the tests on mod load (since calculations will run anyway) and if they differ, log a message about it
    // From https://terraria.wiki.gg/wiki/Wings/List
    private static readonly Dictionary<int, int> DefaultFlightHeights = new()
    {
        { ArmorIDs.Wing.CreativeWings, 18 },
        { ArmorIDs.Wing.AngelWings, 53 },
        { ArmorIDs.Wing.DemonWings, 53 },
        { ArmorIDs.Wing.FairyWings, 67 },
        { ArmorIDs.Wing.FinWings, 67 },
        { ArmorIDs.Wing.FrozenWings, 67 },
        { ArmorIDs.Wing.HarpyWings, 67 },
        { ArmorIDs.Wing.Jetpack, 77 },
        { ArmorIDs.Wing.RedsWings, 77 },
        { ArmorIDs.Wing.DTownsWings, 77 },
        { ArmorIDs.Wing.WillsWings, 77 },
        { ArmorIDs.Wing.CrownosWings, 77 },
        { ArmorIDs.Wing.CenxsWings, 77 },
        { ArmorIDs.Wing.LazuresBarrierPlatform, 77 },
        { ArmorIDs.Wing.Yoraiz0rsSpell, 77 },
        { ArmorIDs.Wing.JimsWings, 77 },
        { ArmorIDs.Wing.SkiphssPaws, 77 },
        { ArmorIDs.Wing.LokisWings, 77 },
        { ArmorIDs.Wing.ArkhalisWings, 77 },
        { ArmorIDs.Wing.LeinforsWings, 77 },
        { ArmorIDs.Wing.GhostarsWings, 77 },
        { ArmorIDs.Wing.SafemanWings, 77 },
        { ArmorIDs.Wing.FoodBarbarianWings, 77 },
        { ArmorIDs.Wing.GroxTheGreatWings, 77 },
        { ArmorIDs.Wing.LeafWings, 81 },
        { ArmorIDs.Wing.BatWings, 81 },
        { ArmorIDs.Wing.BeeWings, 81 },
        { ArmorIDs.Wing.ButterflyWings, 81 },
        { ArmorIDs.Wing.FlameWings, 81 },
        { ArmorIDs.Wing.Hoverboard, 94 },
        { ArmorIDs.Wing.BoneWings, 94 },
        { ArmorIDs.Wing.MothronWings, 94 },
        { ArmorIDs.Wing.SpectreWings, 94 },
        { ArmorIDs.Wing.BeetleWings, 94 },
        { ArmorIDs.Wing.FestiveWings, 107 },
        { ArmorIDs.Wing.SpookyWings, 107 },
        { ArmorIDs.Wing.TatteredFairyWings, 107 },
        { ArmorIDs.Wing.SteampunkWings, 107 },
        { ArmorIDs.Wing.BetsyWings, 119 },
        { ArmorIDs.Wing.RainbowWings, 128 },
        { ArmorIDs.Wing.FishronWings, 143 },
        { ArmorIDs.Wing.NebulaMantle, 143 },
        { ArmorIDs.Wing.VortexBooster, 143 },
        { ArmorIDs.Wing.SolarWings, 167 },
        { ArmorIDs.Wing.StardustWings, 167 },
        { ArmorIDs.Wing.LongTrailRainbowWings, 201 },
    };

    private static Dictionary<int, VerticalWingStats> MeasuredVerticalWingStats = new();

    private static bool IsFetchingWingStats = false;

    public override void PostSetupContent()
    {
        FetchVerticalWingSpeeds();
        CalculateFlightHeights();
    }

    public override void Unload()
    {
        IsFetchingWingStats = false;
        MeasuredVerticalWingStats = null!;
    }

    #region Flight Height

    public static int? GetFlightHeight(int wingID)
    {
        return 0; // TODO: impl
    }

    private static float CalculateFlightHeight(int wingID)
    {
        return 0f; // TODO: impl
    }

    private static void CalculateFlightHeights()
    {
        // TODO: impl
    }

    #endregion

    #region Vertical Wing Stats

    public static VerticalWingStats? GetVerticalWingStats(int wingID)
    {
        return MeasuredVerticalWingStats.GetValueOrDefault(wingID);
    }

    private static void MeasureVerticalWingSpeeds(int wingID, VerticalWingStats stats)
    {
        MeasuredVerticalWingStats.Add(wingID, stats);
    }

    private static void FetchVerticalWingSpeeds()
    {
        MeasuredVerticalWingStats = new Dictionary<int, VerticalWingStats>();

        IsFetchingWingStats = true;

        var testPlayer = new Player();

        for (int wingID = ArmorIDs.Wing.DemonWings; wingID < ArmorIDs.Wing.Sets.Stats.Length; wingID++)
        {
            var testItem = new Item {
                wingSlot = wingID,
                ModItem = new WingStatsModItem(),
            };

            testPlayer.equippedWings = testItem;
            testPlayer.wingsLogic = wingID;

            // Trigger VerticalWingSpeeds hook
            testPlayer.WingMovement();
        }

        IsFetchingWingStats = false;
    }

    // GlobalItem doesn't seem to work until after the game has finished loading
    private class WingStatsModItem : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            if (!IsFetchingWingStats)
                return;

            var stats = new VerticalWingStats(ascentWhenFalling, ascentWhenRising, maxCanAscendMultiplier, maxAscentMultiplier, constantAscend);
            MeasureVerticalWingSpeeds(player.wingsLogic, stats);
        }
    }

    #endregion
}
