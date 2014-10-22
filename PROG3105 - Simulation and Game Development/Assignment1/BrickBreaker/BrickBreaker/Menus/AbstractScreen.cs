using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    class AbstractScreen
    {
        public BrickBreakerGame GameHandle { get; set; }
        public SpriteBatch SpriteBatchHandle { get; set; }
        public List<AbstractGameObject> ObjectDrawList { get; private set; }

        public Rectangle ScreenBounds { get; private set; }

        public AbstractScreen()
        {

        }
        public AbstractScreen(BrickBreakerGame game, SpriteBatch spriteBatch)
        {
            GameHandle = game;
            SpriteBatchHandle = spriteBatch;

            ObjectDrawList = new List<AbstractGameObject>();
        }

        public virtual void Initialize()
        {
            ScreenBounds = new Rectangle(
                GameHandle.GraphicsDevice.Viewport.X,
                GameHandle.GraphicsDevice.Viewport.Y,
                GameHandle.GraphicsDevice.Viewport.Width,
                GameHandle.GraphicsDevice.Viewport.Height
            );
        }
        public virtual void LoadContent()
        {

        }

        public virtual void Update()
        {
            ProcessInput();

            foreach (AbstractGameObject obj in ObjectDrawList)
            {
                obj.Update();
            }

        }

        protected virtual void ProcessInput()
        {

        }

        public virtual void Draw()
        {
            foreach (AbstractGameObject obj in ObjectDrawList)
            {
                obj.Draw(SpriteBatchHandle);
            }
        }
    }
}
