using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Key : HitBoxObject
    {
        public Key(Texture2D texture, Vector2 position)
            : base(position)
        {
            this.texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            if (PickedUp == false)
                sourceRectangle = new Rectangle(280, 220, 40, 24);
            else
                sourceRectangle = new Rectangle(0, 0, 0, 0);
        }

        public bool PickedUp { get; set; }
    }
}
