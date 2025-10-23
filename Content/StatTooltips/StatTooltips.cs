namespace AccessoriesPlus.Content.StatTooltips;

public class StatTooltips : GlobalItem
{
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        var stats = Stats.GetStats(item);
        if (stats == null)
            return;

        var statTooltips = new List<TooltipLine>();
        stats.Apply(statTooltips);
        tooltips.InsertTooltips(stats.LineNameToInsertAround, stats.After, statTooltips.ToArray());
    }
}
