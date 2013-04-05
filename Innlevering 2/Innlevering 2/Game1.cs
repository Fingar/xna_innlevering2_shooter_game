using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Innlevering_2.GameStates;

namespace Innlevering_2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ContentLoader<SpriteFont> fontLoader;
        ContentLoader<Texture2D> imageLoader;
        ContentLoader<SoundEffect> soundLoader;
        
        GameState gameState;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            InputController input = new InputController(this);
            Components.Add(input);
            Services.AddService(typeof(InputController), input);

            fontLoader = new ContentLoader<SpriteFont>(this);
            Services.AddService(typeof(ContentLoader<SpriteFont>), fontLoader);

            imageLoader = new ContentLoader<Texture2D>(this);
            Services.AddService(typeof(ContentLoader<Texture2D>), imageLoader);

            soundLoader = new ContentLoader<SoundEffect>(this);
            Services.AddService(typeof(ContentLoader<SoundEffect>), soundLoader);

            graphics.PreferredBackBufferWidth = 1440;
            graphics.PreferredBackBufferHeight = 900;
        }

        protected override void Initialize()
        {
            base.Initialize();
            gameState = new Menu(this);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            gameState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            gameState.Draw(spriteBatch, gameTime);

            base.Draw(gameTime);
        }

        public void changeState(GameState newState)
        {
            gameState = newState;
        }
    
    }
}
