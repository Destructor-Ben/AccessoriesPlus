namespace AccessoriesPlus.Utilities;

public static class KeybindUtils
{
    public static string GetAssignedKeybind(this ModKeybind keybind)
    {
        var assignedKeys = keybind.GetAssignedKeys();

        if (assignedKeys.Count == 0)
            return ModInstance.GetLocalization("Keybinds.Unknown").Value;

        return assignedKeys[0];
    }
}
