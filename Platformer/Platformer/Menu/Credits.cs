using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Credits : Menu
    {
        Texture2D creditsTexture;

        public Credits()
            : base()
        {
            Button b = new Button(new Vector2(100, 460), new Rectangle(0, 280, 280, 70));
            buttons.Add(b);
            creditsTexture = Core.Content.Load<Texture2D>("Credits");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (buttons[0].Pressed)
                Return = true;
        }

        public override void Draw()
        {
            Core.spriteBatch.Begin();
            Core.spriteBatch.Draw(background, Vector2.Zero, Color.White);
            Core.spriteBatch.Draw(creditsTexture, new Vector2(100, 100), Color.White);
            base.Draw();
            Core.spriteBatch.End();
        }

        public bool Return { get; set; }
    }
}
