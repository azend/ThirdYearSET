using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    class Brick : AbstractGameObject
    {
        public int BreakValue { get; set; }
        public Brick()
        {
            BreakValue = 1;
            OnCollision = new DestroyAction();
        }
        public override void Update()
        {
            
        }

    }
}
