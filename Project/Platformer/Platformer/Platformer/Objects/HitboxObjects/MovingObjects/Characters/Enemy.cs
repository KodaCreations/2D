using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Enemy : Character
    {
        public Enemy(Vector2 position)
            : base(position)
        {
            sourceRectangle = new Rectangle(0, 46, 34, 46);
            speed.X = 1.0f;
            frameInterval = 125;
        }

        public bool PlatformPatrol(Rectangle targetHitBox)
        {
            if (HitBox.Intersects(targetHitBox))
            {
                position.Y = targetHitBox.Y - HitBox.Height + 1;
                IsOnGround = true;

                if (HitBox.X + HitBox.Width > targetHitBox.X + targetHitBox.Width)
                {
                    currentDirection = Direction.Left;
                    speed.X = -1.0f;
                }                   
                else if (HitBox.X < targetHitBox.X)
                {
                    currentDirection = Direction.Right;
                    speed.X = 1.0f;
                }                
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
