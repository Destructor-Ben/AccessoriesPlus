using AccessoriesPlus.Config.CustomConfigStuff;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Config.SubConfigs;

[SubConfig]
public record MountStatsConfig
{
    public static MountStatsConfig Instance => ClientConfig.Instance.MountStatsConfig;

    public bool Enabled = true;

    public List<ItemDefinition> Whitelist = [];
    public List<ItemDefinition> Blacklist = [];

    public bool FlightTimeTooltipEnabled = true;
    public bool CanHoverTooltipEnabled = true;
    public bool RunSpeedTooltipEnabled = true;
    public bool SwimSpeedTooltipEnabled = true;
    public bool AccelerationTooltipEnabled = true;
    public bool JumpSpeedTooltipEnabled = true;
    public bool JumpHeightTooltipEnabled = true;
    public bool AutoJumpTooltipEnabled = true;
    public bool HeightBoostTooltipEnabled = true;
    public bool FallDamageMultTooltipEnabled = true;
    public bool BoostedMinecartTooltipEnabled = true;
}
