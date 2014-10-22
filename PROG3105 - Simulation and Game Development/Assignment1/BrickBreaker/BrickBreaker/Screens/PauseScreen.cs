//#define myDebug




using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    class PauseScreen : AbstractMenuScreen
    {
        

        public PauseScreen(BrickBreakerGame game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
#if myDebug
            menuItems = new String[] { "Resume","New Game","Save", "Load", "Settings", "Help", "Main Menu", "DEBUG: Exit" };
#else
            menuItems = new String[] { "Resume","New Game","Save", "Load", "Settings", "Help", "Main Menu" };
#endif
        }

        

        public override void Draw()
        {
            base.Draw();

            SpriteBatchHandle.DrawString(
                letteringSprite,
                "PAUSE",
                new Vector2(10, 0),
                Color.White
            );
        }

        
        protected override void ProcessInput()
        {
            base.ProcessInput();
            if (kbdHelper.KeyUp(Keys.Enter))
            {
                SimpleModal modal = new SimpleModal(GameHandle, SpriteBatchHandle);
                switch (SelectedIndex)
                {
                         //"Resume","New Game","Save", "", "", "Help", "Exit"
                    case 0: //Resume
                        GameHandle.SwitchScreens(SCREEN_IDS.GAME_SCREEN);
                        break;
                    case 1: //New Game
                        GameHandle.NewGame();
                        GameHandle.SwitchScreens(SCREEN_IDS.GAME_SCREEN);
                        break;

                    case 2: //Save
                        GameHandle.SaveGame();                       
                        //TODO ADD GAME SAVED
                        break;
                    case 3://Load
                        GameHandle.LoadGame();
                        //TODO ADD GAME LOADED POPUP
                        GameHandle.SwitchScreens(SCREEN_IDS.GAME_SCREEN); 
                        break;
                    case 4://Settings
                        modal.Show("Settings\n\nFeature in next version.");
                        //GameHandle.SwitchScreens(SCREEN_IDS.SETTINGS_SCREEN);
                        break;
                    case 5:
                        modal.Show("Help\n\nFeature in next version.");
                        //GameHandle.SwitchScreens(SCREEN_IDS.HELP_SCREEN);
                        break;
                    case 6:
                        GameHandle.SwitchScreens(SCREEN_IDS.START_SCREEN);
                        break;
#if myDebug
                    case 7:
                        GameHandle.Exit();
                        break;
#endif
                }
            }
            else if (kbdHelper.KeyUp(Keys.P) || kbdHelper.KeyUp(Keys.Escape))
            {
                GameHandle.SwitchScreens(SCREEN_IDS.GAME_SCREEN);
            }
                
           

            

        }
    }
}
