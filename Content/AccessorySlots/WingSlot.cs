using ServerConfig = AccessoriesPlus.Config.ServerConfig;

namespace AccessoriesPlus.Content.AccessorySlots;

public class WingSlot : AbstractAccessorySlot
{
    public override int FunctionalItemID => ItemID.CreativeWings;
    public override int VanityItemID => ItemID.WingsSolar;

    public override bool IsValidItem(Item item)
    {
        return item.wingSlot > 0;
    }

    public override bool IsEnabled()
    {
        return ServerConfig.Instance.SlotWings;
    }
}
