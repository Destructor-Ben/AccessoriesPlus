using AccessoriesPlus.Config.CustomConfigStuff;
using AccessoriesPlus.Config.SubConfigs;
using Terraria.ModLoader.Config;

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
