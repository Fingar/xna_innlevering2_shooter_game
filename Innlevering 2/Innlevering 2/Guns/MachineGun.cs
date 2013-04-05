using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Innlevering_2.GameObjects;
using Microsoft.Xna.Framework;
using Innlevering_2.ProjectileTypes;

namespace Innlevering_2.Guns
{
    public class MachineGun : Gun
    {

        public MachineGun(Game game)
            : base(game, "granadeLauncher", "HUDMachineGun", 0.07f, 2, 5)
        {
            //projectile = new ProjectileData("RPG", new Rectangle(-5, -5, 10, 10), 0, Vector2.UnitY * 150, 30, 30, true);
        }

        public override void Fire(World world, Player player, GameTime gameTime)
        {
            if (CooldownTimer <= 0 /*&& MagazineCount > 0*/)
            {
                world.AddProjectile(new Bullet_Normal(player, player.getBulletSpawnPosition(), Vector2.Normalize(player.getReticulePosition() - player.Position) * 1000));
                CooldownTimer = Cooldown;
                //MagazineCount--;
            }

        }
    }
}
