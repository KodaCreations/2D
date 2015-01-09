using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Door : HitBoxObject
    {
        public Door(Texture2D texture, Vector2 position)
            : base(position)
        {
            this.texture = texture;
            Open = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (Open)
                sourceRectangle = new Rectangle(280, 0, 70, 110);
            else
                sourceRectangle = new Rectangle(280, 110, 70, 110);

            base.Update(gameTime);
        }

        public override Rectangle HitBox
        { get { return new Rectangle((int)position.X + 10, (int)position.Y + 30, sourceRectangle.Width - 20, sourceRectangle.Height - 30); } }

        public bool Open { get; set; }
    }
}
