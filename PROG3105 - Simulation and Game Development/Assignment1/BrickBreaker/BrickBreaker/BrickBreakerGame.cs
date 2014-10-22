//using BrickBreaker.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace BrickBreaker
{

    public enum SCREEN_IDS
    {
        GAME_SCREEN = 0,
        PAUSE_SCREEN,
        START_SCREEN,
        HELP_SCREEN,
        SETTINGS_SCREEN
    }

    class BrickBreakerGame : Game
    {

        public GraphicsDeviceManager graphics { get; private set; }
        public Random Randomizer { get; private set; }
        public SpriteBatch SpriteBatchHandle { get; private set; }
        public GameState Env { get; set; }
        public Dictionary<SCREEN_IDS, AbstractScreen> Screens { get; private set; }
        public AbstractScreen CurrentScreen { get; private set; }
        public KeyboardState OldKeyboardState { get; private set; }
        public KeyboardState NewKeyboardState { get; private set; }
        public ResourceManager Configuration { get; private set; }
        public SCREEN_IDS previousScreenID { get; private set; }
        public SCREEN_IDS currentScreenID { get; private set; }
        public AbstractModal CurrentModal { get; set; }



        public BrickBreakerGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Randomizer = new Random();
            Screens = new Dictionary<SCREEN_IDS, AbstractScreen>();
            Configuration = new ResourceManager(typeof(Configuration));
            CurrentModal = null;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            //oldKeyboardState = Keyboard.GetState();

            
            SetupScreens();

            OldKeyboardState = Keyboard.GetState();
            NewKeyboardState = OldKeyboardState;

            NewGame();

            foreach (KeyValuePair<SCREEN_IDS, AbstractScreen> element in Screens)
            {
                element.Value.Initialize();
            }

            SwitchScreens(SCREEN_IDS.START_SCREEN);
            //SwitchScreens(SCREEN_IDS.GAME_SCREEN);

            base.Initialize();
        }

        protected void SetupScreens()
        {
            // Add screens here
            Screens.Add(SCREEN_IDS.GAME_SCREEN, new GameScreen(this, SpriteBatchHandle));
            Screens.Add(SCREEN_IDS.PAUSE_SCREEN, new PauseScreen(this, SpriteBatchHandle));
            Screens.Add(SCREEN_IDS.START_SCREEN, new StartMenu(this, SpriteBatchHandle));
            Screens.Add(SCREEN_IDS.HELP_SCREEN, new HelpScreen(this, SpriteBatchHandle));
            //Screens.Add(SCREEN_IDS.SETTINGS_SCREEN, new SettingsScreen(this, SpriteBatchHandle));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatchHandle = new SpriteBatch(GraphicsDevice);

            foreach (KeyValuePair<SCREEN_IDS, AbstractScreen> element in Screens)
            {
                element.Value.SpriteBatchHandle = SpriteBatchHandle;  //Is this necessary or should we modify the classes to not pass it in.
                element.Value.LoadContent();
            }

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            NewKeyboardState = Keyboard.GetState();


            if (CurrentModal == null)
            {
                CurrentScreen.Update();
            }
            else
            {
                CurrentModal.Update();
            }

            if (CurrentModal != null)
            {
                if (CurrentModal.TimeAtShow < 0 && CurrentModal.Expiry != ModalTimeout.INFINITY)
                {
                    CurrentModal.TimeAtShow = (long)Math.Floor(gameTime.TotalGameTime.TotalSeconds);
                }
                else if (CurrentModal.Expiry.GetHashCode() <= (gameTime.TotalGameTime.TotalSeconds - CurrentModal.TimeAtShow) && CurrentModal.Expiry != ModalTimeout.INFINITY)
                {
                    DismissModal();
                }
            }

            OldKeyboardState = NewKeyboardState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatchHandle.Begin();

            CurrentScreen.Draw();

            

            if (CurrentModal != null)
            {
                CurrentModal.Draw();
            }

            SpriteBatchHandle.End();

            base.Draw(gameTime);
        }

        public void SwitchScreens(SCREEN_IDS id)
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.OnHide();
            }
            
           
                CurrentScreen = Screens[id];


                previousScreenID = currentScreenID;
                currentScreenID = id;
            CurrentScreen.OnShow();
        }


        public void NewGame()
        {
            Env = new GameState();

            foreach (KeyValuePair<SCREEN_IDS, AbstractScreen> screenElm in Screens)
            {
                screenElm.Value.OnNewGame();
            }
        }

        public void EndGame(bool hasWon)
        {
            SimpleModal modal = new SimpleModal(this, SpriteBatchHandle);
            modal.Expiry = ModalTimeout.LONG;

            if (hasWon)
            {
                modal.Show("You win!");
            }
            else
            {
                modal.Show("You died... :(");
            }

            SwitchScreens(SCREEN_IDS.START_SCREEN);
        }

        public void SaveGame()
        {
            SimpleModal modal = new SimpleModal(this, SpriteBatchHandle);
            modal.Expiry = ModalTimeout.SHORT;
            modal.Show("Saving game...");

            Env.Save(Configuration.GetString("SAVE_PATH"));
        }

        public void LoadGame()
        {
            SimpleModal modal = new SimpleModal(this, SpriteBatchHandle);
            modal.Expiry = ModalTimeout.SHORT;
            modal.Show("Loading game...");

            Env = GameState.Load(Configuration.GetString("SAVE_PATH"));

            if (Env != null)
            {
                // Show a modal mentioning that the game was loaded 
            }
            else
            {
                // Loading the game failed.
                NewGame();
            }

            CurrentScreen.StateRefresh();
        }

        public virtual void AttachModal(AbstractModal modal)
        {
            CurrentModal = modal;
        }

        public virtual void DismissModal()
        {
            if (CurrentModal != null)
            {
                CurrentModal = null;
            }
        }
        
    }
}
