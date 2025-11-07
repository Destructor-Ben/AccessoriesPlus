using ServerConfig = AccessoriesPlus.Config.ServerConfig;

namespace AccessoriesPlus.Content.AccessorySlots;

public class BootSlot : SpecialAccessorySlot
{
    public override bool IsValidItem(Item item)
    {
        return ServerConfig.Instance.SlotBoots.IsValidItem(item.shoeSlot > 0, item.type);
    }

    public override bool IsEnabled()
    {
        return ServerConfig.Instance.SlotBoots.Enabled;
    }
}
