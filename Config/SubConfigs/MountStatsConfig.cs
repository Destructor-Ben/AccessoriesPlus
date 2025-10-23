using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Config.SubConfigs;

public record MountStatsConfig
{
    public static MountStatsConfig Instance => ClientConfig.Instance.MountStatsConfig;

    // TODO: combine mount and minecart stats configs and just use 2 enabled bools?
    // = if not, then separate the MountStats object into MountStats and MinecartStats
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

    public override string? ToString() => null;
}
