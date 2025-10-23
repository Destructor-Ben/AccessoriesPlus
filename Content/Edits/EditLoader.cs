namespace AccessoriesPlus.Content.Edits;

public class EditLoader : ILoadable
{
    public void Load(Mod mod)
    {
        AutoDislodgeDetour.Apply();
        PDATintDetour.Apply();
    }

    public void Unload() { }
}
