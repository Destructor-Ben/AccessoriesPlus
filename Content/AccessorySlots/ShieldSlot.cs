using ServerConfig = AccessoriesPlus.Config.ServerConfig;

namespace AccessoriesPlus.Content.AccessorySlots;

public class ShieldSlot : SpecialAccessorySlot
{
    public override bool IsValidItem(Item item)
    {
        return ServerConfig.Instance.SlotShield.IsValidItem(item.shieldSlot > 0, item.type);
    }

    public override bool IsEnabled()
    {
        return ServerConfig.Instance.SlotShield.Enabled;
    }
}
