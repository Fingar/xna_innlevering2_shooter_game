using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Innlevering_2.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Innlevering_2.ProjectileTypes
{
    public class HandGrenade : Projectile
    {

        private float timer;

        public HandGrenade(Player player, Vector2 spawnPosition, Vector2 spawnSpeed, float fuse)
            : base("RPG", new Rectangle(-5, -5, 10, 10), 0, Vector2.UnitY * 350, 15, 50, true, player, spawnPosition, spawnSpeed)
        {
            timer = fuse;
        }

        public override void Update(World world, List<Player> players, GameTime gameTime)
        {
            base.Update(world, players, gameTime);
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer <= 0)
            {
                Explode(world);
            }
        }

        protected override void HandleCollision(World world)
        {
            Speed = Vector2.Zero;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
            SpriteFont font = ((ContentLoader<SpriteFont>)Game.Services.GetService(typeof(ContentLoader<SpriteFont>))).get("GrenadeFont");
            String text = String.Format("{0:N1}", timer);
            spriteBatch.DrawString(font, text, Position - new Vector2(font.MeasureString(text).X / 2, 20), Color.Red);
        }

    }
}
