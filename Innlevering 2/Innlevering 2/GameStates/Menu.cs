using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Innlevering_2.GUI;

namespace Innlevering_2.GameStates
{
    public class Menu : GameState
    {
        private GameMenu gameMenu;

        public Menu(Game game)
            : base(game)
        {
            SpriteFont buttonFont = ((ContentLoader<SpriteFont>)Game.Services.GetService(typeof(ContentLoader<SpriteFont>))).get("ButtonFont");
            gameMenu = new GameMenu(Game, GameMenu.Direction.Vertical);
            gameMenu.Buttons.Add(new Button(new Rectangle(50, 50, 200, 50), game, Color.Green, Color.GreenYellow, Button.TEXT_ALIGN_MID, "Game", buttonFont));
            gameMenu.Buttons.Add(new Button(new Rectangle(50, 110, 200, 50), game, Color.Green, Color.GreenYellow, Button.TEXT_ALIGN_MID, "MaskTest", buttonFont));
            gameMenu.Buttons.Add(new Button(new Rectangle(50, 170, 200, 50), game, Color.Green, Color.GreenYellow, Button.TEXT_ALIGN_MID, "MaskTest2", buttonFont));
            gameMenu.Buttons.Add(new Button(new Rectangle(50, 230, 200, 50), game, Color.Green, Color.GreenYellow, Button.TEXT_ALIGN_MID, "Exit", buttonFont));
        }

        public override void Update(GameTime gameTime)
        {

            gameMenu.Update(gameTime);

            switch (gameMenu.getPressed())
            {
                case 0:
                    ((Game1)Game).changeState(new InGame(Game));
                    break;
                case 1:
                    ((Game1)Game).changeState(new maskTest(Game));
                    break;
                case 2:
                    ((Game1)Game).changeState(new maskTest2(Game));
                    break;
                case 3:
                    Game.Exit();
                    break;
            }

            InputController controller = (InputController)Game.Services.GetService(typeof(InputController));
            if (controller.KeyWasPressed(Keys.Escape) ||
                controller.ButtonWasPressed(Buttons.Start))
            {
                Game.Exit();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(((ContentLoader<Texture2D>)Game.Services.GetService(typeof(ContentLoader<Texture2D>))).get("Background"),
                new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height), Color.White);
            gameMenu.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}
