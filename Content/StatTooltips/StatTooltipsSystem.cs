namespace AccessoriesPlus.Content.StatTooltips;

public class StatTooltipsSystem : GlobalItem
{
    public static ModKeybind RevealStatsKeybind { get; private set; } = null!;

    public override void Load()
    {
        RevealStatsKeybind = KeybindLoader.RegisterKeybind(Mod, "RevealStatsKeybind", "LeftAlt");
    }

    public override void Unload()
    {
        RevealStatsKeybind = null!;
    }

    public static TooltipStats? GetStats(Item item)
    {
        return new WingStats().FetchStats(item)
               // ?? new HookStats().FetchStats(item)
            ?? new LightPetStats().FetchStats(item);
        // ?? new MountStats().FetchStats(item);
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        var stats = GetStats(item);
        if (stats is null)
            return;

        var statTooltips = stats.GetTooltips().ToArray();
        stats.InsertTooltips(tooltips, statTooltips);
    }
}
