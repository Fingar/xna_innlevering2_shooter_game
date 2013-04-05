using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Innlevering_2.GameObjects;
using Microsoft.Xna.Framework;
using C3.XNA;


//Kanskje fjerne playername i HUD og flytte alle elementer opp og på èn linje?
namespace Innlevering_2.GUI
{
    public class HUD
    {
        //Textures
        private Texture2D backgroundTexture;
        private Texture2D healthIcon;
        private Texture2D healthBar;
        private Texture2D weaponTexture;
        public string weaponTextureName;

        //Texture-positions
        public Rectangle HUDposition { get; private set; }
        private Rectangle healthIconPosition;
        private Rectangle healthBarPosition;
        private Rectangle weaponPosition;

        //Spritefonts and font-position
        private SpriteFont HUDfont;
        private Vector2 nameTextPosition;
        private Vector2 lifeTextPosition;
        private Vector2 deathsTextPosition;
        private Vector2 killsTextPosition;
        private Vector2 ammoTextPosition;

        public HUD(Game game, Rectangle hudPosition)
        {

            HUDposition = hudPosition;
            weaponTextureName = "HUDDebugWpn";    //Foreløpig hardkodet inn. Senere: henter texture fra player.weapon.HUDtexture.

            //textures
            backgroundTexture = ((ContentLoader<Texture2D>)game.Services.GetService(typeof(ContentLoader<Texture2D>))).get("HUD_background");
            healthIcon = ((ContentLoader<Texture2D>)game.Services.GetService(typeof(ContentLoader<Texture2D>))).get("Health_Icon_2");
            healthBar = ((ContentLoader<Texture2D>)game.Services.GetService(typeof(ContentLoader<Texture2D>))).get("Health_Bar");
            weaponTexture = ((ContentLoader<Texture2D>)game.Services.GetService(typeof(ContentLoader<Texture2D>))).get(weaponTextureName);
            //fonts
            HUDfont = ((ContentLoader<SpriteFont>)game.Services.GetService(typeof(ContentLoader<SpriteFont>))).get("HUDfont");

            //Texture positions
            healthIconPosition = new Rectangle(hudPosition.X + 7, hudPosition.Y + 25, healthIcon.Bounds.Width, healthIcon.Bounds.Height);
            healthBarPosition = new Rectangle(hudPosition.X + 50, hudPosition.Y + 44, healthBar.Width, healthBar.Height);
            weaponPosition = new Rectangle(hudPosition.X + 250, hudPosition.Y + 10, weaponTexture.Width, weaponTexture.Height);
            //Text-positions
            nameTextPosition = new Vector2(hudPosition.X + 10, hudPosition.Y + 10);
            lifeTextPosition = new Vector2(hudPosition.X + 10, hudPosition.Y + 60);
            deathsTextPosition = new Vector2(hudPosition.X + 10, hudPosition.Y + 75);
            killsTextPosition = new Vector2(hudPosition.X + 100, hudPosition.Y + 75);
            ammoTextPosition = new Vector2(hudPosition.X + 300, hudPosition.Y + 75);

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Player player, Rectangle hudPosition, World world)
        {
            //Textures
            spriteBatch.Draw(backgroundTexture, hudPosition, Color.White);
            spriteBatch.Draw(healthIcon, healthIconPosition, Color.White);
            spriteBatch.Draw(healthBar, new Rectangle(healthBarPosition.X, healthBarPosition.Y, player.Health, healthBar.Height), Color.White);
            spriteBatch.Draw(player.Weapon.hudWeaponTexture, weaponPosition, Color.White);
            //Fonts
            spriteBatch.DrawString(HUDfont, player.PlayerName, nameTextPosition, Color.White);
            spriteBatch.DrawString(HUDfont, "Lives:  " + player.Life, lifeTextPosition, Color.OliveDrab);
            spriteBatch.DrawString(HUDfont, "Deaths: " + player.Deaths, deathsTextPosition, Color.Tomato);
            spriteBatch.DrawString(HUDfont, "Kills: " + player.Kills, killsTextPosition, Color.White);
            spriteBatch.DrawString(HUDfont, "Ammo: " + player.Weapon.MagazineCount + "/" + player.Weapon.MagazineSize, ammoTextPosition, Color.White);
        }
    }
}
