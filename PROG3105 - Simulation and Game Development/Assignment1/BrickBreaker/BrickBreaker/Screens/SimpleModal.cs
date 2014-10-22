using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    class SimpleModal : AbstractModal
    {
        public string ModalPhrase { get; private set; }
        private SpriteFont font;

        public SimpleModal(BrickBreakerGame game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            Expiry = ModalTimeout.INFINITY;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            font = GameHandle.Content.Load<SpriteFont>("menuFont");
        }

        public void Show(string modalText)
        {
            ModalPhrase = modalText;

            GameHandle.AttachModal(this);
        }

        public override void Update()
        {
            base.Update();

            if (kbdHelper.KeyUp(Keys.Enter))
            {
                if (GameHandle.CurrentModal == this)
                {
                    GameHandle.DismissModal();
                }
            }
        }

        public override void Draw()
        {
            base.Draw();

            SpriteBatchHandle.DrawString(font, ModalPhrase, new Vector2(Bounds.Left, Bounds.Top), Color.White);

            SpriteBatchHandle.DrawString(font, "Press enter to continue", new Vector2(Bounds.Left, Bounds.Bottom - font.LineSpacing), Color.White);
        }
    }
}
