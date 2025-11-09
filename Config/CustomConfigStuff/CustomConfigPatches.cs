using System.Reflection;
using AccessoriesPlus.Utilities;
using MonoMod.Cil;
using Newtonsoft.Json;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;

namespace AccessoriesPlus.Config.CustomConfigStuff;

file static class ConfigExtensionMethods
{
    public static bool IsCustomConfig(this Type? type)
    {
        return type?.IsAssignableTo(typeof(CustomModConfig)) ?? false;
    }

    public static bool IsCustomConfig(this object? obj)
    {
        return obj?.GetType().IsCustomConfig() ?? false;
    }

    public static bool IsInCustomConfig(this PropertyFieldWrapper field)
    {
        return field.MemberInfo.DeclaringType.IsCustomConfig();
    }

    public static bool IsInCustomConfig(this ConfigElement element)
    {
        return element.MemberInfo.IsInCustomConfig();
    }
}

// These patches are loaded in the mod ctor, since configs get loaded shortly after that
public static class CustomConfigPatches
{
    public static JsonSerializerSettings JsonSettings = null!;
    public static JsonSerializerSettings JsonSettingsCompact = null!;

    // ReSharper disable once InconsistentNaming
    private static FieldInfo ConfigManager_serializerSettings = null!;

    public static void Load()
    {
        JsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            NullValueHandling = NullValueHandling.Ignore,
        };

        JsonSettingsCompact = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            NullValueHandling = NullValueHandling.Ignore,
        };

        ConfigManager_serializerSettings = typeof(ConfigManager).GetField("serializerSettings", BindingFlags.Public | BindingFlags.Static)
                                        ?? throw new NullReferenceException();

        MonoModHooks.Modify(
            typeof(ObjectElement).GetMethod(nameof(ObjectElement.OnBind), BindingFlags.Instance | BindingFlags.Public),
            ILUtils.CreateILPatch(PatchObjectElementOnBind)
        );

        MonoModHooks.Modify(
            typeof(ConfigElement).GetMethod(nameof(ConfigElement.OnBind), BindingFlags.Instance | BindingFlags.Public),
            ILUtils.CreateILPatch(PatchConfigElementOnBind)
        );

        // Patch the json handling methods so I can do it myself
        MonoModHooks.Modify(
            typeof(ConfigManager).GetMethod(nameof(ConfigManager.Load), BindingFlags.Static | BindingFlags.NonPublic),
            ILUtils.CreateILPatch(PatchConfigManagerLoad)
        );

        MonoModHooks.Modify(
            typeof(ConfigManager).GetMethod(nameof(ConfigManager.Save), BindingFlags.Static | BindingFlags.NonPublic),
            ILUtils.CreateILPatch(PatchConfigManagerSave)
        );

        MonoModHooks.Modify(
            typeof(ConfigManager).GetMethod(nameof(ConfigManager.Reset), BindingFlags.Static | BindingFlags.NonPublic),
            ILUtils.CreateILPatch(PatchConfigManagerReset)
        );

        MonoModHooks.Modify(
            typeof(ConfigManager).GetMethod(nameof(ConfigManager.GeneratePopulatedClone), BindingFlags.Static | BindingFlags.Public),
            ILUtils.CreateILPatch(PatchConfigManagerClone)
        );
    }

    public static void Unload()
    {
        JsonSettings = null!;
        JsonSettingsCompact = null!;
        ConfigManager_serializerSettings = null!;
    }

    private static void PatchObjectElementOnBind(ILCursor c)
    {
        // Emit code right at the start to default expanded to false (but only if the element is in a CustomModConfig)
        c.EmitLdarg0();
        c.EmitDelegate(static (ObjectElement self) =>
            {
                if (!self.IsInCustomConfig())
                    return;

                self.expanded = false;
            }
        );

        // Make the 'hasToString' variable false for my custom types to get rid of the ToString stuff
        for (int j = 0; j < 2; j++)
        {
            c.GotoNext(MoveType.After, i => i.MatchLdstr("ToString"));
        }

        c.Index += 6;

        c.EmitLdarg0();
        c.EmitDelegate(static (bool orig, ObjectElement self) =>
            {
                if (!self.IsInCustomConfig())
                    return orig;

                return false;
            }
        );

        // Make the expand/collapse button further to the right
        c.GotoNext(MoveType.After, i => i.MatchLdcR4(-52f));
        c.EmitLdarg0();
        c.EmitDelegate(static (float orig, ObjectElement self) =>
            {
                if (!self.IsInCustomConfig())
                    return orig;

                return -26f;
            }
        );
    }

    private static void PatchConfigElementOnBind(ILCursor c)
    {
        // Make the "Reload Required" text addition work in nested elements
        c.GotoNext(MoveType.Before, i => i.MatchStloc1());
        c.Index -= 6;

        // TODO: this works on all configs, including non custom ones, but oh well
        /* TODO: finish fixing
        var label = c.DefineLabel();
        c.EmitBr(label);
        c.Index += 9;
        c.MarkLabel(label);

        // Fix the MemberInfo.GetValue call
        c.GotoNext(MoveType.Before, i => i.MatchRet());
        c.Index -= 6;

        // TODO: same issue as anove, works on non custom configs (including other mods)
        c.EmitLdarg0();
        c.EmitDelegate((ConfigElement self) =>
        // TODO: how the fuck do we get the value from the load time config?
        {
        });*/
    }

    // TODO: test this properly
    private static void PatchConfigManagerLoad(ILCursor c)
    {
        c.EmitLdarg0();
        c.EmitDelegate((ModConfig config) =>
            {
                if (!config.IsCustomConfig())
                    return true;

                // I'll do it my way!
                // Copied from ConfigManager.Load
                string filename = config.Mod.Name + "_" + config.Name + ".json";
                string path = Path.Combine(ConfigManager.ModConfigPath, filename);

                if (config.Mode == ConfigScope.ServerSide && ModNet.NetReloadActive)
                {
                    // #999: Main.netMode isn't 1 at this point due to #770 fix.
                    string netJson = ModNet.pendingConfigs.Single(x => x.modname == config.Mod.Name && x.configname == config.Name).json;
                    JsonConvert.PopulateObject(netJson, config, JsonSettings);
                    return false;
                }

                bool jsonFileExists = File.Exists(path);

                try
                {
                    if (jsonFileExists)
                    {
                        string json = File.ReadAllText(path);
                        JsonConvert.PopulateObject(json, config, JsonSettings);
                    }
                    else
                    {
                        ConfigManager.Reset(config);
                    }
                }
                catch (Exception e) when (jsonFileExists && (e is JsonReaderException || e is JsonSerializationException))
                {
                    Logging.tML.Warn($"Then config file {config.Name} from the mod {config.Mod.Name} located at {path} failed to load. The file was likely corrupted somehow, so the defaults will be loaded and the file deleted.");
                    File.Delete(path);
                    ConfigManager.Reset(config);
                }

                return false;
            }
        );

        var label = c.DefineLabel();
        c.EmitBrtrue(label);

        c.EmitRet();

        c.MarkLabel(label);
    }

    // TODO: test this properly
    private static void PatchConfigManagerSave(ILCursor c)
    {
        c.GotoNext(MoveType.After, i => i.MatchLdsfld(ConfigManager_serializerSettings));
        c.EmitLdarg0();
        c.EmitDelegate((JsonSerializerSettings orig, ModConfig config) =>
            {
                if (!config.IsCustomConfig())
                    return orig;

                return JsonSettings;
            }
        );
    }

    // TODO: test this properly
    private static void PatchConfigManagerReset(ILCursor c)
    {
        c.EmitLdarg0();
        c.EmitDelegate((ModConfig config) =>
            {
                if (!config.IsCustomConfig())
                    return false;

                // Do my method of resetting
                var ctor = config.GetType().GetConstructor([])
                        ?? throw new NullReferenceException();

                var defaultConfig = (ModConfig)ctor.Invoke([]);
                string json = JsonConvert.SerializeObject(defaultConfig, JsonSettingsCompact);
                JsonConvert.PopulateObject(json, config, JsonSettingsCompact);
                return true;
            }
        );

        var label = c.DefineLabel();
        c.EmitBrfalse(label);

        c.EmitRet();

        c.MarkLabel(label);
    }

    // TODO: test this properly
    private static void PatchConfigManagerClone(ILCursor c)
    {
        c.EmitLdarg0();
        c.EmitDelegate((ModConfig config) =>
            {
                if (!config.IsCustomConfig())
                    return null;

                // Do my method of cloning
                string json = JsonConvert.SerializeObject(config, JsonSettingsCompact);
                object? clone = JsonConvert.DeserializeObject(json, config.GetType(), JsonSettingsCompact);
                if (clone is null)
                    throw new NullReferenceException();

                var cloneConfig = (ModConfig)clone;
                cloneConfig.Name = config.Name;
                cloneConfig.Mod = config.Mod;
                return cloneConfig;
            }
        );

        var label = c.DefineLabel();
        c.EmitDup(); // Make a copy so the top one can be compared, and the bottom one returned
        c.EmitLdnull();
        c.EmitCeq();
        c.EmitBrtrue(label);

        c.EmitRet();

        c.MarkLabel(label);
        c.EmitPop(); // Pop off the extra copy of the config that was going to be returned
    }

    // TODO: the methods needed patching below are all for networking
    // TODO: patch ModConfig.SaveChanges, ConfigManager.HandleInGameChangeConfigPacket, ModNet.SendServerConfigs, ModNet.SyncClientMods, ModNet.AnyModNeedsReloadFromNetConfigsCheckOnly
}
