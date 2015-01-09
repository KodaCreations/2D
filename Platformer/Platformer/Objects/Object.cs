using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    abstract class Object
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle sourceRectangle;

        public Object(Vector2 position)
        {
            this.position = position;
        }

        public virtual void Update(GameTime gameTime)
        { }

        public virtual void Draw()
        {
            Core.spriteBatch.Draw(texture, position, sourceRectangle, Color.White);
        }

        public Texture2D Texture { get { return texture; } }

        public Vector2 Position { get { return position; } }

        public Rectangle SourceRectangle { get { return sourceRectangle; } }
    }
}