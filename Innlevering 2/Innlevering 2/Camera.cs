using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Innlevering_2.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Innlevering_2.GUI;

namespace Innlevering_2
{
    public class Camera
    {
        public Player Player { get; protected set; }
        public Vector2 Position { get; protected set; }

        public World World { get; protected set; }

        public Rectangle Target { get; protected set; }

        public Rectangle HudPosition { get; protected set; }    //HudPosition settes i camera, siden hver spiller har sitt eget Camera.

        //Kenneth: la til opprettelse av HUD
        public HUD Hud;

        public Camera(Player player, World world, Vector2 startPosition, Rectangle target, Rectangle hudPosition)
        {
            Hud = new HUD(player.Game, hudPosition);
            Player = player;
            World = world;
            Position = startPosition;
            Target = target;
            HudPosition = hudPosition;
        }



        public void Update(GameTime gameTime)
        {
            Position = Player.Position - new Vector2(Target.Width / 2, Target.Height / 2);
            Position = new Vector2(MathHelper.Clamp(Position.X, 0, World.Level.Bounds.Width - Target.Width),
                                   MathHelper.Clamp(Position.Y, 0, World.Level.Bounds.Height - Target.Height));
        }

        //Kenneth: Merk at Hud.Draw tegnes fra Camera :)
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(World.Image, Target, new Rectangle((int)(Position.X), (int)(Position.Y), Target.Width, Target.Height), Color.White);
            Hud.Draw(spriteBatch, gameTime, Player, HudPosition, World);

        }

    }
}
