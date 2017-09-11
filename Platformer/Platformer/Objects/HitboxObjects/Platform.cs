using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Platform : HitBoxObject
    {
        public Platform(Texture2D texture, Vector2 position, Rectangle sourceRectangle)
            : base(position)
        {
            this.texture = texture;
            this.position = position;
            this.sourceRectangle = sourceRectangle;
        }
    }
}
