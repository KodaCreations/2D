using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Button : HitBoxObject
    {
        MouseState mouseState, oldMouseState;
        Point mousePosition;
        int sourcePosY;

        public Button(Vector2 position, Rectangle sourceRectangle)
            : base(position)
        {
            texture = Core.Content.Load<Texture2D>("Objects/Buttons");
            this.position = position;
            sourcePosY = sourceRectangle.Y;
        }

        private void AnimateButton(GameTime gameTime)
        {
            if (HitBox.Contains(mousePosition))
                sourceRectangle = new Rectangle(280, sourcePosY, 280, 70);
            else
                sourceRectangle = new Rectangle(0, sourcePosY, 280, 70);
        }

        public override void Update(GameTime gameTime)
        {
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;

            if (HitBox.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed)
                Pressed = true;
            else
                Pressed = false;

            AnimateButton(gameTime);
        }

        public override void Draw()
        {
            base.Draw();
        }

        public bool Pressed { get; set; }
    }
}