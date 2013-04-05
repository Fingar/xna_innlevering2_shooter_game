using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Innlevering_2.GameObjects;
using Microsoft.Xna.Framework;
using C3.XNA;

namespace Innlevering_2
{

    /*Opprett en liste av CollisionBox
      Opprett et rutenett av CollisionBox.Rectangle
      bruk rutenettet til å sjekke om et rektangel intersecter med CollisionBox.Rectangle
      hvis collisionEnabled == True, sjekk for collisions inne i det spesifikke rektangelet.*/
    /// <summary>
    /// Opprett en liste av CollisionBox
    /// Opprett et rutenett av CollisionBox.Rectangle
    /// Bruk rutenettet til å sjekke om et rektangel intersecter med CollisionBox.Rectangle
    /// Hvis collisionEnabled == true, sjekk for collisions inne i dette objektets rektangel.
    /// </summary>
    public class CollisionBox
    {
        public Rectangle rectangle { get; private set; }
        public bool collisionEnabled { get; private set; }
        public Color Color { get; set; }    //Brukt til testing

        public CollisionBox(Rectangle rectangle)
        {
            this.rectangle = rectangle;
            Color = Color.Red;
        }

        /// <summary>
        /// Hvis et rektangel "intersecter" med dette objektets rektangel, blir dette objektets
        /// collisionEnabled == true. Bruk den bool'en til å sjekke kollisjoner KUN inne i dette objektets rektangel,
        /// og ikke resten av brettet.
        /// F.eks Projectile.Collision(Prosjektilets kollisjons-rektangel).
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        /// <param name="collisionRectangle">The rectangle to check for collision VS this object's rectangle</param>
        public void CheckForCollision(GameTime gameTime, Rectangle collisionRectangle)
        {
            if (collisionRectangle.Intersects(this.rectangle))
            {
                collisionEnabled = true;
            }
            else
            {
                collisionEnabled = false;
            }

        }

    }
}
