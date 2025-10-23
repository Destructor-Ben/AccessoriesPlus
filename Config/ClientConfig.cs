using AccessoriesPlus.Config.CustomConfigStuff;
using Terraria.ModLoader.Config;
using HookStatsConfig = AccessoriesPlus.Config.SubConfigs.HookStatsConfig;
using LightPetStatsConfig = AccessoriesPlus.Config.SubConfigs.LightPetStatsConfig;
using MinecartStatsConfig = AccessoriesPlus.Config.SubConfigs.MinecartStatsConfig;
using MountStatsConfig = AccessoriesPlus.Config.SubConfigs.MountStatsConfig;
using WingStatsConfig = AccessoriesPlus.Config.SubConfigs.WingStatsConfig;

namespace AccessoriesPlus.Config;

public class ClientConfig : CustomModConfig
{
    public static ClientConfig Instance => ModContent.GetInstance<ClientConfig>();
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [Header("StatTooltips")]
    public WingStatsConfig WingStatsConfig = new();
    public HookStatsConfig HookStatsConfig = new();
    public LightPetStatsConfig LightPetStatsConfig = new();
    public MinecartStatsConfig MinecartStatsConfig = new();
    public MountStatsConfig MountStatsConfig = new();

    // TODO: new visuals for certain accessories should be moved here
}
