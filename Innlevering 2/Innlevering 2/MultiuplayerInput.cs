using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Innlevering_2
{
    public class MultiuplayerInput : GameComponent
    {

        private static PlayerIndex[] players = new PlayerIndex[4] { PlayerIndex.One, PlayerIndex.Two, PlayerIndex.Three, PlayerIndex.Four };

        public Dictionary<PlayerIndex, GamePadState> GamePadStates { get; protected set; }
        private Dictionary<PlayerIndex, GamePadState> oldGamePadStates;
        public Dictionary<PlayerIndex, bool> GamePadStateHasChanged { get; protected set; }


        public MultiuplayerInput(Game game)
            : base(game)
        {
            GamePadStates = new Dictionary<PlayerIndex, GamePadState>();
            GamePadStateHasChanged = new Dictionary<PlayerIndex, bool>();
        }

        public override void Update(GameTime gameTime)
        {
            oldGamePadStates = GamePadStates;
            foreach(PlayerIndex index in players){
                GamePadStates[index] = GamePad.GetState(index);
                GamePadStateHasChanged[index] = GamePadStates[index] != oldGamePadStates[index];
            }
            
        }

    }

    
}
