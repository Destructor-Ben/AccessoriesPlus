using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Config.CustomConfigStuff;

public abstract class CustomModConfig : ModConfig
{
    // Recursive reload required!
    public override bool NeedsReload(ModConfig pendingConfig)
    {
        return ObjectNeedsReload(this, pendingConfig);
    }

    private static bool ObjectNeedsReload(object a, object b)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var variable in ConfigManager.GetFieldsAndProperties(a))
        {
            // Nothing is different
            if (ConfigManager.ObjectEquals(variable.GetValue(a), variable.GetValue(b)))
                continue;

            // This field has reload required and has changed, so we need a reload
            var reloadRequired = ConfigManager.GetCustomAttributeFromMemberThenMemberType<ReloadRequiredAttribute>(variable, a, null);
            if (reloadRequired != null)
                return true;

            // This field itself might not be reload required, but its children might be reload required
            // We only check this if it's a sub config to avoid complexities
            var subConfigAttribute = ConfigManager.GetCustomAttributeFromMemberThenMemberType<SubConfigAttribute>(variable, a, null);
            if (subConfigAttribute != null && ObjectNeedsReload(variable.GetValue(a), variable.GetValue(b)))
                return true;
        }

        return false;
    }
}
