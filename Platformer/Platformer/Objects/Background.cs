using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Background : Object
    {
        public Background(Vector2 position, Texture2D texture)
            : base(position)
        {
            this.texture = texture;
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
        }
    }
}
