using AccessoriesPlus.Config.SubConfigs;
using AccessoriesPlus.Utilities;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Content.StatTooltips;

public class MountStats : TooltipStats
{
    // TODO: separate minecart stats out? do we want a separate minecart config?
    private static MountStatsConfig Config => MountStatsConfig.Instance;

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

    public override bool Enabled => Config.Enabled;
    public override bool PressKeyToRevealStats => Config.PressKeyToRevealStats;
    public override List<ItemDefinition> Whitelist => Config.Whitelist;
    public override List<ItemDefinition> Blacklist => Config.Blacklist;

    public override bool ItemMeetsDefaultCondition(Item item)
    {
        return item.mountType > MountID.None;
    }

    protected override void SetStatsFromItem(Item item)
    {
        var vanillaStats = Mount.mounts[item.mountType];

        FlightTime = vanillaStats.flightTimeMax;
        CanHover = vanillaStats.usesHover;
        RunSpeed = Math.Max(vanillaStats.runSpeed, vanillaStats.dashSpeed);
        SwimSpeed = vanillaStats.swimSpeed;
        Acceleration = vanillaStats.acceleration;
        JumpSpeed = vanillaStats.jumpSpeed;
        JumpHeight = vanillaStats.jumpHeight;
        AutoJump = vanillaStats.constantJump;
        HeightBoost = vanillaStats.heightBoost;
        FallDamageMult = vanillaStats.fallDamage;

        // Minecart upgrade kit
        if (!MountID.Sets.Cart[item.mountType] || !Main.LocalPlayer.UsingSuperCart)
            return;

        RunSpeed = Math.Max(vanillaStats.MinecartUpgradeRunSpeed, vanillaStats.MinecartUpgradeDashSpeed);
        Acceleration = vanillaStats.MinecartUpgradeAcceleration;
        JumpHeight = vanillaStats.MinecartUpgradeJumpHeight;
        JumpSpeed = vanillaStats.MinecartUpgradeJumpSpeed;
        BoostedMinecart = true;
    }

    public override IEnumerable<TooltipLine> GetTooltips()
    {
        // Flight
        if (FlightTime > 0)
            yield return TooltipUtils.GetTooltipLine("MountStats.FlightTime", (decimal)MathUtils.Round(FlightTime / 60f, 0.1f));

        // Run speeds
        if (RunSpeed != 0f)
            yield return TooltipUtils.GetTooltipLine("MountStats.RunSpeed", (decimal)MathUtils.Round(RunSpeed * MathUtils.PixelsPerTick2MilesPerHour, 0.1f));

        if (SwimSpeed != 0f)
            yield return TooltipUtils.GetTooltipLine("MountStats.SwimSpeed", (decimal)MathUtils.Round(SwimSpeed * MathUtils.PixelsPerTick2MilesPerHour, 0.1f));

        if (Acceleration != 0f)
            yield return TooltipUtils.GetTooltipLine("MountStats.Acceleration", (decimal)MathUtils.Round(Acceleration * MathUtils.PixelsPerTickPerTick2MilesPerHourPerSecond, 0.1f));

        // Jumping
        if (JumpSpeed != 0f)
            yield return TooltipUtils.GetTooltipLine("MountStats.JumpSpeed", (decimal)MathUtils.Round(JumpSpeed * MathUtils.PixelsPerTick2MilesPerHour, 0.1f));

        if (JumpHeight != 0)
            yield return TooltipUtils.GetTooltipLine("MountStats.JumpHeight", (decimal)MathUtils.Round(JumpHeight / 16f, 0.1f)); // TODO: is this correct?

        // Misc
        if (HeightBoost != 0)
            yield return TooltipUtils.GetTooltipLine("MountStats.HeightBoost", (decimal)MathUtils.Round((HeightBoost + 42) / 16f, 0.1f)); // 42 is player height

        if (FallDamageMult != 1f)
            yield return TooltipUtils.GetTooltipLine("MountStats.FallDamageMult", FallDamageMult);

        // Non stat lines
        if (CanHover)
            yield return TooltipUtils.GetTooltipLine("MountStats.CanHover");

        if (AutoJump)
            yield return TooltipUtils.GetTooltipLine("MountStats.AutoJump");

        // Bosted for minecarts
        if (BoostedMinecart)
            yield return TooltipUtils.GetTooltipLine("MountStats.BoostedMinecart");
    }
}
