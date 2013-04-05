using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Innlevering_2.GUI
{
    public class GameMenu
    {
        public enum Direction { Vertical, Horizontal }

        public Boolean Open { get; set; }

        public Game Game { get; protected set; }

        public List<Button> Buttons { get; protected set; }
        protected int buttonIndex;

        protected Rectangle oldMousePos;

        protected Boolean joysticChange;
        protected Boolean keyboardChange;
        protected Direction dir;

        public GameMenu(Game game, Direction dir)
        {
            Game = game;
            this.dir = dir;
            Buttons = new List<Button>();
            buttonIndex = 1;
        }

        public void Update(GameTime gameTime)
        {
            CheckButtons();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Button b in Buttons)
            {
                b.Draw(spriteBatch, gameTime);
            }
        }

        protected void CheckButtons()
        {
            InputController controller = (InputController)Game.Services.GetService(typeof(InputController));
            Rectangle mousePos = new Rectangle(controller.mouseState.X, controller.mouseState.Y, 1, 1);
            Buttons[buttonIndex].Hover = false;
            if (mousePos != oldMousePos)
            {
                oldMousePos = mousePos;
                for (int i = 0; i < Buttons.Count; i++)
                {
                    if (Buttons[i].CheckHover(mousePos))
                    {
                        buttonIndex = i;
                    }
                }
            }
            if (dir == Direction.Vertical)
            {
                if (Math.Abs(controller.gamePadState.ThumbSticks.Left.Y) > .5f)
                {
                    if (!joysticChange)
                    {
                        buttonIndex -= Math.Sign(controller.gamePadState.ThumbSticks.Left.Y);
                        joysticChange = true;
                    }
                }
                else
                {
                    joysticChange = false;
                }

                if (controller.KeyWasPressed(Keys.W) ||
                    controller.KeyWasPressed(Keys.Up) ||
                    controller.KeyWasPressed(Keys.S) ||
                    controller.KeyWasPressed(Keys.Down))
                {
                    if (!keyboardChange)
                    {
                        if (controller.KeyWasPressed(Keys.S) ||
                            controller.KeyWasPressed(Keys.Down))
                        {
                            buttonIndex++;
                        }
                        else
                        {
                            buttonIndex--;
                        }
                        keyboardChange = true;
                    }
                }
                else
                {
                    keyboardChange = false;
                }
            }
            else
            {
                if (Math.Abs(controller.gamePadState.ThumbSticks.Left.X) > .5f)
                {
                    if (!joysticChange)
                    {
                        buttonIndex += Math.Sign(controller.gamePadState.ThumbSticks.Left.X);
                        joysticChange = true;
                    }
                }
                else
                {
                    joysticChange = false;
                }

                if (controller.KeyWasPressed(Keys.A) ||
                    controller.KeyWasPressed(Keys.Left) ||
                    controller.KeyWasPressed(Keys.D) ||
                    controller.KeyWasPressed(Keys.Right))
                {
                    if (!keyboardChange)
                    {
                        if (controller.KeyWasPressed(Keys.D) ||
                            controller.KeyWasPressed(Keys.Right))
                        {
                            buttonIndex++;
                        }
                        else
                        {
                            buttonIndex--;
                        }
                        keyboardChange = true;
                    }
                }
                else
                {
                    keyboardChange = false;
                }
            }
            buttonIndex = (int)MathHelper.Clamp(buttonIndex, 0, Buttons.Count - 1);
            Buttons[buttonIndex].Hover = true;
        }

        public int getPressed()
        {
            InputController controller = (InputController)Game.Services.GetService(typeof(InputController));
            if (controller.KeyWasPressed(Keys.Enter) ||
                (controller.MouseButtonWasPressed(InputController.MouseButton.Left) &&
                    Buttons[buttonIndex].CheckHover(controller.getMouseRect()))
                || controller.ButtonWasPressed(Microsoft.Xna.Framework.Input.Buttons.A))
            {
                return buttonIndex;
            }
            return -1;
        }
    }
}
