namespace AccessoriesPlus.Utilities;

public static class ItemUtils
{
    /// <summary>
    /// Applies the equip effects of the given item to the given player.
    /// </summary>
    /// <param name="player">The player to apply the effects to.</param>
    /// <param name="itemType">The item type that will have its effects applied.</param>
    /// <param name="hideVisual">Whether visual effects will be applied.</param>
    public static void CopyVanillaEquipEffects(this Player player, int itemType, bool hideVisual = false)
    {
        var item = new Item();
        item.SetDefaults(itemType);
        player.ApplyEquipFunctional(item, hideVisual);
    }
}
