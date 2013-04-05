using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Innlevering_2
{
    public abstract class GameState
    {

        public Game Game { get; protected set; }

        public GameState(Game game)
        {
            Game = game;
        }

        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws the graphics
        /// NOTE: expects spriteBatch to have begun
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
