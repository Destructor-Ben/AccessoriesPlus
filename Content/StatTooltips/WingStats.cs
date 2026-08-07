using AccessoriesPlus.Config.SubConfigs;
using AccessoriesPlus.Utilities;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Content.StatTooltips;

public class WingStats : TooltipStats
{
    private static WingStatsConfig Config => WingStatsConfig.Instance;

    public float? FlightTime { get; private set; } = null;
    public int? FlightHeight { get; private set; } = null;

    public float? MaxHSpeed { get; private set; } = null;
    public float? HAccelerationMult { get; private set; } = null;

    public bool CanHover { get; private set; } = false;
    public float? MaxHSpeedHover { get; private set; } = null;
    public float? HAccelerationMultHover { get; private set; } = null;

    public float? MaxAscentMultiplier { get; private set; } = null;
    public float? MaxCanAscendMultiplier { get; private set; } = null;
    public float? ConstantAscend { get; private set; } = null;
    public float? AscentWhenRising { get; private set; } = null;
    public float? AscentWhenFalling { get; private set; } = null;

    public override bool Enabled => Config.Enabled;
    public override bool PressKeyToRevealStats => Config.PressKeyToRevealStats;
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
        FlightHeight = WingStatsCalculator.GetFlightHeight(item.wingSlot);

        MaxHSpeed = vanillaStats.AccRunSpeedOverride;
        HAccelerationMult = vanillaStats.AccRunAccelerationMult;

        CanHover = vanillaStats.HasDownHoverStats;
        MaxHSpeedHover = vanillaStats.DownHoverSpeedOverride;
        HAccelerationMultHover = vanillaStats.DownHoverAccelerationMult;

        if (MaxHSpeed == -1f)
            MaxHSpeed = null;

        if (MaxHSpeedHover == -1f)
            MaxHSpeedHover = null;

        var verticalWingStats = WingStatsCalculator.GetVerticalWingStats(item.wingSlot);
        if (verticalWingStats is null)
            return;

        var actualVerticalWingStats = verticalWingStats.Value;
        MaxAscentMultiplier = actualVerticalWingStats.MaxAscentMultiplier;
        MaxCanAscendMultiplier = actualVerticalWingStats.MaxCanAscendMultiplier;
        ConstantAscend = actualVerticalWingStats.ConstantAscend;
        AscentWhenRising = actualVerticalWingStats.AscentWhenRising;
        AscentWhenFalling = actualVerticalWingStats.AscentWhenFalling;
    }

    public override IEnumerable<TooltipLine> GetTooltips()
    {
        // Flight
        if (Config.FlightTimeTooltipEnabled && FlightTime is not null)
            yield return TooltipUtils.GetTooltipLine("WingStats.FlightTime", (FlightTime.Value / 60f).ToNiceString(1), MathF.Floor(FlightTime.Value));

        if (Config.FlightHeightTooltipEnabled && FlightHeight is not null)
            yield return TooltipUtils.GetTooltipLine("WingStats.FlightHeight", FlightHeight.Value);

        // Horizontal motion
        if (Config.MaxHSpeedTooltipEnabled && MaxHSpeed is not null)
            yield return TooltipUtils.GetTooltipLine("WingStats.MaxHSpeed", (MaxHSpeed.Value * MathUtils.PixelsPerTick2MilesPerHour).ToNiceString(1));

        if (Config.HAccelerationMultTooltipEnabled && HAccelerationMult is not null)
            yield return TooltipUtils.GetTooltipLine("WingStats.HAccelerationMult", (HAccelerationMult * 100).Value.ToNiceString(0));

        // Hovering
        if (CanHover)
        {
            if (Config.MaxHSpeedHoverTooltipEnabled && MaxHSpeedHover is not null)
                yield return TooltipUtils.GetTooltipLine("WingStats.MaxHSpeedHover", (MaxHSpeedHover.Value * MathUtils.PixelsPerTick2MilesPerHour).ToNiceString(1));

            if (Config.HAccelerationMultHoverTooltipEnabled && HAccelerationMultHover is not null)
                yield return TooltipUtils.GetTooltipLine("WingStats.HAccelerationMultHover", (HAccelerationMultHover * 100).Value.ToNiceString(0));
        }

        // Vertical motion
        if (Config.MaxAscentMultiplierTooltipEnabled && MaxAscentMultiplier is not null)
            yield return TooltipUtils.GetTooltipLine("WingStats.MaxAscentMultiplier", (MaxAscentMultiplier * 100).Value.ToNiceString(0));

        if (Config.MaxCanAscendMultiplierTooltipEnabled && MaxCanAscendMultiplier is not null)
            yield return TooltipUtils.GetTooltipLine("WingStats.MaxCanAscendMultiplier", (MaxCanAscendMultiplier * 100).Value.ToNiceString(0));

        if (Config.ConstantAscendTooltipEnabled && ConstantAscend is not null)
            yield return TooltipUtils.GetTooltipLine("WingStats.ConstantAscend", (ConstantAscend.Value * MathUtils.PixelsPerTickPerTick2MilesPerHourPerSecond).ToNiceString(1));

        if (Config.AscentWhenRisingTooltipEnabled && AscentWhenRising is not null)
            yield return TooltipUtils.GetTooltipLine("WingStats.AscentWhenRising", (AscentWhenRising * MathUtils.PixelsPerTickPerTick2MilesPerHourPerSecond).Value.ToNiceString(1));

        if (Config.AscentWhenFallingTooltipEnabled && AscentWhenFalling is not null)
            yield return TooltipUtils.GetTooltipLine("WingStats.AscentWhenFalling", (AscentWhenFalling * MathUtils.PixelsPerTickPerTick2MilesPerHourPerSecond).Value.ToNiceString(1));
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

            if (Config.AddMissingFlightTooltip)
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
