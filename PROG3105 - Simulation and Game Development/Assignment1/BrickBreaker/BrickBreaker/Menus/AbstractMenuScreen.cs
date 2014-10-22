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
        String[] menuItems;
        Int32 selectedIndex;

        Color normal = Color.White;
        Color hilite = Color.Yellow;

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        SpriteBatch spriteBatch;
        SpriteFont letteringSprite;
        Rectangle imageRectangle;

        Vector2 position;
        float width = 0f;
        float height = 0f;

        Game game;


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

        public AbstractMenuScreen(BrickBreakerGame game, SpriteBatch spriteBatch, SpriteFont letteringSprite, String[] menuItems)
            : base(game, spriteBatch)
        {
            this.menuItems = menuItems;
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.letteringSprite = letteringSprite;
            imageRectangle = new Rectangle( 0, 0,game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
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
                (game.Window.ClientBounds.Width - width) / 2,
                (game.Window.ClientBounds.Height - height) / 2);
        }


        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) &&
                oldKeyboardState.IsKeyDown(theKey);
        }

        public override void Update()
        {
            keyboardState = Keyboard.GetState();

            if (CheckKey(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
            }
            if (CheckKey(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }
            base.Update();

            oldKeyboardState = keyboardState;
        }

        public override void Draw()
        {
            base.Draw();
            Vector2 location = position;
            Color tint;

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedIndex)
                    tint = hilite;
                else
                    tint = normal;
                spriteBatch.DrawString(
                    letteringSprite,
                    menuItems[i],
                    location,
                    tint);
                location.Y += letteringSprite.LineSpacing + 5;
            }
        }


    }
}
