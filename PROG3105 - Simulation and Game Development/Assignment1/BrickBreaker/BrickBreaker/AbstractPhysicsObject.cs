using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    abstract class AbstractPhysicsObject : AbstractGameObject
    {
        public Vector3 Move { get; set; }

        public AbstractPhysicsObject()
        {
            Move = new Vector3();
        }

        public override void Update()
        {
            Position += Move;
        }

    }
}
