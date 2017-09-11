using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class GameOverMenu : Menu
    {

        public GameOverMenu()
            : base()
        {
            Button b = new Button(new Vector2(100, 310), new Rectangle(0, 280, 280, 70));
            buttons.Add(b);
            b = new Button(new Vector2(100, 430), new Rectangle(0, 350, 280, 70));
            buttons.Add(b);
            b = new Button(new Vector2(100, 550), new Rectangle(0, 210, 280, 70));
            buttons.Add(b);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (buttons[0].Pressed)
                MainMenu = true;
            else if (buttons[1].Pressed)
                RestartGame = true;
            else if (buttons[2].Pressed)
                Core.EndGame();
        }

        public override void Draw()
        {
            Core.spriteBatch.Begin();
            base.Draw();
            Core.spriteBatch.End();
        }

        public bool MainMenu { get; set; }

        public bool RestartGame { get; set; }

        public bool GameWon { get; set; }
    }
}
