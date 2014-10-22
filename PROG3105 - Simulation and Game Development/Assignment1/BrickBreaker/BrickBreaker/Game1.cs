#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace BrickBreaker
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        KeyboardState oldKeyboardState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random rand;

        GameState env;
        HUD hud;

        Wall topWall;
        Wall leftWall;
        Wall rightWall;

        const int NUM_BRICKS_X = 10;
        const int NUM_BRICKS_Y = 5;
        Brick[,] bricks = new Brick[NUM_BRICKS_X, NUM_BRICKS_Y];

        const float PERCENTAGE_WALL_WIDTH = 0.1f;

        Ball ball;

        Paddle paddle;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            rand = new Random();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            env = new GameState();

            topWall = new Wall(
            new Vector2(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height * PERCENTAGE_WALL_WIDTH),
            new Vector3(0f, 0f, 0f),
            Color.Green
                );

            leftWall = new Wall(
            new Vector2(graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH, graphics.GraphicsDevice.Viewport.Height),
            new Vector3(0f, 0f, 0f),
            Color.Green
            );

            rightWall = new Wall(
                new Vector2(graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH, graphics.GraphicsDevice.Viewport.Height),
                new Vector3(graphics.GraphicsDevice.Viewport.Width - (graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH), 0, 0),
                Color.Green);




            for (int y = 0; y < NUM_BRICKS_Y; y++)
            {
                
                for (int x = 0; x < NUM_BRICKS_X; x++)
                {
                    bricks[x, y] = new Brick();
                    bricks[x, y].Position = new Vector3(
                        ((graphics.GraphicsDevice.Viewport.Width - (graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH * 2)) / NUM_BRICKS_X) * x + (graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH) + x * 2 ,
                        ((graphics.GraphicsDevice.Viewport.Height / 4) / NUM_BRICKS_Y) * y + (graphics.GraphicsDevice.Viewport.Height * PERCENTAGE_WALL_WIDTH) + y * 2, 
                        0f
                    );
                    bricks[x, y].Size = new Vector2(
                        (graphics.GraphicsDevice.Viewport.Width - (graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH * 2)) / NUM_BRICKS_X,
                        (graphics.GraphicsDevice.Viewport.Height / 4) / NUM_BRICKS_Y
                    );

                    //bricks[x, y].Position = new Vector3(
                    //    ((graphics.GraphicsDevice.Viewport.Width - (graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH * 2)) / NUM_BRICKS_X) * x,
                    //    ((graphics.GraphicsDevice.Viewport.Height - (graphics.GraphicsDevice.Viewport.Height * PERCENTAGE_WALL_WIDTH * 2)) / NUM_BRICKS_Y) * y + (graphics.GraphicsDevice.Viewport.Height * PERCENTAGE_WALL_WIDTH * 3),
                    //    0f
                    //);
                    //bricks[x, y].Size = new Vector2(
                    //    (graphics.GraphicsDevice.Viewport.Width - (graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH * 2)) / NUM_BRICKS_X,
                    //    (graphics.GraphicsDevice.Viewport.Height - (graphics.GraphicsDevice.Viewport.Height * PERCENTAGE_WALL_WIDTH * 2)) / NUM_BRICKS_Y
                    //);
                }
            }

            ball = new Ball();

            LaunchBall();

            paddle = new Paddle();
            paddle.Size = new Vector2(80, 10);
            paddle.Position = new Vector3(graphics.GraphicsDevice.Viewport.Width / 2 - paddle.Size.X / 2, graphics.GraphicsDevice.Viewport.Height - 80 - paddle.Size.Y / 2, 0);

            hud = new HUD(env);
            hud.Position = new Vector3((graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH) / 2, (graphics.GraphicsDevice.Viewport.Height * PERCENTAGE_WALL_WIDTH) / 4, 0);

            GameState state = new GameState();
            state.Save("state.txt");
            

            oldKeyboardState = Keyboard.GetState();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            topWall.Texture = Content.Load<Texture2D>("Wall");
            leftWall.Texture = Content.Load<Texture2D>("Wall");
            rightWall.Texture = Content.Load<Texture2D>("Wall");

            Texture2D brickTexture = Content.Load<Texture2D>("Brick");
            for (int y = 0; y < NUM_BRICKS_Y; y++)
            {
                for (int x = 0; x < NUM_BRICKS_X; x++)
                {
                    bricks[x, y].Texture = brickTexture;
                }
            }

            ball.Texture = Content.Load<Texture2D>("Ball");

            paddle.Texture = Content.Load<Texture2D>("Ball");


            hud.Font = Content.Load<SpriteFont>("menufont");

            
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateInput();

            DetectCollisions();
            UpdateLives();
            UpdateScore();

            

            ball.Update();

            paddle.Update();

            hud.Update();

            

            base.Update(gameTime);
        }

        private void UpdateInput()
        {
            KeyboardState newKeyboardState = Keyboard.GetState();

            if (newKeyboardState.IsKeyDown(Keys.A) && !oldKeyboardState.IsKeyDown(Keys.A))
            {
                paddle.MoveLeft();
            }
            else if (newKeyboardState.IsKeyDown(Keys.D) && !oldKeyboardState.IsKeyDown(Keys.D))
            {
                paddle.MoveRight();
            }
            else if (!newKeyboardState.IsKeyDown(Keys.A) && !newKeyboardState.IsKeyDown(Keys.D))
            {
                paddle.MoveStop();
            }

            oldKeyboardState = newKeyboardState;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            topWall.Draw(spriteBatch);
            leftWall.Draw(spriteBatch);
            rightWall.Draw(spriteBatch);

            for (int y = 0; y < NUM_BRICKS_Y; y++)
            {
                for (int x = 0; x < NUM_BRICKS_X; x++)
                {
                    bricks[x, y].Draw(spriteBatch);
                }
            }

            ball.Draw(spriteBatch);

            paddle.Draw(spriteBatch);

            hud.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DetectCollisions()
        {
            TestCollision(ball, hud);

            TestCollision(ball, topWall);
            TestCollision(ball, leftWall);
            TestCollision(ball, rightWall);

            foreach (Brick brick in bricks)
            {
                TestCollision(ball, brick);
            }

            TestCollision(ball, paddle);
        }

        private void TestCollision(AbstractGameObject obj1, AbstractGameObject obj2)
        {
            //
            // Warning: This should really be the responsibility of the object to perform the action
            // but it has yet to be migrated.
            //

            if (obj1.DetectCollision(obj2))
            {
                // Check for collision action
                if (obj1.OnCollision != null)
                {
                    obj1.OnCollision.React(obj1, obj2);
                }

                if (obj2.OnCollision != null)
                {
                    obj2.OnCollision.React(obj2, obj1);
                }
            }
        }

        private void LaunchBall()
        {
            //ball.Position = new Vector3(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Width - graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH, 0.0f);
            ball.Position = new Vector3(200, 200, 0);
            ball.Size = new Vector2(50, 50);
            ball.Move = new Vector3(1f, -1f, 0);
            //ball.Move = new Vector3(1f, 0f, 0);
        }

        private bool CheckIfOnMap(AbstractGameObject obj)
        {
            bool isOnMap = false;

            Rectangle screenBounds = new Rectangle(
                graphics.GraphicsDevice.Viewport.X,
                graphics.GraphicsDevice.Viewport.Y,
                graphics.GraphicsDevice.Viewport.Width,
                graphics.GraphicsDevice.Viewport.Height
            );

            if (screenBounds.Contains(new Point((int)ball.Position.X, (int)ball.Position.Y)))
            {
                isOnMap = true;
            }

            return isOnMap;
        }

        private void UpdateLives()
        {
            if (!CheckIfOnMap(ball))
            {
                env.Lives--;
                LaunchBall();
            }
        }

        private void UpdateScore()
        {
            int score = 0;
            foreach (Brick brick in bricks)
            {
                if (!brick.Show)
                {
                    score += brick.BreakValue;
                }
            }
            env.Points = score;
        }
    }


}
