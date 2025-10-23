namespace AccessoriesPlus.Content.StatTooltips;

public abstract class Stats
{
    public virtual string LineNameToInsertAround => "Equipable";
    public virtual bool After => true;

    public abstract void Apply(List<TooltipLine> tooltips);

    public static Stats GetStats(Item item)
    {
        var wingStats = WingStats.Get(item);
        var hookStats = HookStats.Get(item);
        var lightPetStats = LightPetStats.Get(item);
        var mountStats = MountStats.Get(item);

        return wingStats ?? hookStats ?? lightPetStats ?? mountStats ?? (Stats)null;
    }
}
