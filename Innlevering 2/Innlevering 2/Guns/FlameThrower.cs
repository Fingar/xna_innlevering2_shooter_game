using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Innlevering_2.GameObjects;
using Microsoft.Xna.Framework;
using Innlevering_2.ProjectileTypes;

namespace Innlevering_2.Guns
{
    public class FlameThrower : Gun
    {
        public FlameThrower(Game game)
            : base(game, "granadeLauncher", "HUDMissileLauncher", 0.005f, 2, 5)
        {
            //projectile = new ProjectileData("RPG", new Rectangle(-5, -5, 10, 10), 0, Vector2.UnitY * 150, 30, 30, true);
        }

        public override void Fire(World world, Player player, GameTime gameTime)
        {
            if (CooldownTimer <= 0 /*&& MagazineCount > 0*/)
            {
                world.AddProjectile(new FlameFuel(player, player.getBulletSpawnPosition() + new Vector2(0, 0), Vector2.Normalize(player.getReticulePosition() - player.Position) + new Vector2(0, 2) * 600));
                world.AddProjectile(new FlameFuel(player, player.getBulletSpawnPosition() + new Vector2(10, 0), Vector2.Normalize(player.getReticulePosition() - player.Position) + new Vector2(0, 1) * 600));
                world.AddProjectile(new FlameFuel(player, player.getBulletSpawnPosition() + new Vector2(20, 0), Vector2.Normalize(player.getReticulePosition() - player.Position) + new Vector2(0, 0) * 600));
                world.AddProjectile(new FlameFuel(player, player.getBulletSpawnPosition() + new Vector2(10, 0), Vector2.Normalize(player.getReticulePosition() - player.Position) + new Vector2(0, -1) * 600));
                world.AddProjectile(new FlameFuel(player, player.getBulletSpawnPosition() + new Vector2(0, 0), Vector2.Normalize(player.getReticulePosition() - player.Position) + new Vector2(0, -2) * 600));
                CooldownTimer = Cooldown;
                //MagazineCount--;
            }

        }
    }
}
