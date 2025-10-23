using Terraria.GameContent.ItemDropRules;

namespace AccessoriesPlus.Content.Items;

// TODO: make the texture the moonlord heart, also just copy the Demon Heart
public class MoonLordHeart : ModItem
{
    // TODO: automatically direct textures to assets folder, also make this a TerraUtil feature
    public override string Texture => "AccessoriesPlus/Assets/Textures/Items/MoonLordHeart";

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.DemonHeart);
        Item.master = true;
        Item.value = Item.sellPrice(0, 5);
    }

    public override bool CanUseItem(Player player)
    {
        return Main.masterMode && !player.GetModPlayer<MoonLordHeartPlayer>().HasExtraMoonLordAccessory;
    }

    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<MoonLordHeartPlayer>().HasExtraMoonLordAccessory = true;
        NetMessage.SendData(MessageID.SyncPlayer, number: player.whoAmI);
        return true;
    }
}

public class MoonLordHeartPlayer : ModPlayer
{
    // TODO: save, sync, etc -> reference EM
    public bool HasExtraMoonLordAccessory = false;
}

public class MoonLordHeartLoot : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation)
    {
        return entity.type == ItemID.MoonLordBossBag;
    }

    public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.ByCondition(new NotUsedMoonLordHeartCondition(), ModContent.ItemType<MoonLordHeart>()));
    }
}

public class NotUsedMoonLordHeartCondition : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return !info.player.GetModPlayer<MoonLordHeartPlayer>().HasExtraMoonLordAccessory;
    }

    public bool CanShowItemDropInUI()
    {
        return true;
    }

    public string? GetConditionDescription()
    {
        return null;
    }
}
