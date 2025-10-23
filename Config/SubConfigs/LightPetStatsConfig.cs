using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Config.SubConfigs;

public record LightPetStatsConfig
{
    public static LightPetStatsConfig Instance => ClientConfig.Instance.LightPetStatsConfig;

    public bool Enabled = true;

    public List<ItemDefinition> Whitelist = [];
    public List<ItemDefinition> Blacklist = [];

    public bool BrightnessTooltipEnabled = true;
    public bool ControllableTooltipEnabled = true;
    public bool ExposesTreasureTooltipEnabled = true;
    public bool ExposesEnemiesTooltipEnabled = true;

    public override string? ToString() => null;
}
