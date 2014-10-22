using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    class HelpScreen: AbstractMenuScreen
    {
     
              
        public HelpScreen(BrickBreakerGame game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            menuItems = new String[]
            {
                "Title",
                "FirstLine",
                "SecondLine",
                "ThirdLine",
                "FourthLine",
                "FifthLine",
                "Return"
            };
            
            
        }

        public override void Initialize()
        {
            
            base.Initialize();
        }

        public override void OnShow()
        {
            SelectedIndex = menuItems.Length - 1;
        }

        protected override void ProcessInput()
        {
            if (kbdHelper.KeyUp(Keys.Enter))
            {
                GameHandle.SwitchScreens(GameHandle.previousScreenID);
            }
        }




    }
}
