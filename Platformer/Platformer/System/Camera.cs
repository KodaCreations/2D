using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    public class Camera
    {
        protected float zoom;
        public Matrix transform;
        protected Vector2 position;
        protected Vector2 origin;
        protected float rotation;
        private Rectangle? limits;

        public Camera()
        {
            zoom = 1.0f;
            rotation = 0.0f;
            position = Vector2.Zero;
            origin = new Vector2(Core.Viewport.Width / 2.0f - 25, Core.Viewport.Height / 2.0f + 25);
        }

        public void Move(Vector2 amount) 
        {
            position += amount; 
        }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                // If there's a limit set and there's no zoom or rotation clamp the position
                if (Limits != null && Zoom == 1.0f && Rotation == 0.0f)
                {
                    position.X = MathHelper.Clamp(position.X, Limits.Value.X, Limits.Value.X + Limits.Value.Width - Core.Viewport.Width);
                    position.Y = MathHelper.Clamp(position.Y, Limits.Value.Y, Limits.Value.Y + Limits.Value.Height - Core.Viewport.Height);
                }
            }
        }

        public Rectangle? Limits
        {
            get { return limits; }
            set
            {
                if (value != null)
                {
                    // Assign limit but make sure it's always bigger than the viewport
                    limits = new Rectangle
                    {
                        X = value.Value.X,
                        Y = value.Value.Y,
                        Width = System.Math.Max(Core.Viewport.Width, value.Value.Width),
                        Height = System.Math.Max(Core.Viewport.Height, value.Value.Height)
                    };

                    // Validate camera position with new limit
                    Position = Position;
                }
                else { limits = null; }
            }
        }        

        public void LookAt(Vector2 position)
        {
            Position = position - new Vector2(Core.Viewport.Width / 2.0f - 50, Core.Viewport.Height / 2.0f);
        }

        public Matrix GetTransformation(Vector2 Parallax)
        {
            transform = Matrix.CreateTranslation(new Vector3(-position * Parallax, 0)) *
                Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(origin, 0.0f));
            return transform;
        }      

        public float Zoom { get { return zoom; } set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; } } // Avoids the zoom going into negatives, which flips the image
       
        public float Rotation { get { return rotation; } set { rotation = value; } }

        public Vector2 Origin { get { return origin; } set { origin = value; } }
    }
}
