using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Innlevering_2.Graphics
{
    interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime);
    }
}
