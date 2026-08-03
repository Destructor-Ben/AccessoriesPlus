using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Config.Patches;

// TODO: move to TerraUtil?
// Remove the need for [DefaultValue] in configs
public static class RemoveDefaultValue
{
    private static MethodInfo Method => typeof(ReferenceDefaultsPreservingResolver).GetMethod("CreateProperties", BindingFlags.NonPublic | BindingFlags.Instance)!;

    public static void Load()
    {
        MonoModHooks.Add(Method, OnCreateProperties);
    }

    // ReSharper disable once InconsistentNaming
    private delegate IList<JsonProperty> orig_CreateProperties(ReferenceDefaultsPreservingResolver self, Type type, MemberSerialization memberSerialization);

    private static IList<JsonProperty> OnCreateProperties(orig_CreateProperties orig, ReferenceDefaultsPreservingResolver self, Type type, MemberSerialization memberSerialization)
    {
        // Copy pasted from tML and modified
        var properties = orig(self, type, memberSerialization);

        if (!type.IsAssignableTo(typeof(CustomConfig)) || !type.IsClass)
        {
            return properties;
        }

        var ctor = type.GetConstructor(Type.EmptyTypes);

        if (ctor == null) {
            return properties;
        }

        // The instance of the config containing default values
        object referenceInstance = ctor.Invoke(null);

        foreach (var property in properties.Where(p => p.Readable)) {
            if (property.PropertyType == null)
                continue;

            if (!property.PropertyType.IsValueType || property.Ignored)
            {
                continue;
            }

            property.DefaultValue = property.ValueProvider!.GetValue(referenceInstance);
        }

        return properties;
    }
}
