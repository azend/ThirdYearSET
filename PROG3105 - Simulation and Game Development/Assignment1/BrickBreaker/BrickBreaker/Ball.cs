using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    class Ball : AbstractPhysicsObject
    {
        public Ball()
        {
            Texture = null;
            OnCollision = new BounceAction();
        }
    }
}
