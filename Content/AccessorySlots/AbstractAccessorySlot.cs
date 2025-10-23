namespace AccessoriesPlus.Content.AccessorySlots;

// TODO: rename to something like SpecialAccessorySlot
public abstract class AbstractAccessorySlot : ModAccessorySlot
{
    public virtual LocalizedText FunctionalTooltip => Mod.GetLocalization($"AccessorySlots.{GetType().Name}.FunctionalTooltip");
    public virtual LocalizedText VanityTooltip => Mod.GetLocalization($"AccessorySlots.{GetType().Name}.VanityTooltip");

    public abstract int FunctionalItemID { get; }
    public abstract int VanityItemID { get; }
    public virtual int FunctionalColour => -1;
    public virtual int VanityColour => -1;

    // TODO: custom functional/vanity textures
    public override string FunctionalTexture => "Terraria/Images/Item_" + (FunctionalItemID == -1 ? 0 : FunctionalItemID);
    public override string VanityTexture => "Terraria/Images/Item_" + (VanityItemID == -1 ? 0 : VanityItemID);
    public override string FunctionalBackgroundTexture => "Terraria/Images/Inventory_Back" + (FunctionalColour == -1 ? 0 : FunctionalColour);
    public override string VanityBackgroundTexture => "Terraria/Images/Inventory_Back" + (VanityColour == -1 ? 0 : VanityColour);

    public override void OnMouseHover(AccessorySlotType context)
    {
        switch (context)
        {
            case AccessorySlotType.FunctionalSlot:
                Main.hoverItemName = FunctionalTooltip.Value;
                break;
            case AccessorySlotType.VanitySlot:
                Main.hoverItemName = VanityTooltip.Value;
                break;
        }
    }

    public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
    {
        return IsValidItem(item);
    }

    public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
    {
        return IsValidItem(checkItem);
    }

    public abstract bool IsValidItem(Item item);
}
