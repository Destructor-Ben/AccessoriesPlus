using AccessoriesPlus.Config.CustomConfigStuff;
using AccessoriesPlus.Config.SubConfigs;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Config;

public class ServerConfig : CustomModConfig
{
    public static ServerConfig Instance => ModContent.GetInstance<ServerConfig>();
    public override ConfigScope Mode => ConfigScope.ServerSide;

    #region Improved Accessories

    [Header("ImprovedAccessories"), ReloadRequired]
    public bool ImprovedTerrasparkBoots = true;
    [ReloadRequired]
    public bool ImprovedAnkhShield = true;
    [ReloadRequired]
    public bool ImprovedHorseshoeBundle = true;
    [ReloadRequired]
    public bool ImprovedHandOfCreation = true;
    public PDAConfig ImprovedPDA = new();

    #endregion

    #region Accessory Slots

    // TODO: go through each accessory (for all of the slots) and see if there are any items that set the item.__Slot variable and manually blacklist them here, but in a way that the user can override

    [Header("AccessorySlots")]
    public bool SlotMoonLord = true;
    public CustomAccessorySlotConfig SlotWings = new();
    public CustomAccessorySlotConfig SlotShield = new();
    public CustomAccessorySlotConfig SlotBoots = new();

    #endregion

    #region Obtainability

    [Header("Obtainability"), ReloadRequired]
    public bool ObtainabilityRecipes = true;
    [ReloadRequired]
    public bool ObtainabilityShimmer = true;
    [ReloadRequired]
    public bool ObtainabilityNPCDrops = true;
    [ReloadRequired]
    public bool ObtainabilityTravellingMerchant = true;
    [ReloadRequired]
    public bool ObtainabilityPresents = true;

    #endregion
}
