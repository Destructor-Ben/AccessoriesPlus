using AccessoriesPlus.Config.SubConfigs;
using AccessoriesPlus.Utilities;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Content.StatTooltips;

public class LightPetStats : TooltipStats
{
    private static LightPetStatsConfig Config => LightPetStatsConfig.Instance;

    public float? Brightness { get; private set; } = null;
    public bool Controllable { get; private set; } = false;
    public bool ExposesTreasure { get; private set; } = false;
    public bool ExposesEnemies { get; private set; } = false;

    private static readonly Dictionary<int, LightPetStats> VanillaLightPetStats = new()
    {
        {
            ItemID.ShadowOrb, new LightPetStats
            {
                Brightness = 0.65f,
                Controllable = true,
            }
        },
        { ItemID.CrimsonHeart, new LightPetStats { Brightness = 0.65f } },
        {
            ItemID.MagicLantern, new LightPetStats
            {
                Brightness = 0.65f,
                ExposesTreasure = true,
            }
        },
        { ItemID.FairyBell, new LightPetStats { Brightness = 0.8f } },
        { ItemID.DD2PetGhost, new LightPetStats { Brightness = 0.8f } },
        {
            ItemID.WispinaBottle, new LightPetStats
            {
                Brightness = 1.2f,
                Controllable = true,
            }
        },
        {
            ItemID.SuspiciousLookingTentacle, new LightPetStats
            {
                Brightness = 1.2f,
                ExposesTreasure = true,
                ExposesEnemies = true,
            }
        },
        { ItemID.PumpkingPetItem, new LightPetStats { Brightness = 0.8f } },
        { ItemID.GolemPetItem, new LightPetStats { Brightness = 0.8f } },
        { ItemID.FairyQueenPetItem, new LightPetStats { Brightness = 1.2f } },
    };

    public override bool Enabled => Config.Enabled;
    public override List<ItemDefinition> Whitelist => Config.Whitelist;
    public override List<ItemDefinition> Blacklist => Config.Blacklist;

    public override bool ItemMeetsDefaultCondition(Item item)
    {
        // Fairy bell spawns a random projectile each time, so item.shoot test doesn't work on it
        return (item.shoot > ProjectileID.None && ProjectileID.Sets.LightPet[item.shoot]) || item.type == ItemID.FairyBell;
    }

    protected override void SetStatsFromItem(Item item)
    {
        if (!VanillaLightPetStats.TryGetValue(item.type, out var stats))
            return;

        Brightness = stats.Brightness;
        Controllable = stats.Controllable;
        ExposesTreasure = stats.ExposesTreasure;
        ExposesEnemies = stats.ExposesEnemies;
    }

    public override IEnumerable<TooltipLine> GetTooltips()
    {
        if (Config.BrightnessTooltipEnabled)
        {
            if (Brightness is not null)
                yield return TooltipUtils.GetTooltipLine("LightPetStats.Brightness", (int)(Brightness * 100f));
            else
                yield return TooltipUtils.GetTooltipLine("LightPetStats.BrightnessUnknown");
        }

        if (Controllable && Config.ControllableTooltipEnabled)
            yield return TooltipUtils.GetTooltipLine("LightPetStats.Controllable");

        if (ExposesEnemies && Config.ExposesEnemiesTooltipEnabled)
            yield return TooltipUtils.GetTooltipLine("LightPetStats.ExposesEnemies");

        if (ExposesTreasure && Config.ExposesTreasureTooltipEnabled)
            yield return TooltipUtils.GetTooltipLine("LightPetStats.ExposesTreasure");
    }
}
