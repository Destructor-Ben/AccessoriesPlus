namespace AccessoriesPlus.Content;

public partial class AccessoryInfoDisplay : GlobalInfoDisplay
{
    public override void Load()
    {
        LoadLifeformAnalyzer();
        LoadMetalDetector();
    }

    public override void Unload()
    {
        UnloadLifeformAnalyzer();
        UnloadMetalDetector();
    }

    public override void SetStaticDefaults()
    {
        SetSpelunkableTiles();
    }

    public override void ModifyDisplayParameters(InfoDisplay currentDisplay, ref string displayValue, ref string displayName, ref Color displayColor, ref Color displayShadowColor)
    {
        ModifyLifeformAnalyzer(currentDisplay, ref displayValue, ref displayColor, ref displayShadowColor);
        ModifyMetalDetector(currentDisplay, ref displayValue, ref displayColor, ref displayShadowColor);
    }
}
