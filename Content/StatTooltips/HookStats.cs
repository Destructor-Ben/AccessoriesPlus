using AccessoriesPlus.Config;
using AccessoriesPlus.Utilities;

namespace AccessoriesPlus.Content.StatTooltips;

public class HookStats : Stats
{
    public enum LatchingMode
    {
        Unknown,
        Single,
        Individual,
        Simultaneous
    }

    public float Reach { get; private set; } = -1f;
    public int NumHooks { get; private set; } = -1;
    public LatchingMode Latching { get; private set; } = LatchingMode.Unknown;
    public float ShootSpeed { get; private set; } = -1f;
    public float RetreatSpeed { get; private set; } = -1f;
    public float PullSpeed { get; private set; } = -1f;

    // TODO: combine these into a single dictionary of HookStats
    // TODO: for stats that have a dictionary, get it from the hooks that tML provides such as ProjectileLoader.GrapplePullSpeed and store it
    // TODO: change item ids to projectile ids
    public static Dictionary<int, float> VanillaReach { get; private set; } = new()
    {
        // Pre hardmode
        { ItemID.GrapplingHook, 16f * 18.75f },
        { ItemID.AmethystHook, 16f * 18.75f },
        { ItemID.SquirrelHook, 16f * 19f },
        { ItemID.TopazHook, 16f * 20.625f },
        { ItemID.SapphireHook, 16f * 22.5f },
        { ItemID.EmeraldHook, 16f * 24.375f },
        { ItemID.RubyHook, 16f * 26.25f },
        { ItemID.AmberHook, 16f * 27.5f },
        { ItemID.DiamondHook, 16f * 29.125f },
        { ItemID.WebSlinger, 16f * 22.625f },
        { ItemID.SkeletronHand, 16f * 21.875f },
        { ItemID.SlimeHook, 16f * 18.75f },
        { ItemID.FishHook, 16f * 25f },
        { ItemID.IvyWhip, 16f * 28f },
        { ItemID.BatHook, 16f * 31.25f },
        { ItemID.CandyCaneHook, 16f * 25f },

        // Hardmode               
        { ItemID.DualHook, 16f * 27.5f },
        { ItemID.QueenSlimeHook, 16f * 30f },
        { ItemID.ThornHook, 16f * 30f },
        { ItemID.IlluminantHook, 16f * 30f },
        { ItemID.WormHook, 16f * 30f },
        { ItemID.TendonHook, 16f * 30f },
        { ItemID.AntiGravityHook, 16f * 31.25f },
        { ItemID.SpookyHook, 16f * 34.375f },
        { ItemID.ChristmasHook, 16f * 34.375f },
        { ItemID.LunarHook, 16f * 34.375f },
        { ItemID.StaticHook, 16f * 37.5f },
    };

    public static Dictionary<int, int> VanillaNumHooks { get; private set; } = new()
    {
        // Pre hardmode
        { ItemID.GrapplingHook, 1 },
        { ItemID.AmethystHook, 1 },
        { ItemID.SquirrelHook, 1 },
        { ItemID.TopazHook, 1 },
        { ItemID.SapphireHook, 1 },
        { ItemID.EmeraldHook, 1 },
        { ItemID.RubyHook, 1 },
        { ItemID.AmberHook, 1 },
        { ItemID.DiamondHook, 1 },
        { ItemID.WebSlinger, 8 },
        { ItemID.SkeletronHand, 2 },
        { ItemID.SlimeHook, 3 },
        { ItemID.FishHook, 2 },
        { ItemID.IvyWhip, 3 },
        { ItemID.BatHook, 1 },
        { ItemID.CandyCaneHook, 1 },

        // Hardmode               
        { ItemID.DualHook, 2 },
        { ItemID.QueenSlimeHook, 1 },
        { ItemID.ThornHook, 3 },
        { ItemID.IlluminantHook, 3 },
        { ItemID.WormHook, 3 },
        { ItemID.TendonHook, 3 },
        { ItemID.AntiGravityHook, 3 },
        { ItemID.SpookyHook, 3 },
        { ItemID.ChristmasHook, 3 },
        { ItemID.LunarHook, 4 },
        { ItemID.StaticHook, 2 },
    };

    public static Dictionary<int, LatchingMode> VanillaLatching { get; private set; } = new()
    {
        // Pre hardmode
        { ItemID.GrapplingHook, LatchingMode.Single },
        { ItemID.AmethystHook, LatchingMode.Single },
        { ItemID.SquirrelHook, LatchingMode.Single },
        { ItemID.TopazHook, LatchingMode.Single },
        { ItemID.SapphireHook, LatchingMode.Single },
        { ItemID.EmeraldHook, LatchingMode.Single },
        { ItemID.RubyHook, LatchingMode.Single },
        { ItemID.AmberHook, LatchingMode.Single },
        { ItemID.DiamondHook, LatchingMode.Single },
        { ItemID.WebSlinger, LatchingMode.Simultaneous },
        { ItemID.SkeletronHand, LatchingMode.Simultaneous },
        { ItemID.SlimeHook, LatchingMode.Simultaneous },
        { ItemID.FishHook, LatchingMode.Simultaneous },
        { ItemID.IvyWhip, LatchingMode.Simultaneous },
        { ItemID.BatHook, LatchingMode.Single },
        { ItemID.CandyCaneHook, LatchingMode.Single },

        // Hardmode               
        { ItemID.DualHook, LatchingMode.Individual },
        { ItemID.QueenSlimeHook, LatchingMode.Single },
        { ItemID.ThornHook, LatchingMode.Simultaneous },
        { ItemID.IlluminantHook, LatchingMode.Simultaneous },
        { ItemID.WormHook, LatchingMode.Simultaneous },
        { ItemID.TendonHook, LatchingMode.Simultaneous },
        { ItemID.AntiGravityHook, LatchingMode.Simultaneous },
        { ItemID.SpookyHook, LatchingMode.Simultaneous },
        { ItemID.ChristmasHook, LatchingMode.Simultaneous },
        { ItemID.LunarHook, LatchingMode.Simultaneous },
        { ItemID.StaticHook, LatchingMode.Individual },
    };

    public static Dictionary<int, float> VanillaRetreatSpeed { get; private set; } = new()
    {
        // Pre hardmode
        { ItemID.GrapplingHook, 11f },
        { ItemID.AmethystHook, 11f },
        { ItemID.SquirrelHook, 11f },
        { ItemID.TopazHook, 11.75f },
        { ItemID.SapphireHook, 12.5f },
        { ItemID.EmeraldHook, 13.25f },
        { ItemID.RubyHook, 14f },
        { ItemID.AmberHook, 15f },
        { ItemID.DiamondHook, 14.75f },
        { ItemID.WebSlinger, 11f },
        { ItemID.SkeletronHand, 11f },
        { ItemID.SlimeHook, 11f },
        { ItemID.FishHook, 11f },
        { ItemID.IvyWhip, 15f },
        { ItemID.BatHook, 20f },
        { ItemID.CandyCaneHook, 11f },

        // Hardmode               
        { ItemID.DualHook, 17f },
        { ItemID.QueenSlimeHook, 11f },
        { ItemID.ThornHook, 18f },
        { ItemID.IlluminantHook, 18f },
        { ItemID.WormHook, 18f },
        { ItemID.TendonHook, 18f },
        { ItemID.AntiGravityHook, 20f },
        { ItemID.SpookyHook, 22f },
        { ItemID.ChristmasHook, 17f },
        { ItemID.LunarHook, 24f },
        { ItemID.StaticHook, 24f },
    };

    public static Dictionary<int, float> VanillaPullSpeed { get; private set; } = new()
    {
        // Pre hardmode
        { ItemID.GrapplingHook, 11f },
        { ItemID.AmethystHook, 11f },
        { ItemID.SquirrelHook, 11f },
        { ItemID.TopazHook, 11f },
        { ItemID.SapphireHook, 11f },
        { ItemID.EmeraldHook, 11f },
        { ItemID.RubyHook, 11f },
        { ItemID.AmberHook, 11f },
        { ItemID.DiamondHook, 11f },
        { ItemID.WebSlinger, 11f },
        { ItemID.SkeletronHand, 11f },
        { ItemID.SlimeHook, 11f },
        { ItemID.FishHook, 11f },
        { ItemID.IvyWhip, 11f },
        { ItemID.BatHook, 14f },
        { ItemID.CandyCaneHook, 11f },

        // Hardmode               
        { ItemID.DualHook, 11f },
        { ItemID.QueenSlimeHook, 11f },
        { ItemID.ThornHook, 12f },
        { ItemID.IlluminantHook, 11f },
        { ItemID.WormHook, 11f },
        { ItemID.TendonHook, 11f },
        { ItemID.AntiGravityHook, 11f },
        { ItemID.SpookyHook, 11f },
        { ItemID.ChristmasHook, 11f },
        { ItemID.LunarHook, 16f },
        { ItemID.StaticHook, 11f },
    };

    private HookStats() { }

    public static HookStats? Get(Item item)
    {
        if (!ClientConfig.Instance.HookStatsConfig.Enabled || item.shoot <= ProjectileID.None || !Main.projHook[item.shoot])
            return null;

        var stats = new HookStats();
        var proj = new Projectile();
        int type = item.type;
        proj.SetDefaults(item.shoot);

        // Reach
        stats.Reach = proj.ModProjectile?.GrappleRange() ?? VanillaReach.GetValueOrDefault(item.type, -1f);

        // Number of hooks
        int predictedNumHooks = ProjectileID.Sets.SingleGrappleHook[item.shoot] ? 1 : -1;
        stats.NumHooks = VanillaNumHooks.GetValueOrDefault(type, predictedNumHooks);

        int numHooks = -1;
        ProjectileLoader.NumGrappleHooks(proj, Main.LocalPlayer, ref numHooks);
        if (numHooks != -1)
            stats.NumHooks = numHooks;

        // Latching
        var predictedLatching = ProjectileID.Sets.SingleGrappleHook[item.shoot] ? LatchingMode.Single : LatchingMode.Unknown;
        stats.Latching = VanillaLatching.GetValueOrDefault(type, predictedLatching);

        // Shoot speed
        stats.ShootSpeed = item.shootSpeed;

        // Retreat speed
        stats.RetreatSpeed = VanillaRetreatSpeed.GetValueOrDefault(item.type, -1f);

        float retreatSpeed = -1f;
        ProjectileLoader.GrappleRetreatSpeed(proj, Main.LocalPlayer, ref retreatSpeed);
        if (retreatSpeed != -1f)
            stats.RetreatSpeed = retreatSpeed;

        // Pull speed
        stats.PullSpeed = VanillaPullSpeed.GetValueOrDefault(item.type, -1f);

        float pullSpeed = -1f;
        ProjectileLoader.GrapplePullSpeed(proj, Main.LocalPlayer, ref pullSpeed);
        if (pullSpeed != -1f)
            stats.PullSpeed = pullSpeed;

        return stats;
    }

    public override void Apply(List<TooltipLine> tooltips)
    {
        // Reach
        if (Reach != -1)
            tooltips.Add(TooltipUtils.GetTooltipLine("HookStats.Reach", (decimal)MathUtils.Round(Reach / 16f, 0.1f)));
        else
            tooltips.Add(TooltipUtils.GetTooltipLine("HookStats.ReachUnknown"));

        // Number of hooks
        if (NumHooks != -1)
            tooltips.Add(TooltipUtils.GetTooltipLine("HookStats.NumHooks", NumHooks));
        else
            tooltips.Add(TooltipUtils.GetTooltipLine("HookStats.NumHooksUnknown"));

        // Latching mode
        tooltips.Add(
            TooltipUtils.GetTooltipLine(
                Latching switch
                {
                    LatchingMode.Single       => "HookStats.LatchingSingle",
                    LatchingMode.Individual   => "HookStats.LatchingIndividual",
                    LatchingMode.Simultaneous => "HookStats.LatchingSimultaneous",
                    _                         => "HookStats.LatchingUnknown",
                }
            )
        );

        // Speeds
        if (ShootSpeed != -1)
            tooltips.Add(TooltipUtils.GetTooltipLine("HookStats.ShootSpeed", (decimal)MathUtils.Round(ShootSpeed * MathUtils.PPTToMPH), 0.1f));
        else
            tooltips.Add(TooltipUtils.GetTooltipLine("HookStats.ShootSpeedUnknown"));

        if (RetreatSpeed != -1)
            tooltips.Add(TooltipUtils.GetTooltipLine("HookStats.RetreatSpeed", (decimal)MathUtils.Round(RetreatSpeed * MathUtils.PPTToMPH), 0.1f));
        else
            tooltips.Add(TooltipUtils.GetTooltipLine("HookStats.RetreatSpeedUnknown"));

        if (PullSpeed != -1)
            tooltips.Add(TooltipUtils.GetTooltipLine("HookStats.PullSpeed", (decimal)MathUtils.Round(PullSpeed * MathUtils.PPTToMPH), 0.1f));
        else
            tooltips.Add(TooltipUtils.GetTooltipLine("HookStats.PullSpeedUnknown"));
    }
}
