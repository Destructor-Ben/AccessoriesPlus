namespace AccessoriesPlus.Content.AccessorySlots;

public abstract class SpecialAccessorySlot : ModAccessorySlot
{
    public virtual LocalizedText FunctionalTooltip => Mod.GetLocalization($"AccessorySlots.{Name}.FunctionalTooltip");
    public virtual LocalizedText VanityTooltip => Mod.GetLocalization($"AccessorySlots.{Name}.VanityTooltip");

    public override string FunctionalTexture => $"AccessoriesPlus/Assets/Textures/AccessorySlots/{Name}_Functional";
    public override string VanityTexture => $"AccessoriesPlus/Assets/Textures/AccessorySlots/{Name}_Vanity";

    public override void OnMouseHover(AccessorySlotType context)
    {
        if (context == AccessorySlotType.FunctionalSlot)
            Main.hoverItemName = FunctionalTooltip.Value;

        if (context == AccessorySlotType.VanitySlot)
            Main.hoverItemName = VanityTooltip.Value;
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
