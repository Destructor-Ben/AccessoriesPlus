using AccessoriesPlus.Utilities;
using TerraUtil.Edits;

namespace AccessoriesPlus.Content.Edits;

public class PDATintDetour : Detour
{
    public override void Apply()
    {
        On_NPC.GetNPCColorTintedByBuffs += delegate(On_NPC.orig_GetNPCColorTintedByBuffs orig, NPC self, Color npcColor)
        {
            var originalColor = orig(self, npcColor);

            // TODO: move the InfoDisplayActive util into a util file
            if (!AccessoryInfoDisplay.LifeformAnalyzerNPCs.Contains(self) || !InfoDisplayUtils.IsActive(InfoDisplay.LifeformAnalyzer) || !PDAConfig.Instance.LifeformAnalyzerHighlight)
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
