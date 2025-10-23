using AccessoriesPlus.Content.InfoDisplays;
using AccessoriesPlus.Utilities;
using PDAConfig = AccessoriesPlus.Config.SubConfigs.PDAConfig;

namespace AccessoriesPlus.Content.Edits;

public static class PDATintDetour
{
    public static void Apply()
    {
        On_NPC.GetNPCColorTintedByBuffs += delegate(On_NPC.orig_GetNPCColorTintedByBuffs orig, NPC self, Color npcColor)
        {
            var originalColor = orig(self, npcColor);

            // TODO: move the InfoDisplayActive util into a util file
            if (!ModContent.GetInstance<LifeformAnalyzerTweaks>().LifeformAnalyzerNPCs.Contains(self) || !InfoDisplayUtils.IsActive(InfoDisplay.LifeformAnalyzer) || !PDAConfig.Instance.LifeformAnalyzerHighlight)
                return originalColor;

            // TODO: test the updated colour
            const byte MinR = byte.MaxValue, MinG = byte.MaxValue, MinB = 50;

            if (npcColor.R < MinR)
                npcColor.R = MinR;

            if (npcColor.G < MinG)
                npcColor.G = MinG;

            if (npcColor.B < MinB)
                npcColor.B = MinB;

            return npcColor;
        };
    }
}
