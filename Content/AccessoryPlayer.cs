namespace AccessoriesPlus.Content;

public partial class AccessoryPlayer : ModPlayer
{
    public override void PostUpdateMiscEffects()
    {
        ApplyInfoHighlights();
    }
}
