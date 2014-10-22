using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    [Serializable()]
    public class SaveableVector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public SaveableVector2()
        {
            X = 0;
            Y = 0;
        }

        public SaveableVector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public SaveableVector2(Vector2 vect)
        {
            X = vect.X;
            Y = vect.Y;
        }

        public Vector2 ToVector2() {
            return new Vector2(X, Y);
        }
    }
}
