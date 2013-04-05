using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Innlevering_2.GUI
{
    public class Button
    {
        public const int TEXT_ALIGN_LEFT = 0;
        public const int TEXT_ALIGN_MID = 1;
        public const int TEXT_ALIGN_RIGHT = 2;

        protected int textAlign;
        public String Text { get; protected set; }
        public Rectangle DrawRect { get; protected set; }
        public Boolean Hover { get; set; }
        protected Texture2D texture;
        protected Color color;
        protected Color hoverColor;
        protected SpriteFont font;
        protected Vector2 drawTextPos;

        protected Boolean drawText;

        public Button(Rectangle drawRect, Texture2D texture, int textAlignment, String text, SpriteFont font)
        {
            textAlign = textAlignment;
            Text = text;
            DrawRect = drawRect;
            this.texture = texture;
            this.font = font;
            drawText = true;
            Vector2 textSize = font.MeasureString(Text);
            if (textAlign == TEXT_ALIGN_LEFT)
            {
                drawTextPos = new Vector2(DrawRect.X + (DrawRect.Height - textSize.Y) / 2, DrawRect.Y + (DrawRect.Height - textSize.Y) / 2);
            }
            else if (textAlign == TEXT_ALIGN_MID)
            {
                drawTextPos = new Vector2(DrawRect.X + (DrawRect.Width - textSize.X) / 2, DrawRect.Y + (DrawRect.Height - textSize.Y) / 2);
            }
            else if (textAlign == TEXT_ALIGN_RIGHT)
            {
                drawTextPos = new Vector2(DrawRect.X + DrawRect.Width - (DrawRect.Height - textSize.Y) / 2, DrawRect.Y + (DrawRect.Height - textSize.Y) / 2);
            }
        }
        public Button(Rectangle drawRect, Game game, Color color, Color hoverColor, int textAlignment, String text, SpriteFont font)
            : this(drawRect, new Texture2D(game.GraphicsDevice, 1, 1), textAlignment, text, font)
        {
            this.color = color;
            this.hoverColor = hoverColor;
            texture.SetData(new Color[] { Color.White });
        }
        public Button(Rectangle drawRect, Game game, Color color)
        {
            DrawRect = drawRect;
            this.color = color;
            hoverColor = color;
            texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });
        }
        public Button(Rectangle drawRect, Game game, Color color, Color hoverColor)
            : this(drawRect, game, color)
        {
            this.hoverColor = hoverColor;
        }


        public bool CheckHover(Rectangle mousePos)
        {
            return DrawRect.Contains(mousePos);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(texture, DrawRect, (Hover) ? hoverColor : color);
            if (drawText)
            {
                
                spriteBatch.DrawString(font, Text, drawTextPos, Color.White);

            }
        }
    }
}
