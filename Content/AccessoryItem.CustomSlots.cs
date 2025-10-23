namespace AccessoriesPlus.Content;

public partial class AccessoryItem
{
    /* TODO: add back
        // Forcing wings, etc. into modded slots
        private static bool ForceSlots(Item item, Player player, int slot, bool modded)
        {
            var wingSlot = ModContent.GetInstance<WingSlot>();
            var shieldSlot = ModContent.GetInstance<ShieldSlot>();
            var bootSlot = ModContent.GetInstance<BootSlot>();

            // Have to minus the number of accessory slots, since that is how many slots ahead the vanity one is, and subtracting it gets back to the original slot
            int vanitySlot = slot - player.GetModPlayer<ModAccessorySlotPlayer>().SlotCount;

            if (ServerConfig.Instance.SlotWings && ServerConfig.Instance.SlotForceWings && wingSlot.IsValidItem(item))
                return modded && (wingSlot.Type == slot || wingSlot.Type == vanitySlot);
            else if (ServerConfig.Instance.SlotShield && ServerConfig.Instance.SlotForceShields && shieldSlot.IsValidItem(item))
                return modded && (shieldSlot.Type == slot || shieldSlot.Type == vanitySlot);
            else if (ServerConfig.Instance.SlotBoots && ServerConfig.Instance.SlotForceBoots && bootSlot.IsValidItem(item))
                return modded && (bootSlot.Type == slot || bootSlot.Type == vanitySlot);

            return true;
        }
        */
}
