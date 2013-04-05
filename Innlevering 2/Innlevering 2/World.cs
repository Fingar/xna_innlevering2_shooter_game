using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Innlevering_2.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Innlevering_2
{
    public class World
    {
        public Level Level { get; protected set; }
        public List<Player> Players { get; protected set; }

        public List<Projectile> Projectiles { get; protected set; }

        public Game Game { get; protected set;}

        private List<Projectile> toDestroy = new List<Projectile>();
        private List<Projectile> toAdd = new List<Projectile>();

        public Texture2D Image { get; protected set; }

        public World(Game game, Level level){
            Game = game;
            Level = level;
            Players = new List<Player>();
            Projectiles = new List<Projectile>();
            Image = new Texture2D(Game.GraphicsDevice, level.Bounds.Width, level.Bounds.Height);
        }

        public void Update(GameTime gameTime)
        {
            Level.Update(gameTime);
            foreach (Projectile proj in Projectiles)
            {
                proj.Update(this, Players, gameTime);
            }
            foreach (Projectile proj in toDestroy)
            {
                Projectiles.Remove(proj);
            }
            toDestroy = new List<Projectile>();

            foreach (Projectile proj in toAdd)
            {
                Projectiles.Add(proj);
            }
            toAdd = new List<Projectile>();

            foreach (Player player in Players)
            {
                player.Update(this, gameTime);
            }
            UpdateImage(gameTime);
        }

        public void UpdateImage(GameTime gameTime)
        {
            RenderTarget2D target = new RenderTarget2D(Game.GraphicsDevice, Level.Bounds.Width, Level.Bounds.Height);
            Game.GraphicsDevice.SetRenderTarget(target);
            Game.GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch spriteBatch2 = new SpriteBatch(Game.GraphicsDevice);
            Level.Draw(spriteBatch2);
            spriteBatch2.Begin();


            foreach (Player player in Players)
            {
                player.Draw(spriteBatch2, gameTime);
            }

            foreach (Projectile proj in Projectiles)
            {
                proj.Draw(spriteBatch2, gameTime);
            }
            spriteBatch2.End();
            Game.GraphicsDevice.SetRenderTarget(null);
            Image = target;
        }



        internal void AddProjectile(Projectile projectile)
        {
            toAdd.Add(projectile);
            //Console.WriteLine(Projectiles);
        }

        internal void RemoveProjectile(Projectile projectile)
        {
            toDestroy.Add(projectile);
            //Console.WriteLine(Projectiles);
        }

    }
}
