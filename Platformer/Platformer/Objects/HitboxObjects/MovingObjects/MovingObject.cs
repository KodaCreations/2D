using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    abstract class MovingObject : HitBoxObject
    {
        protected Vector2 speed;
        protected SpriteEffects spriteEffects;

        public MovingObject(Vector2 position)
            : base(position)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!Flying)
            {
                if (IsOnGround)
                    speed.Y = 0.0f;
                else
                    speed.Y += 0.2f;
            }

            position += speed;
        }

        public override void Draw()
        {
            if (Hit == false)
                Core.spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0.0f, Vector2.Zero, 1.0f, spriteEffects, 0);
        }

        public bool IsOnGround { get; set; }

        public bool Flying { get; set; }
    }
}
