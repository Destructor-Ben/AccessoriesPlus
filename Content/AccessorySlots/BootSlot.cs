namespace AccessoriesPlus.Content.AccessorySlots;

public class BootSlot : AbstractAccessorySlot
{
    public override int FunctionalItemID => ItemID.HermesBoots;
    public override int VanityItemID => ItemID.TerrasparkBoots;

    public override bool IsValidItem(Item item)
    {
        return item.shoeSlot > 0;
    }

    public override bool IsEnabled()
    {
        return ServerConfig.Instance.SlotBoots;
    }
}
