using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Trap : Weapon
    {
        public Trap(Vector2 position, int direction)
            : base(position)
        {
            sourceRectangle = new Rectangle(0, 0, 15, 15);
            IsOnGround = false;
            Flying = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override Rectangle HitBox { get { return new Rectangle((int)position.X+6, (int)position.Y, sourceRectangle.Width-12, sourceRectangle.Height); } }
    }
}
