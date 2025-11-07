using MonoMod.Cil;

namespace AccessoriesPlus.Utilities;

public static class ILUtils
{
    public static ILContext.Manipulator CreateILPatch(Action<ILCursor> patchMethod)
    {
        return il =>
        {
            try
            {
                var cursor = new ILCursor(il);
                patchMethod(cursor);
            }
            catch (Exception e)
            {
                throw new ILPatchFailureException(AccessoriesPlusMod.Instance, il, e);
            }
        };
    }

    public static void DumpIL(this ILCursor c)
    {
        DumpIL(c.Context);
    }

    public static void DumpIL(this ILContext il)
    {
        MonoModHooks.DumpIL(AccessoriesPlusMod.Instance, il);
    }
}
