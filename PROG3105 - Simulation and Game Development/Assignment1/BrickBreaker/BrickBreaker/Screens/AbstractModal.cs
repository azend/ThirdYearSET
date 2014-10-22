using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickBreaker
{

    enum ModalTimeout
    {
        INFINITY = -1,
        SHORT = 5,
        LONG = 20
    }
    abstract class AbstractModal : AbstractScreen
    {
        public ModalTimeout Expiry { get; set; }
        public long TimeAtShow { get; set; }
        public Rectangle Bounds {  get; set; }
        public Color BackgroundColor { get; set; }
        private Texture2D Texture { get; set; }

        public AbstractModal(BrickBreakerGame game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            BackgroundColor = new Color(0xCC, 0xCC, 0xCC, 0xff);
            TimeAtShow = -1;

            Initialize();
            LoadContent();
        }

        

        private void SetDefaultBounds()
        {
            Bounds = new Rectangle((int)Math.Round(ScreenBounds.Width / 4.0f), (int)Math.Round(ScreenBounds.Height / 4.0f), (int)Math.Round(ScreenBounds.Width / 2.0f), (int)Math.Round(ScreenBounds.Height / 2.0f)); 
        }

        public override void Initialize()
        {
            base.Initialize();

            SetDefaultBounds();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            Texture = GameHandle.Content.Load<Texture2D>("Brick");
        }

        

        public override void Update()
        {
            base.Update();

        }


        public override void Draw()
        {
            base.Draw();

            SpriteBatchHandle.Draw(Texture, Bounds, BackgroundColor);
        }

    }
}
