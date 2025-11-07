using AccessoriesPlus.Config.CustomConfigStuff;
using Terraria.ModLoader.Config;

// ReSharper disable CollectionNeverUpdated.Global

namespace AccessoriesPlus.Config.SubConfigs;

[SubConfig]
public record CustomAccessorySlotConfig
{
    public bool Enabled = true;

    public List<ItemDefinition> Whitelist = [];
    public List<ItemDefinition> Blacklist = [];

    public bool IsValidItem(bool fitsAutomaticCondition, int type)
    {
        return (fitsAutomaticCondition || Whitelist.Any(item => item.Type == type)) && Blacklist.All(item => item.Type != type);
    }
}
