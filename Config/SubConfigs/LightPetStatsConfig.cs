using AccessoriesPlus.Config.Elements;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Config.SubConfigs;

[CustomModConfigItem(typeof(CustomObjectElement))]
public record LightPetStatsConfig
{
    public static LightPetStatsConfig Instance => ClientConfig.Instance.LightPetStatsConfig;

    public bool Enabled = true;

    public List<ItemDefinition> Whitelist = [];
    public List<ItemDefinition> Blacklist = [];

    [Header("TooltipLines")]
    public bool BrightnessTooltipEnabled = true;
    public bool ControllableTooltipEnabled = true;
    public bool ExposesTreasureTooltipEnabled = true;
    public bool ExposesEnemiesTooltipEnabled = true;
}
