using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Innlevering_2.GameObjects;
using Microsoft.Xna.Framework;
using Innlevering_2.ProjectileTypes;

namespace Innlevering_2.Guns
{
    public class ShotGun : Gun
    {

        public ShotGun(Game game)
            : base(game, "granadeLauncher", "HUDShotGun", 0.3f, 2, 5)
        {
            //projectile = new ProjectileData("RPG", new Rectangle(-5, -5, 10, 10), 0, Vector2.UnitY * 150, 30, 30, true);
        }

        public override void Fire(World world, Player player, GameTime gameTime)
        {
            if (CooldownTimer <= 0 /*&& MagazineCount > 0*/)
            {
                world.AddProjectile(new ShotGun_Pellet(player, player.getBulletSpawnPosition() + new Vector2(0, 0), Vector2.Normalize(player.getReticulePosition() - player.Position + new Vector2(-4, -4)) * 1410));
                world.AddProjectile(new ShotGun_Pellet(player, player.getBulletSpawnPosition() + new Vector2(0, 0), Vector2.Normalize(player.getReticulePosition() - player.Position + new Vector2(-2, -2)) * 1505));
                world.AddProjectile(new ShotGun_Pellet(player, player.getBulletSpawnPosition() + new Vector2(0, 0), Vector2.Normalize(player.getReticulePosition() - player.Position + new Vector2(0, 0)) * 1605));
                world.AddProjectile(new ShotGun_Pellet(player, player.getBulletSpawnPosition() + new Vector2(0, 0), Vector2.Normalize(player.getReticulePosition() - player.Position + new Vector2(2, 2)) * 1510));
                world.AddProjectile(new ShotGun_Pellet(player, player.getBulletSpawnPosition() + new Vector2(0, 0), Vector2.Normalize(player.getReticulePosition() - player.Position + new Vector2(4, 4)) * 1400));
                CooldownTimer = Cooldown;
                //MagazineCount--;
            }

        }
    }
}
