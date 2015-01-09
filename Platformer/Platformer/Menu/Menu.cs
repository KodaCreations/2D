using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Platformer
{
    abstract class Menu
    {
        protected List<Button> buttons = new List<Button>();
        protected Texture2D background;
        Song bgMusic;

        public Menu()
        {
            background = Core.Content.Load<Texture2D>("Backgrounds/MenuBackground");
        }

        public virtual void Update(GameTime gameTime)
        {  
            Core.IsMouseVisible = true;
            for (int i = 0; i < buttons.Count; i++)
                buttons[i].Update(gameTime);
        }

        public virtual void Draw()
        {
            for (int i = 0; i < buttons.Count; i++)
                buttons[i].Draw();
        }
    }
}
