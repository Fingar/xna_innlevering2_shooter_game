using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Innlevering_2
{
    public abstract class GameObject
    {
        public Vector2 Position { get; protected set; }
        public Vector2 DeltaS { get; protected set; }
        public Vector2 Gravity { get; protected set; }

        public Game Game { get; protected set; }


        public GameObject(Game game)
        {
            Game = game;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public void move(Vector2 delta)
        {
            Position += delta;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

    }
}
