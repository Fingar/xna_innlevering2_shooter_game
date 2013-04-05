using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using C3.XNA;
using Innlevering_2.Graphics;
using Innlevering_2.Guns;

namespace Innlevering_2.GameObjects
{
    public class Player : GameObject
    {
        private Rectangle relativeBounds;
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)(relativeBounds.X + Position.X), (int)(relativeBounds.Y + Position.Y), relativeBounds.Width, relativeBounds.Height);
            }
        }

        //public Physics physics;
        public float Speed { get; protected set; }
        public float FallingSpeed { get; protected set; }

        private float gravity = 1000f;
        private float jumpForce = 500f;

        public bool Grounded { get; protected set; }

        private int walkSlope = 5;

        private Sprite reticule;

        private float reticuleAngle;

        private PlayerIndex playerIndex;
        private GamePadController controller;

        // .Fingar - Variabler
        private Sprite bodyRunning;
        private Sprite bodyStandStill;
        private Sprite bodyInAir;
        private Sprite arm;
        private Sprite gun;
        Point armSize = new Point(21, 7);
        Point gunSize = new Point(36, 13);
        Point aimLaserSize = new Point(66, 1);
        Point frameSize = new Point(32, 40);
        Point currentFrame = new Point(0, 0);
        Point sheetSize = new Point(12, 0);
        private bool facingRight = true;
        private int timeSinceLastFrame = 0;
        private int millisecondsPerFrame = 300;
        private Rectangle armBounds;

        public Gun Weapon
        {
            get
            {
                return guns[activeGun];
            }
        }

        private List<Gun> guns;
        private int activeGun = 0;

        //Kenneth: Health blir hardkodet i konstruktør til 100.
        public int Health { get; protected set; }

        //Kenneth: PlayerName er foreløpig hardkodet i konstruktør, ønsker å ta det inn som parameter ved senere tidspunkt.
        public string PlayerName { get; protected set; }

        //Kenneth: La til Life-property. Hardkoder inn i konstruktør, ta som parameter senere.
        public int Life { get; protected set; }

        //Kenneth:
        public int Deaths { get; protected set; }

        //Kenneth:
        public int Kills { get; protected set; }

        //Kenneth: Må ha keyboard for å teste inventory
        KeyboardState oldKeyboardState;
        KeyboardState currentKeyboardState;

        public Player(Game game, PlayerIndex playerIndex, Vector2 PlayerPosition)
            : base(game)
        {
            guns = new List<Gun>(new Gun[]{
                new MachineGun(Game),
                new ShotGun(Game),
                new GrenadeLauncher(Game)
            }
            );
            this.playerIndex = playerIndex;
            Position = PlayerPosition;
            relativeBounds = new Rectangle(-10, -10, 22, 40);
            Speed = 300;

            reticule = new Sprite(Game, "AimLaser");
            bodyRunning = new Sprite(Game, "RunningSolider_NoArms");
            bodyStandStill = new Sprite(Game, "SoliderStatic");
            bodyInAir = new Sprite(Game, "SoliderJump");
            arm = new Sprite(Game, "SoliderArm");

            controller = new GamePadController(playerIndex);

            controller = new GamePadController(playerIndex);

            //Kenneth: Hardkoder inn spillernavn,health, life, deaths, kills. ønsker som parameter senere
            PlayerName = "Godzilla!";
            Health = 100;
            Life = 5;
            Deaths = 0;
            Kills = 0;
            //la til keyboard
            oldKeyboardState = Keyboard.GetState();
        }

        public void Update(World world, GameTime gameTime)
        {
            if (activeGun == 0)
            {
                gun = new Sprite(Game, "MachineGun");
            }
            else if (activeGun == 1)
            {
                gun = new Sprite(Game, "ShotGun");
            }
            else
            {
                gun = new Sprite(Game, "GrenadeLauncher");
            }

            oldKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            controller.Update(gameTime);
            if (controller.ButtonWasPressed(Buttons.DPadRight))
            {
                activeGun++;
                activeGun %= guns.Count;
            }
            if (controller.ButtonWasPressed(Buttons.DPadLeft))
            {
                activeGun += guns.Count-1;
                activeGun %= guns.Count;
            }
            Weapon.Update(gameTime);


            //if (controller.ButtonWasPressed(Buttons.RightTrigger))
            if (controller.gamePadState.IsButtonDown(Buttons.RightTrigger))
            {
                Weapon.Fire(world, this, gameTime);
                //((DestructableLevel)level).removeCircle(getReticulePosition(), 20);
            }
            if (controller.ButtonWasPressed(Buttons.X))
            {
                Weapon.Reload();
                //((DestructableLevel)level).removeCircle(getReticulePosition(), 20);
            }
            //reticule position

            //NINJAROPE FIRE
            /*
            if (controller.gamePadState.IsButtonDown(Buttons.LeftTrigger))
            {
                ninjaRope.Fire(world, this, gameTime);
            }
            */

            if (controller.gamePadState.ThumbSticks.Right.LengthSquared() > .75f)
            {
                reticuleAngle = (float)Math.Atan2(controller.gamePadState.ThumbSticks.Right.Y, controller.gamePadState.ThumbSticks.Right.X);
                if (controller.gamePadState.ThumbSticks.Right.X < 0)
                {
                    facingRight = false;
                }
                else
                {
                    facingRight = true;
                }
            }

            Vector2 move = Vector2.Zero;

            //Movement
            if (Math.Abs(controller.gamePadState.ThumbSticks.Left.X) >= 0.2f)
            {
                move += controller.gamePadState.ThumbSticks.Left * Vector2.UnitX;
            }
            //if (Math.Abs(controller.gamePadState.ThumbSticks.Left.Y) >= 0.2f)
            //    move -= controller.gamePadState.ThumbSticks.Left * Vector2.UnitY;

            if (Grounded)
            {
                if (controller.ButtonWasPressed(Buttons.A))
                {
                    jump();
                }
                TryWalk(move * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, world.Level);
            }
            else
            {
                TryMove(move * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, world.Level);
                Fall(gameTime, world.Level);
            }

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            
            // 
            if (!(Math.Abs(controller.gamePadState.ThumbSticks.Left.X) <= 0.2f))
            {
                if (timeSinceLastFrame > millisecondsPerFrame)
                {

                    if (!(facingRight))
                    {
                        ++currentFrame.X;
                        if (currentFrame.X >= sheetSize.X)
                        {
                            currentFrame.X = 0;
                        }
                    }
                    else
                    {
                        --currentFrame.X;
                        if (currentFrame.X <= 0)
                        {
                            currentFrame.X = sheetSize.X - 1;
                        }
                    }
                }
            }

            armBounds = new Rectangle((int) Position.X, (int) Position.Y, 32, 8);
        }

        public bool TryWalk(Vector2 rel, ICollidable collision)
        {
            int tries = -walkSlope;
            while (collision.Collide(new Rectangle(Bounds.X + (int)Math.Round(rel.X * (walkSlope - tries) / walkSlope), Bounds.Y + (int)Math.Round(rel.Y) - tries, Bounds.Width, Bounds.Height)) &&
                tries < walkSlope)
            {
                tries++;
            }
            if (tries == walkSlope)
            {
                return false;
            }
            if (tries == -walkSlope)
            {
                Grounded = false;
                move(rel);
                return true;
            }

            move(rel * new Vector2((walkSlope - tries) / walkSlope, 1) - Vector2.UnitY * tries);
            return true;
        }
        public bool TryMove(Vector2 rel, ICollidable collision)
        {
            if (collision.Collide(new Rectangle(Bounds.X + (int)Math.Round(rel.X), Bounds.Y + (int)Math.Round(rel.Y), Bounds.Width, Bounds.Height)))
            {
                return false;
            }

            move(rel);
            return true;
        }

        public void Fall(GameTime gameTime, ICollidable collision)
        {
            FallingSpeed += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Grounded = !TryMove(Vector2.UnitY * (float)gameTime.ElapsedGameTime.TotalSeconds * FallingSpeed, collision);
            if (Grounded)
            {
                FallingSpeed = 0;
                TryWalk(Vector2.Zero, collision);
            }
        }




        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            //debug
            /*Primitives2D.DrawRectangle(spriteBatch, Bounds,
                Color.Brown);
             */

            //AIMING RETICULE
            //reticule.DrawCenter(spriteBatch, getReticulePosition(), gameTime);

            reticule.Draw(spriteBatch,
                getReticulePosition() + new Vector2(0, 3),
                new Rectangle(
                0,
                0,
                36,
                1),
                -reticuleAngle,
                new Vector2(0, 0),
                Color.White,
                SpriteEffects.None,
                aimLaserSize,
                gameTime,
                0);


            //PLAYER BODY - Move on X-axis
            if (!(Math.Abs(controller.gamePadState.ThumbSticks.Left.X) <= 0.2f))
            {

                //In air
                if (!(Grounded))
                {
                    if (facingRight)
                    {
                        PlayerInAirRight(spriteBatch, gameTime);
                    }
                    else
                    {
                        PlayerInAirLeft(spriteBatch, gameTime);
                    }
                }

                //On ground
                else
                {
                    if (facingRight)
                    {
                        PlayerRunRight(spriteBatch, gameTime);
                    }
                    else
                    {
                        PlayerRunLeft(spriteBatch, gameTime);
                    }
                }
            }

            //PLAYER BODY - Standstill on X-axis
            else
            {

                //In air
                if (!(Grounded))
                {
                    if (facingRight)
                    {
                        PlayerInAirRight(spriteBatch, gameTime);
                    }
                    else
                    {
                        PlayerInAirLeft(spriteBatch, gameTime);
                    }
                }

                //On ground
                else
                {
                    //STANDSTILL
                    if (facingRight)
                    {
                        PlayerStandStillRight(spriteBatch, gameTime);
                    }
                    else
                    {
                        PlayerStandStillLeft(spriteBatch, gameTime);
                    }
                }
            }

            //PLAYER ARM - Rotation
            if (facingRight)
            {
                PlayerArmFaceRight(spriteBatch, gameTime);
            }
            else
            {
                PlayerArmFaceLeft(spriteBatch, gameTime);

            }


        }

        public Vector2 getReticulePosition()
        {
            return new Vector2((float)Math.Cos(reticuleAngle), -(float)Math.Sin(reticuleAngle)) * 30 + Position;
        }

        public Vector2 getGunPosition()
        {
            return new Vector2((float)Math.Cos(reticuleAngle), -(float)Math.Sin(reticuleAngle)) * 2 + Position;
        }

        public Vector2 getBulletSpawnPosition()
        {
            return new Vector2((float)Math.Cos(reticuleAngle), -(float)Math.Sin(reticuleAngle)) * 35 + Position;
        }

        public void Damage(Projectile projectile)
        {
            Health -= (int)projectile.Damage;

            if (Health <= 0)
            {
                //player got killed by projectile.Owner
                //Life--
                //OTHER player got ++ Kills
                //THIS player got ++ Deaths
                //Play death animation && death-sound
                //respawn!
                Deaths++;
                projectile.Owner.Kills++;
                Life--;

                if (Life <= 0)
                {
                    //TODO!!
                    //Game Over
                }
            }
        }

        /*protected bool Collide()
        {
            Rectangle playerRect = new Rectangle((int)Position.X, (int)Position.Y, PlayerSize.X, PlayerSize.Y);
            Rectangle mapFrameRect = new Rectangle(room.PosX, room.PosY, room.Width, room.Height);

            return (playerRect.Intersects(mapFrameRect));
        }*/

        internal void jump()
        {
            Grounded = false;
            FallingSpeed = -jumpForce;
        }

        //PLAYER STANDSTILL
        public void PlayerStandStillRight(SpriteBatch spriteBatch, GameTime gameTime) 
        {
            bodyStandStill.Draw(spriteBatch,
                new Vector2(Position.X - (frameSize.X / 2), Position.Y - 10),
                new Rectangle(
                0,
                0,
                32,
                40),
                SpriteEffects.None,
                gameTime);
        }

        private void PlayerStandStillLeft(SpriteBatch spriteBatch, GameTime gameTime) 
        {
             bodyStandStill.Draw(spriteBatch,
                new Vector2(Position.X - (frameSize.X / 2), Position.Y - 10),
                new Rectangle(
                0,
                0,
                32,
                40),
                SpriteEffects.FlipHorizontally,
                gameTime);
        }

        //PLAYER RUN
        private void PlayerRunRight(SpriteBatch spriteBatch, GameTime gameTime)
        {
            bodyRunning.Draw(spriteBatch,
                new Vector2(Position.X - (frameSize.X / 2), Position.Y - (frameSize.Y / 2) + 11),
                new Rectangle(
                currentFrame.X * frameSize.X,
                currentFrame.Y * frameSize.Y,
                frameSize.X,
                frameSize.Y),
                SpriteEffects.None,
                gameTime);
        }

        private void PlayerRunLeft(SpriteBatch spriteBatch, GameTime gameTime)
        {
            bodyRunning.Draw(spriteBatch,
                new Vector2(Position.X - (frameSize.X / 2), Position.Y - (frameSize.Y / 2) + 11),
                new Rectangle(
                currentFrame.X * frameSize.X,
                currentFrame.Y * frameSize.Y,
                frameSize.X,
                frameSize.Y),
                SpriteEffects.FlipHorizontally,
                gameTime);
        }

        //PLAYER JUMP
        private void PlayerInAirRight(SpriteBatch spriteBatch, GameTime gameTime)
        {
            bodyInAir.Draw(spriteBatch,
                new Vector2(Position.X - (frameSize.X / 2), Position.Y - 10),
                new Rectangle(
                0,
                0,
                32,
                40),
                SpriteEffects.None,
                gameTime);
        }

        private void PlayerInAirLeft(SpriteBatch spriteBatch, GameTime gameTime)
        {
            bodyInAir.Draw(spriteBatch,
                new Vector2(Position.X - (frameSize.X / 2), Position.Y - 10),
                new Rectangle(
                0,
                0,
                32,
                40),
                SpriteEffects.FlipHorizontally,
                gameTime);
        }

        //PLAYER ARMS
        private void PlayerArmFaceRight(SpriteBatch spriteBatch, GameTime gameTime) 
        {
            gun.Draw(spriteBatch,
                getGunPosition() + new Vector2(0, 6),
                new Rectangle(
                0,
                0,
                36,
                13),
                -reticuleAngle,
                new Vector2(0, 6.5f),
                Color.White,
                SpriteEffects.None,
                gunSize,
                gameTime,
                0);
            arm.Draw(spriteBatch,
                new Vector2(Position.X, Position.Y + 6),
                new Rectangle(
                0,
                0,
                21,
                7),
                -reticuleAngle,
                new Vector2(2, 3),
                Color.White,
                SpriteEffects.None,
                armSize,
                gameTime,
                0);
        }

        private void PlayerArmFaceLeft(SpriteBatch spriteBatch, GameTime gameTime) 
        {
            gun.Draw(spriteBatch,
                getGunPosition() + new Vector2(0, 6),
                new Rectangle(
                0,
                0,
                36,
                13),
                -reticuleAngle,
                new Vector2(2, 6.5f),
                Color.White,
                SpriteEffects.FlipVertically,
                gunSize,
                gameTime,
                0);
            arm.Draw(spriteBatch,
                new Vector2(Position.X + 2, Position.Y + 6),
                new Rectangle(
                0,
                0,
                21,
                7),
                -reticuleAngle - 3,
                new Vector2(20, 3),
                Color.White,
                SpriteEffects.FlipHorizontally,
                armSize,
                gameTime,
                0);
        }
    }
}
