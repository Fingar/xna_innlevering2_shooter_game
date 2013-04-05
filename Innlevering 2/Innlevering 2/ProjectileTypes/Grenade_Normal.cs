using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Innlevering_2.GameObjects;

namespace Innlevering_2.ProjectileTypes
{
    public class Grenade_Normal : Projectile
    {
        public Grenade_Normal(Player player, Vector2 spawnPosition, Vector2 spawnSpeed)
            : base("RPG", new Rectangle(-5, -5, 10, 10), 0, Vector2.UnitY * 150, 30, 30, true, player, spawnPosition, spawnSpeed)
        {

        }

        protected override void HandleCollision(World world)
        {
            Explode(world);
        }

    }
}
