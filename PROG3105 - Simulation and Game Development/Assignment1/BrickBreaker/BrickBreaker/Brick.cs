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
        public Texture2D Texture { get; set; }

        public Brick()
        {
        }
        public override void Update()
        {
            
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            Rectangle bounds = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            sb.Draw(Texture, bounds, new Color(255, 255, 0, 0));
        }
    }
}
