namespace AccessoriesPlus.Content;

public partial class AccessoryNPC : GlobalNPC
{
    public override void ModifyShop(NPCShop shop)
    {
        ModifyObtainabilityShop(shop);
    }

    public override void SetupTravelShop(int[] shop, ref int nextSlot)
    {
        SetupObtainabilityTravelShop(shop, ref nextSlot);
    }

    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        ModifyObtainabilityLoot(npc, npcLoot);
    }
}
