namespace AccessoriesPlus.Content;

public partial class AccessorySystem : ModSystem
{
    public override void AddRecipes()
    {
        AddImprovedRecipies();
        AddObtainabilityRecipes();
    }

    public override void PostAddRecipes()
    {
        RemoveImprovedRecipies();
        RemoveObtainabilityRecipes();
    }
}
