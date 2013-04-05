using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Innlevering_2.GameObjects;
using Microsoft.Xna.Framework;
using Innlevering_2.ProjectileTypes;

namespace Innlevering_2.Guns
{
    public class GrenadeLauncher : Gun
    {
        
        public GrenadeLauncher(Game game)
            : base(game, "granadeLauncher", "HUDGrenadeLauncher", 1f, 2, 5)
        {
            //projectile = new ProjectileData("RPG", new Rectangle(-5, -5, 10, 10), 0, Vector2.UnitY * 150, 30, 30, true);
        }

        public override void Fire(World world, Player player, GameTime gameTime)
        {
            if (CooldownTimer <= 0 /*&& MagazineCount > 0*/)
            {
                world.AddProjectile(new HandGrenade(player, player.getBulletSpawnPosition(), Vector2.Normalize(player.getReticulePosition() - player.Position) * 700, 5f));
                CooldownTimer = Cooldown;
                //MagazineCount--;
            }

        }
    }
}
