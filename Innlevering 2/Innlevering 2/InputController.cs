using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace Innlevering_2
{
    public class InputController : GameComponent
    {
        public enum MouseButton { Left, Midle, Right }

        public MouseState mouseState { get; protected set; }
        public KeyboardState keyboardState { get; protected set; }
        public GamePadState gamePadState { get; protected set; }

        private MouseState oldMs;
        private GamePadState oldGs;
        private KeyboardState oldKs;

        public bool mouseChanged { get; protected set; }
        public bool keyboardChanged { get; protected set; }
        public bool gamePadChanged { get; protected set; }

        public InputController(Game game)
            : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            oldGs = gamePadState;
            oldKs = keyboardState;
            oldMs = mouseState;

            mouseState = Mouse.GetState();
            mouseChanged = mouseState != oldMs;
            keyboardState = Keyboard.GetState();
            keyboardChanged = keyboardState != oldKs;
            gamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None);
            gamePadChanged = gamePadState != oldGs;

        }

        public bool KeyWasPressed(Keys key)
        {
            if (!keyboardChanged) return false;
            return keyboardState.IsKeyDown(key) && oldKs.IsKeyUp(key);
        }

        public bool ButtonWasPressed(Buttons button)
        {
            if (!gamePadChanged) return false;
            return gamePadState.IsButtonDown(button) && oldGs.IsButtonUp(button);
        }

        public bool MouseButtonWasPressed(MouseButton button)
        {
            if (!mouseChanged) return false;
            if (button == MouseButton.Left) return mouseState.LeftButton == ButtonState.Pressed &&
                oldMs.LeftButton == ButtonState.Released;
            if (button == MouseButton.Midle) return mouseState.MiddleButton == ButtonState.Pressed &&
                oldMs.MiddleButton == ButtonState.Released;
            if (button == MouseButton.Right) return mouseState.RightButton == ButtonState.Pressed &&
                oldMs.RightButton == ButtonState.Released;
            return false;
        }

        public Rectangle getMouseRect()
        {
            return new Rectangle(mouseState.X, mouseState.Y, 1, 1);
        }

        public Point getMouseChange()
        {
            return new Point(oldMs.X - mouseState.X, oldMs.Y - mouseState.Y);
        }
    }
}
