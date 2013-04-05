using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Innlevering_2.GUI
{
    public class InGameMenu:GameMenu
    {


        public InGameMenu(Game game):base(game,Direction.Horizontal)
        {
            SpriteFont buttonFont = ((ContentLoader<SpriteFont>)game.Services.GetService(typeof(ContentLoader<SpriteFont>))).get("ButtonFont");
            Buttons.Add(new Button(new Rectangle(50, 50, 150, 50), game, Color.Green, Color.GreenYellow, Button.TEXT_ALIGN_MID, "Exit", buttonFont));
            Buttons.Add(new Button(new Rectangle(210, 50, 150, 50), game, Color.Green, Color.GreenYellow, Button.TEXT_ALIGN_MID, "Continue", buttonFont));
        }
    }
}
