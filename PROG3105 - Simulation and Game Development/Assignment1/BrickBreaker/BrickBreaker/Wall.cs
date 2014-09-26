using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    class Wall : AbstractGameObject
    {
        public static Texture2D Texture { get; set; }

        public override void Update() { }

        public override void Draw(SpriteBatch sb)
        {
            Rectangle bounds = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            sb.Draw(Texture, bounds, new Color(255, 255, 0, 0));
        }
    }
}
