using AccessoriesPlus.Config.CustomConfigStuff;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Config.SubConfigs;

[SubConfig]
public record HookStatsConfig
{
    public static HookStatsConfig Instance => ClientConfig.Instance.HookStatsConfig;

    public bool Enabled = true;

    public List<ItemDefinition> Whitelist = [];
    public List<ItemDefinition> Blacklist = [];

    public bool ReachTooltipEnabled = true;
    public bool NumHooksTooltipEnabled = true;
    public bool LatchingTooltipEnabled = true;
    public bool ShootSpeedTooltipEnabled = true;
    public bool RetreatSpeedTooltipEnabled = true;
    public bool PullSpeedTooltipEnabled = true;
}
