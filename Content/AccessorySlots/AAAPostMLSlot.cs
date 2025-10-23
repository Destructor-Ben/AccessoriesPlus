using AccessoriesPlus.Content.Items;

namespace AccessoriesPlus.Content.AccessorySlots;

// Triple A so it gets placed before other slots
// ReSharper disable once InconsistentNaming
public class AAAPostMLSlot : ModAccessorySlot
{
    public override bool IsEnabled()
    {
        return /* TODO: config ServerConfig.Instance.SlotMoonlord && */Main.hardMode && NPC.downedMoonlord && Player.GetModPlayer<MoonLordHeartPlayer>().HasExtraMoonLordAccessory;
    }
}
