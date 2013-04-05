using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Innlevering_2
{
    public interface ICollidable
    {

        bool Collide(Rectangle rect);
        bool Collide(Point point);
    }
}
