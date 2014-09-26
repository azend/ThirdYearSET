using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    abstract class AbstractGameObject
    {
        public Vector3 Position { get; set; }
        public Vector2 Size { get; set; }

        public AbstractGameObject()
        {
            Position = new Vector3(0, 0, 0);
            Size = new Vector2(0, 0);
        }
        abstract public void Update();

        abstract public void Draw(SpriteBatch sb);
    }
}
