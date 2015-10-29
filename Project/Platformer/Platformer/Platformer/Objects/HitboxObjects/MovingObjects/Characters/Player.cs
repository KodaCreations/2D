using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Player : Character
    {
        private int trapTimer;
        private int gunTimer;
        private int health = 5;

        public Vector2 RespawnPosition { get; set; }

        public Player(Vector2 position)
            : base(position)
        {
            sourceRectangle = new Rectangle(0, 0, 34, 46);
            GetDirection = 1;
            frameInterval = 100;
        }

        public bool PixelCollision(Texture2D targetTexture, Rectangle targetHitBox, Rectangle targetSourceRectangle)
        {
            Color[] dataA = new Color[sourceRectangle.Width * sourceRectangle.Height];
            texture.GetData(0, sourceRectangle, dataA, 0, dataA.Length);
            Color[] dataB = new Color[targetSourceRectangle.Width * targetSourceRectangle.Height];
            targetTexture.GetData(0, targetSourceRectangle, dataB, 0, dataB.Length);

            int top = Math.Max(HitBox.Top, targetHitBox.Top);
            int bottom = Math.Min(HitBox.Bottom, targetHitBox.Bottom);
            int left = Math.Max(HitBox.Left, targetHitBox.Left);
            int right = Math.Min(HitBox.Right, targetHitBox.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color colorA = dataA[(x - HitBox.Left) + (y - HitBox.Top) * HitBox.Width];
                    Color colorB = dataB[(x - targetHitBox.Left) + (y - targetHitBox.Top) * targetHitBox.Width];
                    if (colorA.A >= 200 && colorB.A >= 200)
                    {
                        health -= 1;
                        if (health != 0)
                            position = RespawnPosition;
                        return true;
                    }
                }
            }
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            speed.X = 0;

            if (Core.KeyState.IsKeyDown(Keys.Right) && position.X < Core.WorldSize.Width - sourceRectangle.Width)
            {
                currentDirection = Direction.Right;
                GetDirection = 1;
                speed.X += 3;
            }
            else if (Core.KeyState.IsKeyDown(Keys.Left) && position.X > 0)
            {
                currentDirection = Direction.Left;
                GetDirection = -1;
                speed.X -= 3;
            }
            if (Core.KeyState.IsKeyDown(Keys.Up) && IsOnGround == true)
            {
                speed.Y -= 9.0f;
                IsOnGround = false;
            }

            trapTimer -= gameTime.ElapsedGameTime.Milliseconds;
            if (trapTimer <= 0)
                if (Core.KeyState.IsKeyDown(Keys.Z))
                {
                    PlaceTrap = true;
                    trapTimer = 5000;
                }

            gunTimer -= gameTime.ElapsedGameTime.Milliseconds;
            if (gunTimer <= 0)
                if (Core.KeyState.IsKeyDown(Keys.X))
                {
                    ShootGun = true;
                    gunTimer = 1000;
                }

            base.Update(gameTime);
        }



        public int GetDirection { get; set; }

        public bool ShootGun { get; set; }

        public bool PlaceTrap { get; set; }

        public int Health { get { return health; } set { health = value; } }
    }
}
