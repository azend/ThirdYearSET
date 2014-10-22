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

        public Texture2D Texture { get; set; }

        public bool Show { get; set; }
        public ICollisionAction OnCollision { get; protected set; }
        public Color wallColor { get; set; }

        public Rectangle Bounds
        {
            get
            {
                /* Rectangle temp;
                 if (Texture != null)
                 {
                     temp = new Rectan
                 * gle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
                 }
                 else
                 {
                     temp = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
                 }
                 return temp;*/
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            }
        }

        



        public AbstractGameObject(Vector3 newPosition, Vector2 newSize)
        {
            Position = newPosition;
            Size = newSize;
            Show = true;
        }
        public AbstractGameObject()
        {
            Position = new Vector3(0, 0, 0);
            Size = new Vector2(0, 0);
            Show = true;
        }
        abstract public void Update();

        public virtual void Draw(SpriteBatch sb)
        {
            if (Show)
            {
                sb.Draw(Texture, Bounds, Color.White);
                //sb.Draw(Texture, Bounds, null, Color.White, Position.Z, new Vector2((float)Math.Round(Bounds.Width / 2f), (float)Math.Round(Bounds.Height / 2f)), SpriteEffects.None, 0f);
            }
        }

        public Boolean DetectCollision(AbstractGameObject target)
        {
            bool collision = false;

            if (target.Show && Bounds.Intersects(target.Bounds))
            {
                collision = true;
            }

            return collision;
        }
    }
}
