using AccessoriesPlus.Config.CustomConfigStuff;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Config.SubConfigs;

[SubConfig]
public record PDAConfig
{
    public static PDAConfig Instance => ServerConfig.Instance.ImprovedPDA;

    [Header("Radar")]
    public bool RadarHighlightEnemies = true;
    public bool RadarHighlightDanger = true;

    [Header("MetalDetector")]
    public bool MetalDetectorDistanceInfo = true;
    public bool MetalDetectorArrows = true;
    public bool MetalDetectorHighlight = true;
    [ReloadRequired]
    public bool TrackGems = true;
    [ReloadRequired]
    public bool TrackHellstone = true;
    // TODO: add whitelist and blacklist for metal detector when TileDefinition finally comes around

    [Header("LifeformAnalyzer")]
    public bool LifeformAnalyzerDistanceInfo = true;
    public bool LifeformAnalyzerArrows = true;
    public bool LifeformAnalyzerHighlight = true;

    // TODO: are these 2 needed?
    public bool UseNPCWhitelist = true;
    public bool UseNPCBlacklist = true;

    //public Dictionary<NPCDefinition, int> NPCWhitelist = new();
    // TODO: add rarities to NPC whitelist
    public List<NPCDefinition> NPCWhitelist = [];
    public List<NPCDefinition> NPCBlacklist = [];
}
