using AccessoriesPlus.Config.Elements;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Config.SubConfigs;

[CustomModConfigItem(typeof(CustomObjectElement))]
public record MinecartStatsConfig
{
    public static MinecartStatsConfig Instance => ClientConfig.Instance.MinecartStatsConfig;

    public bool Enabled = true;
    public bool PressKeyToRevealStats = false;

    public List<ItemDefinition> Whitelist = [];
    public List<ItemDefinition> Blacklist = [];

    [Header("TooltipLines")]
    public bool RunSpeedTooltipEnabled = true;
    public bool AccelerationTooltipEnabled = true;
    public bool JumpSpeedTooltipEnabled = true;
    public bool JumpHeightTooltipEnabled = true;
    public bool HeightBoostTooltipEnabled = true;
    public bool BoostedStatsTooltipEnabled = true;
}
