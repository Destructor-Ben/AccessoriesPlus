namespace AccessoriesPlus.Content;

public partial class AccessoryItem : GlobalItem
{
    public override void SetStaticDefaults()
    {
        ReplaceBOBTexture();
        ModifyShimmerCrafting();
    }

    public override void Unload()
    {
        UndoReplaceBOBTexture();
    }

    public override void SetDefaults(Item entity)
    {
        ModifyAccessoryStats(entity);
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        ModifyAccessoryEffects(item, player, hideVisual);
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        ModifyAccessoryTooltips(item, tooltips);
    }

    public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
        ModifyPresentLoot(item, itemLoot);
    }

    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        return ForceSlots(item, player, slot, modded);
    }
}
