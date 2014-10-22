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
       
       
        public Wall (Vector2 size, Vector3 position, Color wallColor) : base(position, size)
        {
            this.wallColor = wallColor;
            //this.Texture = null;
        }



        public Wall(Vector3 position, Texture2D texture)
        {
            this.Position = position;
            this.Texture = texture;

            this.Size = new Vector2((float)texture.Width, (float)texture.Height);
            this.wallColor = Color.White;
        }

        public override void Update() { }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, Bounds, wallColor);
        }


    }
}
