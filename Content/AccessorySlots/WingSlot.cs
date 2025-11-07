using ServerConfig = AccessoriesPlus.Config.ServerConfig;

namespace AccessoriesPlus.Content.AccessorySlots;

public class WingSlot : SpecialAccessorySlot
{
    public override bool IsValidItem(Item item)
    {
        return ServerConfig.Instance.SlotWings.IsValidItem(item.wingSlot > 0, item.type);
    }

    public override bool IsEnabled()
    {
        return ServerConfig.Instance.SlotWings.Enabled;
    }
}
