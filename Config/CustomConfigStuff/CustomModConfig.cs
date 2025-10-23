using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Config.CustomConfigStuff;

public abstract class CustomModConfig : ModConfig
{
    // TODO: recursive ReloadRequired
    // TODO: ensure all of the sub configs work properly - I think they do apart from recursive reload required
    // TODO: default to not expanded for objects
}
