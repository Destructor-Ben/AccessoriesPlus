using AccessoriesPlus.Config.SubConfigs;
using AccessoriesPlus.Utilities;
using Terraria.ModLoader.Config;
using VanillaWingStats = Terraria.DataStructures.WingStats;

namespace AccessoriesPlus.Content.StatTooltips;

public class WingStats : TooltipStats
{
    private static WingStatsConfig Config => WingStatsConfig.Instance;

    public float? FlightTime { get; private set; } = null;
    public float? FlightHeight { get; private set; } = null;
    public float? MaxHSpeed { get; private set; } = null;
    public float? HAccelerationMult { get; private set; } = null;
    public bool CanHover { get; private set; } = false;
    public float? MaxHSpeedHover { get; private set; } = null;
    public float? HAccelerationMultHover { get; private set; } = null;

    public override bool Enabled => Config.Enabled;
    public override List<ItemDefinition> Whitelist => Config.Whitelist;
    public override List<ItemDefinition> Blacklist => Config.Blacklist;

    public static Dictionary<int, float?> VanillaFlightHeight = new()
    {
        { ArmorIDs.Wing.CreativeWings, 18 * 16f },
        { ArmorIDs.Wing.AngelWings, 53 * 16f },
        { ArmorIDs.Wing.DemonWings, 53 * 16f },
        { ArmorIDs.Wing.FairyWings, 67 * 16f },
        { ArmorIDs.Wing.FinWings, 67 * 16f },
        { ArmorIDs.Wing.FrozenWings, 67 * 16f },
        { ArmorIDs.Wing.HarpyWings, 67 * 16f },
        { ArmorIDs.Wing.Jetpack, 77 * 16f },
        { ArmorIDs.Wing.RedsWings, 77 * 16f },
        { ArmorIDs.Wing.DTownsWings, 77 * 16f },
        { ArmorIDs.Wing.WillsWings, 77 * 16f },
        { ArmorIDs.Wing.CrownosWings, 77 * 16f },
        { ArmorIDs.Wing.CenxsWings, 77 * 16f },
        { ArmorIDs.Wing.LazuresBarrierPlatform, 77 * 16f },
        { ArmorIDs.Wing.Yoraiz0rsSpell, 77 * 16f },
        { ArmorIDs.Wing.JimsWings, 77 * 16f },
        { ArmorIDs.Wing.SkiphssPaws, 77 * 16f },
        { ArmorIDs.Wing.LokisWings, 77 * 16f },
        { ArmorIDs.Wing.ArkhalisWings, 77 * 16f },
        { ArmorIDs.Wing.LeinforsWings, 77 * 16f },
        { ArmorIDs.Wing.GhostarsWings, 77 * 16f },
        { ArmorIDs.Wing.SafemanWings, 77 * 16f },
        { ArmorIDs.Wing.FoodBarbarianWings, 77 * 16f },
        { ArmorIDs.Wing.GroxTheGreatWings, 77 * 16f },
        { ArmorIDs.Wing.LeafWings, 81 * 16f },
        { ArmorIDs.Wing.BatWings, 81 * 16f },
        { ArmorIDs.Wing.BeeWings, 81 * 16f },
        { ArmorIDs.Wing.ButterflyWings, 81 * 16f },
        { ArmorIDs.Wing.FlameWings, 81 * 16f },
        { ArmorIDs.Wing.Hoverboard, 94 * 16f },
        { ArmorIDs.Wing.BoneWings, 94 * 16f },
        { ArmorIDs.Wing.MothronWings, 94 * 16f },
        { ArmorIDs.Wing.SpectreWings, 94 * 16f },
        { ArmorIDs.Wing.BeetleWings, 94 * 16f },
        { ArmorIDs.Wing.FestiveWings, 107 * 16f },
        { ArmorIDs.Wing.SpookyWings, 107 * 16f },
        { ArmorIDs.Wing.TatteredFairyWings, 107 * 16f },
        { ArmorIDs.Wing.SteampunkWings, 107 * 16f },
        { ArmorIDs.Wing.BetsyWings, 119 * 16f },
        { ArmorIDs.Wing.RainbowWings, 128 * 16f },
        { ArmorIDs.Wing.FishronWings, 143 * 16f },
        { ArmorIDs.Wing.NebulaMantle, 143 * 16f },
        { ArmorIDs.Wing.VortexBooster, 143 * 16f },
        { ArmorIDs.Wing.SolarWings, 167 * 16f },
        { ArmorIDs.Wing.StardustWings, 167 * 16f },
        { ArmorIDs.Wing.LongTrailRainbowWings, 201 * 16f },
    };

    public override bool ItemMeetsDefaultCondition(Item item)
    {
        return item.wingSlot > 0;
    }

    protected override void SetStatsFromItem(Item item)
    {
        var vanillaStats = Main.LocalPlayer.GetWingStats(item.wingSlot);
        // Copied this check from from Player.GetWingStats
        if (item.wingSlot <= 0 || item.wingSlot >= ArmorIDs.Wing.Sets.Stats.Length)
            return;

        FlightTime = vanillaStats.FlyTime;
        // TODO: calculate flight height after content is setup
        FlightHeight = VanillaFlightHeight.GetValueOrDefault(item.wingSlot, null);
        MaxHSpeed = vanillaStats.AccRunSpeedOverride;
        HAccelerationMult = vanillaStats.AccRunAccelerationMult;
        CanHover = vanillaStats.HasDownHoverStats;
        MaxHSpeedHover = vanillaStats.DownHoverSpeedOverride;
        HAccelerationMultHover = vanillaStats.DownHoverAccelerationMult;
    }

    public override IEnumerable<TooltipLine> GetTooltips()
    {
        // Flight
        if (Config.FlightTimeTooltipEnabled && FlightTime is not null)
            yield return TooltipUtils.GetTooltipLine("WingStats.FlightTime", MathUtils.Round(FlightTime.Value / 60f, 0.1f));

        if (Config.FlightHeightTooltipEnabled)
        {
            yield return 
                FlightHeight is not null
                    ? TooltipUtils.GetTooltipLine("WingStats.FlightHeight", MathUtils.Round(FlightHeight.Value / 16f, 0.1f))
                    : TooltipUtils.GetTooltipLine("WingStats.FlightHeightUnknown");
        }

        // Horizontal motion
        if (Config.MaxHSpeedTooltipEnabled && MaxHSpeed is not null)
            yield return TooltipUtils.GetTooltipLine("WingStats.MaxHSpeed", MathUtils.Round(MaxHSpeed.Value * MathUtils.PPTToMPH, 0.1f));

        if (Config.HAccelerationMultTooltipEnabled && HAccelerationMult is not null)
            yield return TooltipUtils.GetTooltipLine("WingStats.HAccelerationMult", HAccelerationMult.Value);

        // Hovering
        if (CanHover)
        {
            if (Config.CanHoverTooltipEnabled)
                yield return TooltipUtils.GetTooltipLine("WingStats.CanHover");

            if (Config.MaxHSpeedHoverMultTooltipEnabled && MaxHSpeedHover is not null)
                yield return TooltipUtils.GetTooltipLine("WingStats.MaxHSpeedHover", MathUtils.Round(MaxHSpeedHover.Value * MathUtils.PPTToMPH, 0.1f));

            if (Config.HAccelerationMultHoverTooltipEnabled && HAccelerationMultHover is not null)
                yield return TooltipUtils.GetTooltipLine("WingStats.HAccelerationMultHover", HAccelerationMultHover.Value);
        }

        // Negates fall damage
        if (Config.NegatesFallDamageTooltipEnabled)
            yield return TooltipUtils.GetTooltipLine("WingStats.NegatesFallDamage");
    }
}
