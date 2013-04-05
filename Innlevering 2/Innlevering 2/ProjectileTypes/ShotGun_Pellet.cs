using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Innlevering_2.GameObjects;

namespace Innlevering_2.ProjectileTypes
{
    public class ShotGun_Pellet : Projectile
    {
        public ShotGun_Pellet(Player player, Vector2 spawnPosition, Vector2 spawnSpeed)
            : base("RPG", new Rectangle(-5, -5, 3, 3), 0, Vector2.UnitY * 10, 15, 5, true, player, spawnPosition, spawnSpeed)
        {

        }

        protected override void HandleCollision(World world)
        {
            Explode(world);
        }

    }
}
