using Terraria.UI;

namespace AccessoriesPlus.Utilities.StuffToMoveToTerraUtil;

public class UISystem : TerraUtilLoader<Interface>
{
    public override void AddContent(Interface content)
    {
        if (Main.dedServ)
            return;

        content.UserInterface = new UserInterface();
        content.UserInterface.SetState(content);
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        if (Main.dedServ)
            return;

        foreach (var ui in Content)
        {
            int index = ui.GetLayerInsertIndex(layers);
            if (index == -1)
                return;

            layers.Insert(
                index,
                new LegacyGameInterfaceLayer(
                    AccessoriesPlusMod.Instance.Name + ": " + ui.Name,
                    delegate
                    {
                        if (!ui.Visible)
                            return true;

                        ui.UserInterface.Draw(Main.spriteBatch, null);
                        return true;
                    },
                    ui.ScaleType
                )
            );
        }
    }

    public override void UpdateUI(GameTime gameTime)
    {
        if (Main.dedServ)
            return;

        foreach (var ui in Content.Where(ui => ui.ShouldUpdate))
        {
            ui.UserInterface.Update(gameTime);
        }
    }
}
