using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    abstract class Character : MovingObject
    {
        //protected bool jumping = true;
        protected int sheetPosX = 0;
        private int frame;
        private double frameTimer;
        protected double frameInterval;
        private int nrOfFrames;

        public enum Direction { Right, Left };
        public Direction currentDirection = Direction.Right;

        public Character(Vector2 position)
            : base(position)
        {
            texture = Core.Content.Load<Texture2D>("Objects/CharacterSheet");
            nrOfFrames = 1;
            frameTimer = 100;
            spriteEffects = SpriteEffects.None;
        }

        public bool PlatformCollision(Rectangle targetHitBox)
        {
            Rectangle tempTop = new Rectangle(HitBox.X + 5, HitBox.Y, HitBox.Width - 10, HitBox.Height - 36);
            Rectangle tempBottom = new Rectangle(HitBox.X + 5, HitBox.Y + 41, HitBox.Width - 10, HitBox.Height - 36);
            Rectangle tempRight = new Rectangle(HitBox.X + 29, HitBox.Y + 5, HitBox.Width - 24, HitBox.Height - 10);
            Rectangle tempLeft = new Rectangle(HitBox.X, HitBox.Y + 5, HitBox.Width - 24, HitBox.Height - 10);

            // Landing on platform
            if (tempBottom.Intersects(targetHitBox))
            {
                position.Y = targetHitBox.Y - sourceRectangle.Height + 1;
                speed.Y = 0.0f;
                IsOnGround = true;
                return true;
            }
            // Hitting head against platform
            else if (tempTop.Intersects(targetHitBox))
            {
                position.Y = targetHitBox.Y + targetHitBox.Height;
                speed.Y = 0.0f;
                return true;
            }
            // Hitting platform from the left
            else if (tempRight.Intersects(targetHitBox))
            {
                position.X = targetHitBox.Left - sourceRectangle.Width;
                speed.X = 0.0f;
                return true;
            }
            // Hitting platform from the right
            else if (tempLeft.Intersects(targetHitBox))
            {
                position.X = targetHitBox.Right;
                speed.X = 0.0f;
                return true;
            }
            else
            {
                IsOnGround = false;
                return false;
            }
        }

        protected void Animations()
        {
            // Standing still
            if (speed.Y == 0.0f && speed.X == 0.0f && IsOnGround == true)
            {
                sheetPosX = 0;
                nrOfFrames = 1;
            }
            // Jumping up
            else if (speed.Y < 0.0f && IsOnGround == false)
            {
                sheetPosX = 204;
                nrOfFrames = 1;
            }
            // Falling down
            else if (speed.Y > 0.0f && IsOnGround == false)
            {
                sheetPosX = 238;
                nrOfFrames = 1;
            }
            // Moving right
            else if (speed.X > 0 && speed.Y == 0.0f && IsOnGround == true)
            {
                sheetPosX = 68;
                nrOfFrames = 4;
            }
            // Moving left
            else if (speed.X < 0 && speed.Y == 0.0f && IsOnGround == true)
            {
                sheetPosX = 68;
                nrOfFrames = 4;
            }

            if (currentDirection == Direction.Right)
                spriteEffects = SpriteEffects.None;
            if (currentDirection == Direction.Left)
                spriteEffects = SpriteEffects.FlipHorizontally;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Animations();

            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                sourceRectangle.X = sheetPosX + (frame % nrOfFrames) * 34;
            }
        }
    }
}
