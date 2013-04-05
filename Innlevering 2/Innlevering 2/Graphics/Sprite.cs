using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Innlevering_2.Graphics
{
    public class Sprite : IDrawable
    {
        protected Texture2D texture;

        public Sprite(Game game, String textureName)
        {
            texture = ((ContentLoader<Texture2D>)game.Services.GetService(typeof(ContentLoader<Texture2D>))).get(textureName);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Rectangle rectangle, SpriteEffects spriteEffects, GameTime gameTime)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0 , Vector2.Zero, 1, spriteEffects, 0);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Rectangle rectangle, float rotation, Vector2 rotationOrigin, Color color, SpriteEffects spriteEffects, Point spriteSize, GameTime gameTime, float layer)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, spriteSize.X, spriteSize.Y), rectangle, color, rotation, rotationOrigin, spriteEffects, layer);
        }

        public void DrawCenter(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime)
        {
            spriteBatch.Draw(texture, position - new Vector2(texture.Width/2, texture.Height/2), Color.White);
        }
    }
}
