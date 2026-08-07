using AccessoriesPlus.Config.Elements;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Config.SubConfigs;

[CustomModConfigItem(typeof(CustomObjectElement))]
public record WingStatsConfig
{
    public static WingStatsConfig Instance => ClientConfig.Instance.WingStatsConfig;

    public bool Enabled = true;

    public List<ItemDefinition> Whitelist = [];
    public List<ItemDefinition> Blacklist = [];

    [Header("TooltipLines")]
    public bool AddMissingFlightTooltip = true;
    public bool NegatesFallDamageTooltipEnabled = true;
    public bool CanHoverTooltipEnabled = true;

    public bool FlightTimeTooltipEnabled = true;
    public bool FlightHeightTooltipEnabled = true;

    public bool MaxHSpeedTooltipEnabled = true;
    public bool HAccelerationMultTooltipEnabled = true;
    public bool MaxHSpeedHoverTooltipEnabled = true;
    public bool HAccelerationMultHoverTooltipEnabled = true;

    public bool MaxAscentMultiplierTooltipEnabled = true;
    public bool MaxCanAscendMultiplierTooltipEnabled = true;
    public bool ConstantAscendTooltipEnabled = true;
    public bool AscentWhenRisingTooltipEnabled = true;
    public bool AscentWhenFallingTooltipEnabled = true;
}
