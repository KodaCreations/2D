using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Projectile : Weapon
    {
        public Projectile(Vector2 position, int direction)
            : base(position)
        {
            sourceRectangle = new Rectangle(0, 15, 10, 4);
            speed.X = 6 * direction;
            Flying = true;

            if (direction == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;
            else
                spriteEffects = SpriteEffects.None;
        }

        public override void Update(GameTime gameTime)
        {            
            base.Update(gameTime);
        }

    }
}
