using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    interface ICollisionAction
    {
        void React(AbstractGameObject thisObject, AbstractGameObject otherObject);
    }
}
