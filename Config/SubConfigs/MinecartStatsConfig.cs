using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Config.SubConfigs;

public record MinecartStatsConfig
{
    public static MinecartStatsConfig Instance => ClientConfig.Instance.MinecartStatsConfig;

    public bool Enabled = true;

    public List<ItemDefinition> Whitelist = [];
    public List<ItemDefinition> Blacklist = [];

    // TODO: bool for each stat line
    public int FlightTime { get; private set; } = 0;
    public bool CanHover { get; private set; } = false;
    public float RunSpeed { get; private set; } = 0f;
    public float SwimSpeed { get; private set; } = 0f;
    public float Acceleration { get; private set; } = 0f;
    public float JumpSpeed { get; private set; } = 0f;
    public int JumpHeight { get; private set; } = 0;
    public bool AutoJump { get; private set; } = false;
    public int HeightBoost { get; private set; } = 0;
    public float FallDamageMult { get; private set; } = 0f;
    public bool BoostedMinecart { get; private set; } = false;
}
