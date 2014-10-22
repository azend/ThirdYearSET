using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    class DestroyAction : ICollisionAction
    {
        public void React(AbstractGameObject thisObject, AbstractGameObject otherObject)
        {
            thisObject.Show = false;
        }
    }
}
