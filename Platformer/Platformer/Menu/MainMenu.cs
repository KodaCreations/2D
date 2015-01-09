using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class MainMenu : Menu
    {
        public MainMenu()
            : base()
        {
            Button b = new Button(new Vector2(100, 100), new Rectangle(0, 0, 280, 70));
            buttons.Add(b);
            b = new Button(new Vector2(100, 230), new Rectangle(0, 70, 280, 70));
            buttons.Add(b);
            b = new Button(new Vector2(100, 340), new Rectangle(0, 140, 280, 70));
            buttons.Add(b);
            b = new Button(new Vector2(100, 460), new Rectangle(0, 210, 280, 70));
            buttons.Add(b);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (buttons[0].Pressed)
                StartGame = true;
            else if (buttons[1].Pressed)
                ShowInstructions = true;
            else if (buttons[2].Pressed)
                ShowCredits = true;
            else if (buttons[3].Pressed)
                Core.EndGame();
        }

        public override void Draw()
        {
            Core.spriteBatch.Begin();
            Core.spriteBatch.Draw(background, Vector2.Zero, Color.White);
            base.Draw();
            Core.spriteBatch.End();
        }

        public bool StartGame { get; set; }

        public bool ShowInstructions { get; set; }

        public bool ShowCredits { get; set; }
    }
}
