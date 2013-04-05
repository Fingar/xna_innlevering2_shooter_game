using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Innlevering_2
{
    class GamePadController
    {
        public GamePadState gamePadState { get; protected set; }

        private GamePadState oldGs;

        private PlayerIndex playerIndex;

        public bool gamePadChanged { get; protected set; }

        public GamePadController(PlayerIndex playerIndex)
        {
            this.playerIndex = playerIndex;
        }

        public void Update(GameTime gameTime)
        {
            oldGs = gamePadState;

            gamePadState = GamePad.GetState(playerIndex, GamePadDeadZone.None);
            gamePadChanged = gamePadState != oldGs;

        }

        public bool ButtonWasPressed(Buttons button)
        {
            if (!gamePadChanged) return false;
            return gamePadState.IsButtonDown(button) && oldGs.IsButtonUp(button);
        }

    }
}
