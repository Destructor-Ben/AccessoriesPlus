using AccessoriesPlus.Config;
using AccessoriesPlus.Content.Items;

namespace AccessoriesPlus.Content.AccessorySlots;

// Triple A so it gets placed before other slots
// ReSharper disable once InconsistentNaming
public class AAAPostMLSlot : ModAccessorySlot
{
    public override bool IsEnabled()
    {
        return ServerConfig.Instance.SlotMoonLord && Main.expertMode && Player.GetModPlayer<MoonLordHeartPlayer>().HasExtraMoonLordAccessory;
    }
}
