namespace AccessoriesPlus.Content.StatTooltips;

public abstract class Stats
{
    // TODO: refactor the insertion code
    public virtual string LineNameToInsertAround => "Equipable";
    public virtual bool After => true;

    public abstract void Apply(List<TooltipLine> tooltips);
}
