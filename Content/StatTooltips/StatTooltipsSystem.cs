using AccessoriesPlus.Utilities;

namespace AccessoriesPlus.Content.StatTooltips;

public class StatTooltipsSystem : GlobalItem
{
    public static Stats? GetStats(Item item)
    {
        return (Stats?)WingStats.Get(item)
            ?? (Stats?)HookStats.Get(item)
            ?? (Stats?)LightPetStats.Get(item)
            ?? (Stats?)MountStats.Get(item)
            ?? null;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        var stats = GetStats(item);
        if (stats is null)
            return;

        var statTooltips = new List<TooltipLine>();
        stats.Apply(statTooltips);
        tooltips.InsertTooltips(stats.LineNameToInsertAround, stats.After, statTooltips.ToArray());
    }
}
