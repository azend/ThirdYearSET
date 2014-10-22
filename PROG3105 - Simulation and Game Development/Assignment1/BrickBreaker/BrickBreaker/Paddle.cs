using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    class Paddle : AbstractPhysicsObject
    {
        public int Speed { get; set; }

        public Paddle()
            : base()
        {
            Speed = 10;
        }

        public void MoveLeft() {
            //if (!IsMoving())
            //{
                Move = new Microsoft.Xna.Framework.Vector3(-Speed, 0, 0);
            //}
        }

        public void MoveRight()
        {
            //if (!IsMoving())
            //{
                Move = new Microsoft.Xna.Framework.Vector3(Speed, 0, 0);
            //}
        }

        public void MoveStop()
        {
            Move = new Microsoft.Xna.Framework.Vector3(0, 0, 0);
        }

        private bool IsMoving()
        {
            bool isMoving = false;

            if (Move.X != 0 || Move.Y != 0 || Move.Z != 0)
            {
                isMoving = true;
            }

            return isMoving;
        }
    }
}
