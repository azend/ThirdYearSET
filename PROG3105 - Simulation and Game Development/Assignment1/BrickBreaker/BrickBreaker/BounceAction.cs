using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    class BounceAction : ICollisionAction
    {
        private Random Randomizer;

        public BounceAction()
        {
            Randomizer = new Random();
        }
        public void React(AbstractGameObject thisObject, AbstractGameObject otherObject)
        {
            AbstractPhysicsObject thisPhysicsObject = (AbstractPhysicsObject)thisObject;

            float angleFactor = (Randomizer.Next(0, 10) / 10f) + 0.5f;
 
            Vector3 reflection = thisPhysicsObject.Move;

            if (otherObject.Bounds.Contains(new Point(thisObject.Bounds.Width / 2 + thisObject.Bounds.Left, thisObject.Bounds.Top)))
            {
                reflection.Y = Math.Abs(reflection.Y) * angleFactor;
            }
            else if (otherObject.Bounds.Contains(new Point(thisObject.Bounds.Width / 2 + thisObject.Bounds.Left, thisObject.Bounds.Bottom)))
            {
                reflection.Y = -(Math.Abs(reflection.Y)) * angleFactor;
            }
            else if (otherObject.Bounds.Contains(new Point(thisObject.Bounds.Left, thisObject.Bounds.Height / 2 + thisObject.Bounds.Top)))
            {
                reflection.X = Math.Abs(reflection.X) * angleFactor;
            }
            else if (otherObject.Bounds.Contains(new Point(thisObject.Bounds.Right, thisObject.Bounds.Height / 2 + thisObject.Bounds.Top)))
            {
                reflection.X = -(Math.Abs(reflection.X)) * angleFactor;
            }

            thisPhysicsObject.Move = reflection;


        }
    }
}
