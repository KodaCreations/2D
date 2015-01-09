using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class HeadsUpDisplay : Object
    {
        private int health;

        public HeadsUpDisplay(Vector2 position, int health)
            : base(position)
        {
            this.health = health;
            texture = Core.Content.Load<Texture2D>("Objects/HudElements");          
            sourceRectangle = new Rectangle(0, 0, 353, 70);
        }    

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw();
            if (GotKey)
                Core.spriteBatch.Draw(texture, new Vector2(296, 16), new Rectangle(53, 70, 44, 40), Color.White);

            for (int i = 0; i < health; i++)            
                Core.spriteBatch.Draw(texture, new Vector2(i * 53 + 9, 14), new Rectangle(0, 70, 53, 45), Color.White);            
        }

        public int Health { get { return health; } set { health = value; } }

        public bool GotKey { get; set; }
    }
}