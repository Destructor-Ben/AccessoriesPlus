using AccessoriesPlus.Config.CustomConfigStuff;
using AccessoriesPlus.Utilities.StuffToMoveToTerraUtil;
using log4net;
using ReLogic.Content.Sources;
using NetHandler = AccessoriesPlus.Content.NetHandler;

namespace AccessoriesPlus;

public static class GlobalUsings
{
    public static AccessoriesPlusMod ModInstance => ModContent.GetInstance<AccessoriesPlusMod>();
    public static ILog ModLogger => ModInstance.Logger;
}

public class AccessoriesPlusMod : Mod
{
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
