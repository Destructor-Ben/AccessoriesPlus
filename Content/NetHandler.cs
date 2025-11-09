using AccessoriesPlus.Content.Items;

namespace AccessoriesPlus.Content;

// TODO: proper packet system
// - The problems with tmls networking system are:
//  - Figuring out what packet handler should be used
//  - manually registering packet handlers/ids
//  - Separating the serializing and deserialising into functions
//  - The routing, (e.g. C2S, S2C, S2Cs, and C2S2Cs and C2C) could be abstracted, but it might be easier to just do it manually
//  - Either send to server, or send to all clients, and just make clients ignore the packet if it isn't for them
public static class NetHandler
{
    public static ModPacket GetPacket(PacketID id)
    {
        var packet = ModInstance.GetPacket();
        packet.Write((byte)id);
        return packet;
    }

    public static void HandlePacket(BinaryReader reader, int whoAmI)
    {
        var id = (PacketID)reader.ReadByte();

        switch (id)
        {
            case PacketID.SyncMoonLordHeart:
                MoonLordHeartPlayer.ReceiveMessage(reader, whoAmI);
                break;
            default:
                throw new Exception("Unknown packet id: " + id);
        }
    }
}
