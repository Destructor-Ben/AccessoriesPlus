using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Content.StatTooltips;

public abstract class TooltipStats
{
    public virtual string LineNameToInsertAround => "Equipable";
    public virtual bool After => true;

    public abstract bool Enabled { get; }
    public abstract List<ItemDefinition> Whitelist { get; }
    public abstract List<ItemDefinition> Blacklist { get; }

    public TooltipStats? FetchStats(Item item)
    {
        if (!Enabled)
            return null;

        if ((!ItemMeetsDefaultCondition(item) && Whitelist.All(i => i.Type != item.type)) || Blacklist.Any(i => i.Type == item.type))
            return null;

        SetStatsFromItem(item);

        return this;
    }

    public abstract bool ItemMeetsDefaultCondition(Item item);

    protected abstract void SetStatsFromItem(Item item);

    public abstract IEnumerable<TooltipLine> GetTooltips();
}
