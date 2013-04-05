using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Innlevering_2.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Innlevering_2.Graphics;

namespace Innlevering_2
{
    public abstract class Gun
    {
        public float Cooldown { get; protected set; }
        public float CooldownTimer { get; protected set; }
        public int MagazineSize { get; protected set; }
        public int MagazineCount { get; protected set; }
        public float ReloadTime { get; protected set; }
        public float ReloadTimer { get; protected set; }

        protected Sprite sprite;

        //Kenneth: la til hudSprite
        public Texture2D hudWeaponTexture { get; protected set; }

        //Kenneth: la til hudTextureName i konstruktør
        public Gun(Game game, String textureName, String hudTextureName, float cooldown, int magazineSize, float reloadTime)
        {
            sprite = new Sprite(game, textureName);
            hudWeaponTexture = ((ContentLoader<Texture2D>)game.Services.GetService(typeof(ContentLoader<Texture2D>))).get(hudTextureName);
            Cooldown = cooldown;
            CooldownTimer = 0;
            MagazineSize = magazineSize;
            MagazineCount = MagazineSize;
            ReloadTime = reloadTime;
            ReloadTimer = 0;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (CooldownTimer > 0)
            {
                CooldownTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (ReloadTimer > 0)
            {
                ReloadTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (ReloadTimer <= 0)
                {
                    MagazineCount = MagazineSize;
                }
            }
        }

        public abstract void Fire(World world, Player player, GameTime gameTime);

        public virtual void Reload()
        {
            ReloadTimer = ReloadTime;
        }

        /*public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.DrawCenter(spriteBatch, Player, gameTime);
        }*/
    }
}