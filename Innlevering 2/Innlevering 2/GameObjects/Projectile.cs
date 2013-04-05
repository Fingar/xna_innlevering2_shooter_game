using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Innlevering_2.Graphics;
using Innlevering_2.ProjectileTypes;

namespace Innlevering_2.GameObjects
{
    public abstract class Projectile : GameObject
    {
        public Player Owner { get; protected set; }
        protected Sprite texture;
        public Rectangle Collision
        {
            get
            {
                return new Rectangle(relativeCollision.X + (int)Position.X, relativeCollision.Y + (int)Position.Y, relativeCollision.Width, relativeCollision.Height);
            }
        }
        protected Rectangle relativeCollision;
        public Vector2 Speed { get; protected set; }
        protected float airResistance;
        protected Vector2 gravity;
        public float Damage { get; protected set; }
        protected float radius;
        protected bool destructive;

        public Projectile(String textureName, Rectangle collision, float airResistance, Vector2 gravity, float damage, float radius, bool destructive, Player owner, Vector2 spawnPosition, Vector2 spawnSpeed)
            : base(owner.Game)
        {
            Owner = owner;
            texture = new Sprite(Game, textureName);
            relativeCollision = collision;
            Position = spawnPosition;
            Speed = spawnSpeed;
            this.airResistance = airResistance;
            this.gravity = gravity;
            Damage = damage;
            this.radius = radius;
            this.destructive = destructive;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            texture.DrawCenter(spriteBatch, Position, gameTime);
        }

        public virtual void Update(World world, List<Player> players, GameTime gameTime)
        {
            Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Speed += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Speed -= Speed * airResistance * (float)gameTime.ElapsedGameTime.TotalSeconds;
            // add more gravity stuff

            if (world.Level.Collide(Collision))
            {
                HandleCollision(world);
            }

            foreach (Player player in players)
            {
                if (player.Equals(Owner))
                    break;

                if (player.Bounds.Intersects(Collision))
                {
                    player.Damage(this);
                }
            }
        }

        protected abstract void HandleCollision(World world);

        protected virtual void Explode(World world)
        {
            if (world.Level is DestructableLevel && destructive)
            {
                ((DestructableLevel)world.Level).removeCircle(Position, radius);
            }
            foreach (Player p in world.Players)
            {
                if (Vector2.Distance(p.Position, Position) <= radius)
                {
                    p.Damage(this);
                }
            }
            world.RemoveProjectile(this);
        }

    }
}
