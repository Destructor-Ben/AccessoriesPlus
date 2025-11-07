using AccessoriesPlus.Config.CustomConfigStuff;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Config.SubConfigs;

[SubConfig]
public record WingStatsConfig
{
    public static WingStatsConfig Instance => ClientConfig.Instance.WingStatsConfig;

    public bool Enabled = true;

    public List<ItemDefinition> Whitelist = [];
    public List<ItemDefinition> Blacklist = [];

    public bool FlightTimeTooltipEnabled = true;
    public bool FlightHeightTooltipEnabled = true;
    public bool MaxHSpeedTooltipEnabled = true;
    public bool HAccelerationMultTooltipEnabled = true;
    public bool CanHoverTooltipEnabled = true;
    public bool MaxHSpeedHoverMultTooltipEnabled = true;
    public bool HAccelerationMultHoverTooltipEnabled = true;
    public bool NegatesFallDamageTooltipEnabled = true;
}
