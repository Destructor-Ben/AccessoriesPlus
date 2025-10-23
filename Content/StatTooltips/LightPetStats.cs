using AccessoriesPlus.Config;
using AccessoriesPlus.Utilities;

namespace AccessoriesPlus.Content.StatTooltips;

public class LightPetStats : Stats
{
    public float Brightness { get; private set; } = -1f;
    public bool Controllable { get; private set; } = false;
    public bool ExposesTreasure { get; private set; } = false;
    public bool ExposesEnemies { get; private set; } = false;

    private static Dictionary<int, LightPetStats> VanillaLightPetStats = new()
    {
        {
            ItemID.ShadowOrb, new LightPetStats
            {
                Brightness = 0.65f,
                Controllable = true
            }
        },
        { ItemID.CrimsonHeart, new LightPetStats { Brightness = 0.65f } },
        {
            ItemID.MagicLantern, new LightPetStats
            {
                Brightness = 0.65f,
                ExposesTreasure = true
            }
        },
        { ItemID.FairyBell, new LightPetStats { Brightness = 0.8f } },
        { ItemID.DD2PetGhost, new LightPetStats { Brightness = 0.8f } },
        {
            ItemID.WispinaBottle, new LightPetStats
            {
                Brightness = 1.2f,
                Controllable = true
            }
        },
        {
            ItemID.SuspiciousLookingTentacle, new LightPetStats
            {
                Brightness = 1.2f,
                ExposesTreasure = true,
                ExposesEnemies = true
            }
        },
        { ItemID.PumpkingPetItem, new LightPetStats { Brightness = 0.8f } },
        { ItemID.GolemPetItem, new LightPetStats { Brightness = 0.8f } },
        { ItemID.FairyQueenPetItem, new LightPetStats { Brightness = 1.2f } },
    };

    private LightPetStats() { }

    public static LightPetStats Get(Item item)
    {
        return !ClientConfig.Instance.LightPetStatsConfig.Enabled || item.shoot <= ProjectileID.None || !ProjectileID.Sets.LightPet[item.shoot]
            ? null
            : VanillaLightPetStats.GetValueOrDefault(item.type, new());
    }

    public override void Apply(List<TooltipLine> tooltips)
    {
        if (Brightness != -1f)
            tooltips.Add(TooltipUtils.GetTooltipLine("LightPetStats.Brightness", (int)(Brightness * 100f)));
        else
            tooltips.Add(TooltipUtils.GetTooltipLine("LightPetStats.BrightnessUnknown"));

        if (Controllable)
            tooltips.Add(TooltipUtils.GetTooltipLine("LightPetStats.Controllable"));

        if (ExposesEnemies)
            tooltips.Add(TooltipUtils.GetTooltipLine("LightPetStats.ExposesEnemies"));

        if (ExposesTreasure)
            tooltips.Add(TooltipUtils.GetTooltipLine("LightPetStats.ExposesTreasure"));
    }
}
