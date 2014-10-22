using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    class HUD : AbstractGameObject
    {
        public GameState env { get; set; }
        public SpriteFont Font { get; set; }

        public HUD()
            : base()
        {
            env = null;
        }
        public HUD(GameState env)
            : base()
        {
            this.env = env;
        }

        public override void Update()
        {
            //throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(Font, 
                String.Format("POINTS {0}  LIVES {1}", env.Points, env.Lives), 
                new Vector2(Position.X, Position.Y), 
                Color.White
            );

        }
    }
}
