using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    abstract class HitBoxObject : Object
    {
        public HitBoxObject(Vector2 position)
            : base(position)
        {

        }

        public bool SimpleCollision(Rectangle targetHitBox)
        {
            if (HitBox.Intersects(targetHitBox))
                return true;
            else
                return false;
        }

        public bool SimpleContains(Rectangle targetHitBox)
        {
            if (HitBox.Contains(targetHitBox))
                return true;
            else
                return false;
        }

        public override void Draw()
        {
            if (Hit == false)
                base.Draw();
        }

        public virtual Rectangle HitBox { get { return new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height); } }

        public bool Hit { get; set; }
    }
}
