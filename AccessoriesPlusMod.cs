using AccessoriesPlus.Config.CustomConfigStuff;
using AccessoriesPlus.Utilities.StuffToMoveToTerraUtil;
using ReLogic.Content.Sources;
using NetHandler = AccessoriesPlus.Content.NetHandler;

namespace AccessoriesPlus;

public class AccessoriesPlusMod : Mod
{
    // TODO: make a global static using class that makes the Instance and Logger fields of the mod class easily accessible globally
    public static AccessoriesPlusMod Instance => ModContent.GetInstance<AccessoriesPlusMod>();

    public AccessoriesPlusMod()
    {
        CustomConfigPatches.Load();
    }

    public override void Unload()
    {
        CustomConfigPatches.Unload();
    }

    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {
        NetHandler.HandlePacket(reader, whoAmI);
    }

    // TODO: move this to TerraUtil
    public override IContentSource CreateDefaultContentSource()
    {
        return new CustomContentSource(
            base.CreateDefaultContentSource(),
            new Dictionary<string, string>
            {
                ["Content"] = "Assets/Textures",
            }
        );
    }
}
