using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrickBreaker
{
    class AbstractMenuScreen : AbstractScreen
    {
        protected String[] menuItems;
        Int32 selectedIndex = 0;

        Color normal = Color.White;
        Color hilite = Color.Yellow;

        public SpriteFont letteringSprite {get; private set;}


        Vector2 position;
        float width = 0f;
        float height = 0f;



        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                if (selectedIndex < 0)
                    selectedIndex = 0;
                if (selectedIndex >= menuItems.Length)
                    selectedIndex = menuItems.Length - 1;
            }
        }

        public AbstractMenuScreen(BrickBreakerGame game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
           
        }

        public override void LoadContent()
        {
            letteringSprite = GameHandle.Content.Load<SpriteFont>("menufont");
            base.LoadContent();
            MeasureMenu();
        }


        public void Initialize(String[] menuItems)
        {
            this.menuItems = menuItems;
        }







        public override void OnShow()
        {
            base.OnShow();

            selectedIndex = 0;
        }





        private void MeasureMenu()
        {
            height = 0;
            width = 0;
            foreach (string item in menuItems)
            {
                Vector2 size = letteringSprite.MeasureString(item);
                if (size.X > width)
                    width = size.X;
                height += letteringSprite.LineSpacing + 5;
            }

            position = new Vector2(
                (ScreenBounds.Width - width) / 2,
                (ScreenBounds.Height - height) / 2);
        }


        protected override void ProcessInput()
        {
            if (kbdHelper.KeyUp(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
            }
            if (kbdHelper.KeyUp(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }
            base.ProcessInput();
        }


        public override void Draw()
        {
            base.Draw();
            Vector2 location = position;
            Color tint;

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedIndex)
                {
                    tint = hilite;
                }
                else
                {
                    tint = normal;
                }

                SpriteBatchHandle.DrawString(
                    letteringSprite,
                    menuItems[i],
                    location,
                    tint);
                location.Y += letteringSprite.LineSpacing + 5;
            }
        }


    }
}

