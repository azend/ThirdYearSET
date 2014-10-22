using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    class StartMenu : AbstractMenuScreen
    {

        public StartMenu(BrickBreakerGame game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            menuItems = new String[] { "New Game", "Load", "Settings", "Help", "About", "Exit" };
            //throw new NotImplementedException();
        }

        protected override void ProcessInput()
        {
            base.ProcessInput();
            if (kbdHelper.KeyUp(Keys.Enter))
            {
                SimpleModal modal = new SimpleModal(GameHandle, SpriteBatchHandle);

                switch (SelectedIndex)
                {
                    case 0:
                        GameHandle.NewGame();
                        GameHandle.SwitchScreens(SCREEN_IDS.GAME_SCREEN);
                        break;
                    case 1:
                        GameHandle.LoadGame();
                        //TODO ADD GAME LOADED POPUP
                        GameHandle.SwitchScreens(SCREEN_IDS.GAME_SCREEN); 
                        break;
                    case 2:
                        modal.Show("Settings\n\nFeature in next version.");

                        //GameHandle.SwitchScreens(SCREEN_IDS.SETTINGS_SCREEN);
                        break;
                    case 3:
                        modal.Show("Help\n\nFeature in next version.");
                        //GameHandle.SwitchScreens(SCREEN_IDS.HELP_SCREEN);
                        break;
                    case 4:
                        modal.Expiry = ModalTimeout.INFINITY;
                        modal.Show("About - v0.1 Alpha\n\nThis game was made by a team of two\ndedicated programmers who made their\nbest effort finishing this game on\ntime.");
                        break;
                    case 5:
                        GameHandle.Exit();
                        break;

                }
            }

           

            

        }

        public override void Draw()
        {
            base.Draw();
        }








    }
}
