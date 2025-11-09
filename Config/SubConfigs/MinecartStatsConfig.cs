using AccessoriesPlus.Config.CustomConfigStuff;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Config.SubConfigs;

[SubConfig]
public record MinecartStatsConfig
{
    public static MinecartStatsConfig Instance => ClientConfig.Instance.MinecartStatsConfig;

    public bool Enabled = true;

    public List<ItemDefinition> Whitelist = [];
    public List<ItemDefinition> Blacklist = [];

    public bool RunSpeedTooltipEnabled = true;
    public bool AccelerationTooltipEnabled = true;
    public bool JumpSpeedTooltipEnabled = true;
    public bool JumpHeightTooltipEnabled = true;
    public bool HeightBoostTooltipEnabled = true;
    public bool BoostedStatsTooltipEnabled = true;
}
