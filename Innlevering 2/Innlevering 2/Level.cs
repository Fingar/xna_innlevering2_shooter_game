using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Innlevering_2
{
    public class Level : ICollidable
    {

        public Rectangle Bounds { get; protected set; }

        public Game Game { get; protected set; }

        public Level(Game game)
        {
            Game = game;
        }

        public virtual bool Collide(Rectangle rect)
        {
            return false;
        }

        public virtual bool Collide(Point point)
        {
            return false;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

    }
}
