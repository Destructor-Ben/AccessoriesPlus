using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.IO;

namespace AccessoriesPlus.Content.Items;

public class MoonLordHeart : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.DemonHeart);
        Item.value = Item.sellPrice(0, 5);
    }

    public override bool CanUseItem(Player player)
    {
        return Main.expertMode && !player.GetModPlayer<MoonLordHeartPlayer>().HasExtraMoonLordAccessory;
    }

    public override bool? UseItem(Player player)
    {
        var modPlayer = player.GetModPlayer<MoonLordHeartPlayer>();
        modPlayer.HasExtraMoonLordAccessory = true;
        return true;
    }
}

public class MoonLordHeartPlayer : ModPlayer
{
    public bool HasExtraMoonLordAccessory = false;

    public override void SaveData(TagCompound tag)
    {
        tag[nameof(HasExtraMoonLordAccessory)] = HasExtraMoonLordAccessory;
    }

    public override void LoadData(TagCompound tag)
    {
        HasExtraMoonLordAccessory = tag.GetBool(nameof(HasExtraMoonLordAccessory));
    }

    // TODO: not needed if SendClientChanges not needed? or maybe clientClone is used for more than networking
    public override void CopyClientState(ModPlayer targetCopy)
    {
        var clone = (MoonLordHeartPlayer)targetCopy;
        clone.HasExtraMoonLordAccessory = HasExtraMoonLordAccessory;
    }

    // TODO: is this needed? item.UseItem should be run on all clients + the server
    public override void SendClientChanges(ModPlayer clientPlayer)
    {
        var clone = (MoonLordHeartPlayer)clientPlayer;
        if (clone.HasExtraMoonLordAccessory == HasExtraMoonLordAccessory)
            return;

        SyncPlayer(-1, Main.myPlayer, false);
    }

    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
        var packet = NetHandler.GetPacket(PacketID.SyncMoonLordHeart);
        packet.Write((byte)Player.whoAmI);
        packet.Write(HasExtraMoonLordAccessory);
        packet.Send(toWho, fromWho);
    }

    public static void ReceiveMessage(BinaryReader reader, int whoAmI)
    {
        byte targetPlayer = reader.ReadByte();
        var modPlayer = Main.player[targetPlayer].GetModPlayer<MoonLordHeartPlayer>();
        modPlayer.HasExtraMoonLordAccessory = reader.ReadBoolean();

        // Forward to clients
        if (Main.netMode == NetmodeID.Server)
            modPlayer.SyncPlayer(-1, whoAmI, false);
    }
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
