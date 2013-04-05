using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Innlevering_2.GameObjects;

namespace Innlevering_2.ProjectileTypes
{
    public class Grenade_SimpleFrag : Projectile
    {
        public Grenade_SimpleFrag(Player player, Vector2 spawnPosition, Vector2 spawnSpeed)
            : base("RPG", new Rectangle(-5, -5, 10, 10), 0, Vector2.UnitY * 150, 15, 10, true, player, spawnPosition, spawnSpeed)
        {

        }

        protected override void Explode(World world)
        {
            base.Explode(world);
            //Console.WriteLine("test");
            world.AddProjectile(new Grenade_Normal(Owner, Position, Vector2.Normalize(new Vector2(2, -1)) * 200));
            world.AddProjectile(new Grenade_Normal(Owner, Position, Vector2.Normalize(new Vector2(1, -2)) * 200));
            world.AddProjectile(new Grenade_Normal(Owner, Position, -Vector2.UnitY * 200));
            world.AddProjectile(new Grenade_Normal(Owner, Position, Vector2.Normalize(new Vector2(-1, -2)) * 200));
            world.AddProjectile(new Grenade_Normal(Owner, Position, Vector2.Normalize(new Vector2(-2, -1)) * 200));
        }

        protected override void HandleCollision(World world)
        {
            Explode(world);
        }

    }
}
