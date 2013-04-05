using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Innlevering_2.GameObjects;
using Innlevering_2.GUI;
using Microsoft.Xna.Framework.Input;
using C3.XNA;

namespace Innlevering_2.GameStates
{
    public class InGame : GameState
    {
        Player player;


        SoundEffectInstance test; 
        SoundEffectInstance test2;


        InGameMenu pauseMenu;

        //Kenneth: Brukes til å opprette gridnett som kan sjekkes mot for kollisjoner.
        //Se CollisionBox-klassen.
        List<CollisionBox> collisionGrid;
        MouseState mouseState;
        //Kenneth: Rektangel rundt musen, brukt for å teste collisionGrid.
        Rectangle mouseCollisionRectangle;


        public InGame(Game game) : base(game) 
        {
            player = new Player(game, PlayerIndex.One, new Vector2(100f, 100f));
            test = ((ContentLoader<SoundEffect>)Game.Services.GetService(typeof(ContentLoader<SoundEffect>))).get("test").CreateInstance();
            test2 = ((ContentLoader<SoundEffect>)Game.Services.GetService(typeof(ContentLoader<SoundEffect>))).get("test2").CreateInstance();
            test.Play();
            test2.IsLooped = false;
            pauseMenu = new InGameMenu(Game);

            mouseState = Mouse.GetState();
            mouseCollisionRectangle = new Rectangle(mouseState.X, mouseState.Y, 20, 20);




            //Testing av gridnett
            int gridWidth = game.Window.ClientBounds.Width / 100;   //Et rektangel i gridnettet er 100 * 100. Deler derfor på 100 for å få antall rektangler.
            int gridHeight = game.Window.ClientBounds.Height / 100; //Samme her. Tallet fra denne matten blir ikke riktig
            int gridSize = gridWidth * (gridHeight * 2);            //Derfor ganges høyden med 2. Merk at vi da får for mange rektangler totalt, dvs. rektanglene går utenfor skjermen(Bedre enn å ha for få da).
            int x = 0;  //brukes til å plassere rektangler på x-aksen i løkka nedenfor
            int y = 0;  //brukes til å plassere rektangler på y-aksen i løkka nedenfor
            Console.WriteLine(gridSize);    //Debug
            Console.WriteLine(gridWidth);
            Console.WriteLine(gridHeight);
            collisionGrid = new List<CollisionBox>();   //Listen initialiseres
            for (int i = 0; i < gridSize; i++)
            {
                collisionGrid.Add(new CollisionBox(new Rectangle(x, y, 100, 100)));
                x += 100;
                if (collisionGrid.ElementAt(i).rectangle.X > game.Window.ClientBounds.Width)
                {
                    x = 0;
                    y += 100;
                }
            }
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
                if (test.State != SoundState.Playing && test2.State != SoundState.Playing)
                {
                    test2.Play();
                }
                player.Update(gameTime);
            }

            mouseState = Mouse.GetState();
            mouseCollisionRectangle.X = mouseState.X;
            mouseCollisionRectangle.Y = mouseState.Y;

            //Denne løkka kjører CollisionBox.CheckForCollision();
            //Kan man gjøre dette på en bedre måte enn å kjøre det i løkke?
            for (int i = 0; i < collisionGrid.Count; i++)
            {
                //Gridnettet sjekker om CollisionBox.Rectangle intersecter med et rektangel spesifisert i parameteret under.
                //I dette tilfellet sjekker jeg mot mouseCollisionRectangle, men jeg ser for meg at vi sjekker om Projectile.Collision intersecter med
                //objektets rectangle. 
                //Vi kan spesifisere denne sjekken ytterligere, der vi ikke sjekker evt. kollisjoner FØR et evt. prosjektil er i listen World.Projectiles f.eks.
                collisionGrid.ElementAt(i).CheckForCollision(gameTime, mouseCollisionRectangle);

                //Hvis objectets bool coolisionEnabled == true kan vi sjekke ytterligere inne i rectanglets bounds for kollisjoner der.
                if (collisionGrid.ElementAt(i).collisionEnabled == true)
                {
                    collisionGrid.ElementAt(i).Color = Color.Aqua;  //Skifter farge hvis vi kolliderer.
                }
                else
                {
                    collisionGrid.ElementAt(i).Color = Color.Red;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            player.Draw(spriteBatch, gameTime);

            //Her tegner jeg gridnettet som Debugging, trenger naturlig nok ikke å være synlig i spillverden :)
            for (int i = 0; i < collisionGrid.Count; i++)
            {
                spriteBatch.DrawRectangle(collisionGrid.ElementAt(i).rectangle, collisionGrid.ElementAt(i).Color, 1f);
            }
            spriteBatch.DrawRectangle(mouseCollisionRectangle, Color.Green);

            if (pauseMenu.Open)
            {
                pauseMenu.Draw(spriteBatch, gameTime);
            }
            spriteBatch.End();
        }
    }
}
