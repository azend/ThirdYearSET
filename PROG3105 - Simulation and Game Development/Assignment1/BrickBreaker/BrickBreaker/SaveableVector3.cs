using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    [Serializable()]
    public class SaveableVector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public SaveableVector3()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public SaveableVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public SaveableVector3(Vector3 vect)
        {
            X = vect.X;
            Y = vect.Y;
            Z = vect.Z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }
    }
}
