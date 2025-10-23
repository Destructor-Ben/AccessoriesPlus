using TerraUtil.Edits;

namespace AccessoriesPlus.Content.Edits;

#pragma warning disable CS0162 // Unreachable code detected

public class AutoDislodgeDetour : Detour
{
    public override void Apply()
    {
        return;

        On_Player.GrappleMovement += delegate(On_Player.orig_GrappleMovement orig, Player self)
        {
            if (false) //ServerConfig.Instance.AutoDislodgeGrapple
            {
                // Vanilla code
                if (self.grappling[0] < 0)
                    return;

                self.StopVanityActions();
                if (Main.myPlayer == self.whoAmI && self.mount.Active)
                    self.mount.Dismount(self);

                // Actually disloding
                foreach (int hook in self.grappling)
                {
                    if (hook < 0)
                        continue;

                    var proj = Main.projectile[hook];
                    var pos = proj.Center.ToTileCoordinates();
                    var tile = Main.tile[pos.X, pos.Y];

                    if ((TileID.Sets.Platforms[tile.TileType] || tile.TileType == TileID.PlanterBox) && proj.Distance(self.Center) <= 24f)
                        proj.Kill();
                }
            }

            orig(self);
        };
    }
}
