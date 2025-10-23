namespace AccessoriesPlus.Content;

public partial class AccessoryPlayer
{
    private void ApplyInfoHighlights()
    {
        if (Player != Main.LocalPlayer)
            return;

        // Metal detector
        if (PDAConfig.Instance.MetalDetectorHighlight && Util.InfoDisplayActive(InfoDisplay.MetalDetector))
            Player.findTreasure = true;

        // Radar
        // Enemies
        if (PDAConfig.Instance.RadarHighlightEnemies && Util.InfoDisplayActive(InfoDisplay.Radar))
            Player.detectCreature = true;

        // Dangerous tiles
        if (PDAConfig.Instance.RadarHighlightDanger && Util.InfoDisplayActive(InfoDisplay.Radar))
            Player.dangerSense = true;
    }
}
