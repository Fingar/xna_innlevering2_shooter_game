using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Innlevering_2.GameObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using C3.XNA;
using Innlevering_2.Graphics;
using Innlevering_2.GUI;

namespace Innlevering_2.GameStates
{
    public class maskTest2 : GameState
    {
        Texture2D _Planet;
        Texture2D _AlphaMap;

        World world;

        Camera camera1;

        InGameMenu pauseMenu;

        public maskTest2(Game game)
            : base(game)
        {
            LoadContent(Game.Content);
            world = new World(Game,new DestructableLevel(Game, _Planet, _AlphaMap));
            pauseMenu = new InGameMenu(Game);
            world.Players.Add(new Player(Game, PlayerIndex.One, new Vector2(200, 200)));
            camera1 = new Camera(world.Players[0], world, Vector2.Zero, new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height - 100), new Rectangle(0, Game.Window.ClientBounds.Height - 100, Game.Window.ClientBounds.Width, 100));
        
        }

        public void LoadContent(ContentManager Content)
        {
            _Planet = Content.Load<Texture2D>("Cave1Texture");
            _AlphaMap = Content.Load<Texture2D>("Cave1");
        }


        public override void Update(GameTime gameTime)
        {
            InputController controller = (InputController)Game.Services.GetService(typeof(InputController));
            if (controller.KeyWasPressed(Keys.Escape) ||
                    controller.ButtonWasPressed(Buttons.Start))
            {
                pauseMenu.Open = !pauseMenu.Open;
            }
            if (pauseMenu.Open)
            {
                pauseMenu.Update(gameTime);
                int pressed = pauseMenu.getPressed();
                if (pressed >= 0)
                {
                    if (pressed == 0)
                    {
                        ((Game1)Game).changeState(new Menu(Game));
                    }
                    else if (pressed == 1)
                    {
                        pauseMenu.Open = false;
                    }
                }
            }
            else
            {

                /*if (controller.mouseState.LeftButton == ButtonState.Pressed)
                {
                    ((DestructableLevel)world.Level).removeCircle(new Vector2(controller.mouseState.X, controller.mouseState.Y), 20);

                }*/
                world.Update(gameTime);
                camera1.Update(gameTime);
            }

        }



        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            //spriteBatch.Begin();
            //spriteBatch.Draw(backgroundtexture, Vector2.Zero, Color.White);
            //spriteBatch.End();
            spriteBatch.Begin();
            camera1.Draw(spriteBatch, gameTime);
            //Primitives2D.DrawLine(spriteBatch, new Vector2(Game.Window.ClientBounds.Width / 2,0), new Vector2(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height), Color.Black);

            if (pauseMenu.Open)
            {
                pauseMenu.Draw(spriteBatch, gameTime);
            }

            spriteBatch.End();
        }

        
    }
}
