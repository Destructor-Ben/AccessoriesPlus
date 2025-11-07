using AccessoriesPlus.Config.SubConfigs;

namespace AccessoriesPlus.Content.InfoDisplays;

public class LifeformAnalyzerTweaks : GlobalInfoDisplay
{
    public List<NPC> LifeformAnalyzerNPCs = [];
    public NPC? BestNPC = null;

    public override void ModifyDisplayParameters(InfoDisplay currentDisplay, ref string displayValue, ref string displayName, ref Color displayColor, ref Color displayShadowColor)
    {
        if (currentDisplay != InfoDisplay.LifeformAnalyzer)
            return;

        // Disabling vanilla search
        Main.LocalPlayer.accCritterGuideCounter = 15;

        if (Main.GameUpdateCount % 15 == 0)
            SearchNPCS();

        if (PDAConfig.Instance.LifeformAnalyzerDistanceInfo && (BestNPC?.active ?? false))
        {
            var npc = Main.npc[Main.LocalPlayer.accCritterGuideNumber];
            displayValue = "TODO"; // TODO: fix Mods.AccessoriesPlus.InfoDisplays.FoundRareCreature.GetTextValue(npc.GivenOrTypeName, (int)MathUtils.Round(npc.Distance(Main.LocalPlayer.Center) / 16f));

            displayColor = Main.MouseTextColorReal;
            displayShadowColor = Color.Black;

            if (NPCID.Sets.GoldCrittersCollection.Contains(BestNPC.type))
            {
                displayColor = InfoDisplay.GoldInfoTextColor;
                displayShadowColor = InfoDisplay.GoldInfoTextShadowColor;
            }
        }
    }

    private void SearchNPCS()
    {
        BestNPC = null;
        LifeformAnalyzerNPCs.Clear();

        // Finding all rare npcs
        foreach (var npc in Main.npc)
        {
            bool npcInWhitelist = PDAConfig.Instance.UseNPCWhitelist && PDAConfig.Instance.NPCWhitelist.Where(n => n.Type == npc.type).Any();
            bool npcInBlacklist = PDAConfig.Instance.UseNPCBlacklist && PDAConfig.Instance.NPCBlacklist.Where(n => n.Type == npc.type).Any();
            if (npc.active && (npc.rarity > 0 || npcInWhitelist) && !npcInBlacklist && npc.Distance(Main.LocalPlayer.Center) <= 1300f)
                LifeformAnalyzerNPCs.Add(npc);
        }

        // Finding rarest npc
        foreach (var npc in LifeformAnalyzerNPCs)
        {
            if (npc.rarity > (BestNPC?.rarity ?? -1))
                BestNPC = npc;
        }

        Main.LocalPlayer.accCritterGuideNumber = (byte)(BestNPC?.whoAmI ?? -1);
    }
}
