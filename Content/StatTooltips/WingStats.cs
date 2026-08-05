using AccessoriesPlus.Config.SubConfigs;
using AccessoriesPlus.Utilities;
using Terraria.ModLoader.Config;
using VanillaWingStats = Terraria.DataStructures.WingStats;

namespace AccessoriesPlus.Content.StatTooltips;

public class WingStats : TooltipStats
{
    private static WingStatsConfig Config => WingStatsConfig.Instance;

    // TODO: proper null/unknown stat value handling
    // TODO: get vertical wing speed stats
    // TODO: access FlightHeight dictionary properly (rename it and make it instance based)
    // TODO: test whitelist + blacklist
    // TODO: test hybrid wing + boot items

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
        FlightHeight = WingStatsCalculator.VanillaFlightHeight.GetValueOrDefault(item.wingSlot, null);
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
            if (Config.MaxHSpeedHoverMultTooltipEnabled && MaxHSpeedHover is not null)
                yield return TooltipUtils.GetTooltipLine("WingStats.MaxHSpeedHover", MathUtils.Round(MaxHSpeedHover.Value * MathUtils.PPTToMPH, 0.1f));

            if (Config.HAccelerationMultHoverTooltipEnabled && HAccelerationMultHover is not null)
                yield return TooltipUtils.GetTooltipLine("WingStats.HAccelerationMultHover", HAccelerationMultHover.Value);
        }
    }

    public override void InsertTooltips(List<TooltipLine> tooltips, TooltipLine[] statTooltips)
    {
        // Handle info that is better off coming after the stats where there is an existing tooltip line
        var otherTooltips = new List<TooltipLine>();

        // Create the "Allows flight and slow fall" tooltip if it doesn't exist, and insert the other tooltips after
        string? flightTooltipText = Language.GetTextValue("CommonItemTooltip.FlightAndSlowfall");
        int flightTooltipIndex = tooltips.FindIndex(t => t.Text == flightTooltipText);
        if (flightTooltipIndex == -1)
        {
            flightTooltipIndex = tooltips.FindIndexOfTooltipName("Equipable");
            if (flightTooltipIndex == -1)
                return;

            otherTooltips.Add(TooltipUtils.GetTooltipLineWithText("FlightAndSlowFall", flightTooltipText));
        }

        if (Config.NegatesFallDamageTooltipEnabled)
            otherTooltips.Add(TooltipUtils.GetTooltipLine("WingStats.NegatesFallDamage"));

        if (Config.CanHoverTooltipEnabled && CanHover)
            otherTooltips.Add(TooltipUtils.GetTooltipLine("WingStats.CanHover"));

        tooltips.InsertRange(flightTooltipIndex + 1, otherTooltips.ToArray());

        base.InsertTooltips(tooltips, statTooltips);
    }
}
