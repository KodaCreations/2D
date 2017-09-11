using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    abstract class Weapon : MovingObject
    {
        public Weapon(Vector2 position)
            : base(position)
        {
            texture = Core.Content.Load<Texture2D>("Objects/Weapons");
        }

        public bool PlatformCollision(Rectangle targetHitbox)
        {
                if (HitBox.Intersects(targetHitbox))
                {
                    position.Y = targetHitbox.Y - sourceRectangle.Height+1;
                    IsOnGround = true;
                    return true;
                }
                else
                {
                    IsOnGround = false;
                    return false;
                }                    
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
