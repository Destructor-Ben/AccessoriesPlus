using AccessoriesPlus.Utilities;

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
        { ArmorIDs.Wing.RainbowWings, 129 }, // 128 is what the wiki says but my code works with 129 (shhhh)
        { ArmorIDs.Wing.FishronWings, 143 },
        { ArmorIDs.Wing.NebulaMantle, 143 },
        { ArmorIDs.Wing.VortexBooster, 143 },
        { ArmorIDs.Wing.SolarWings, 167 },
        { ArmorIDs.Wing.StardustWings, 167 },
        { ArmorIDs.Wing.LongTrailRainbowWings, 201 },
    };

    private static Dictionary<int, VerticalWingStats> MeasuredVerticalWingStats = new();
    private static Dictionary<int, int> MeasuredFlightHeights = new();

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
        MeasuredFlightHeights = null!;
    }

    #region Flight Height

    public static int? GetFlightHeight(int wingID)
    {
        return MeasuredFlightHeights.GetValueOrDefault(wingID);
    }

    private static int CalculateFlightHeight(int wingID)
    {
        var testPlayer = new Player();
        testPlayer.ResetEffects();

        var testItem = new Item {
            wingSlot = wingID,
            ModItem = new WingStatsModItem(),
        };

        testPlayer.equippedWings = testItem;
        testPlayer.wingsLogic = wingID;

        testPlayer.wingTimeMax = testPlayer.GetWingStats(wingID).FlyTime;
        testPlayer.wingTime = testPlayer.wingTimeMax;

        testPlayer.controlJump = true;
        testPlayer.jump = Player.jumpHeight;
        testPlayer.velocity.Y = -Player.jumpSpeed;

        // Mostly copied code from Player.Update
        // TODO: make this also exit if it runs for too long
        while (testPlayer.velocity.Y <= 0)
        {
            // Jump movement
            testPlayer.JumpMovement();

            // Wing movement
            if (testPlayer.wingTime > 0f && testPlayer.jump == 0 && testPlayer.velocity.Y != 0f)
                testPlayer.WingMovement();
            // Gravity
            else
                testPlayer.velocity.Y += testPlayer.gravity;

            // Position update
            testPlayer.position += testPlayer.velocity;
        }

        float flightHeightFloat = -testPlayer.position.Y / 16f;
        int flightHeight = (int)MathUtils.Round(flightHeightFloat);

        if (DefaultFlightHeights.TryGetValue(wingID, out int actualFlightHeight) && actualFlightHeight != flightHeight)
            ModLogger.Warn($"Failed to calculate flight height for wing ID: {wingID}. Actual vs calculated vs rounded: {actualFlightHeight} vs {flightHeightFloat} vs {flightHeight}");

        return flightHeight;
    }

    private static void CalculateFlightHeights()
    {
        for (int wingID = ArmorIDs.Wing.DemonWings; wingID < ArmorIDs.Wing.Sets.Stats.Length; wingID++)
        {
            int flightHeight = CalculateFlightHeight(wingID);
            MeasuredFlightHeights.Add(wingID, flightHeight);
        }
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
