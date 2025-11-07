using System.ComponentModel;
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
    [DefaultValue(true), ReloadRequired]
    public bool ImprovedHorseshoeBundle;
    [DefaultValue(true), ReloadRequired]
    public bool ImprovedHandOfCreation;
    public PDAConfig ImprovedPDA = new();

    #endregion

    #region Old

    /*

    #region Improved Grappling Hooks

    //[Header("ImprovedGrapplingHooks")]

    //public bool AutoDislodgeGrapple = false;

    #endregion

    #region Improved Pets

    //[Header("ImprovedPets")]

    #endregion

    #region Improved Mounts

    //[Header("ImprovedMounts")]

    #endregion

    #region Improved Minecarts

    //[Header("ImprovedMinecarts")]

    #endregion

    //*/

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

    [DefaultValue(true), Header("Obtainability"), ReloadRequired]
    public bool ObtainabilityRecipes;

    [DefaultValue(true), ReloadRequired]
    public bool ObtainabilityShimmer;

    [DefaultValue(true), ReloadRequired]
    public bool ObtainabilityNPCDrops;

    [DefaultValue(true), ReloadRequired]
    public bool ObtainabilityTravellingMerchant;

    [DefaultValue(true), ReloadRequired]
    public bool ObtainabilityPresents;

    #endregion
}

/* Descriptions to be added later
Ingame description
[c/c78fff:Improved Grappling Hooks]
- A

[c/c78fff:Improved Pets]
- A

[c/c78fff:Improved Mounts]
- A

[c/c78fff:Improved Minecarts]
- A

Steam description
[b]Improved Grappling Hooks[/b]
[list]
[*]A
[/list]

[b]Improved Pets[/b]
[list]
[*]A
[/list]

[b]Improved Mounts[/b]
[list]
[*]A
[/list]

[b]Improved Minecarts[/b]
[list]
[*]A
[/list]
*/
