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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Wall topWall;
        Wall leftWall;
        Wall rightWall;

        const int NUM_BRICKS_X = 10;
        const int NUM_BRICKS_Y = 5;
        Brick[,] bricks = new Brick[NUM_BRICKS_X, NUM_BRICKS_Y];

        const float PERCENTAGE_WALL_WIDTH = 0.1f;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            topWall = new Wall();
            topWall.Size = new Vector2(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height * PERCENTAGE_WALL_WIDTH);

            leftWall = new Wall();
            leftWall.Size = new Vector2(graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH, graphics.GraphicsDevice.Viewport.Height);

            rightWall = new Wall();
            rightWall.Position = new Vector3(graphics.GraphicsDevice.Viewport.Width - (graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH), 0, 0);
            rightWall.Size = new Vector2(graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH, graphics.GraphicsDevice.Viewport.Height);



            for (int y = 0; y < NUM_BRICKS_Y; y++)
            {
                for (int x = 0; x < NUM_BRICKS_X; x++)
                {
                    bricks[x, y] = new Brick();
                    bricks[x, y].Position = new Vector3(
                        ((graphics.GraphicsDevice.Viewport.Width - (graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH * 2)) / NUM_BRICKS_X) * x,
                        ((graphics.GraphicsDevice.Viewport.Height - (graphics.GraphicsDevice.Viewport.Height * PERCENTAGE_WALL_WIDTH * 2)) / NUM_BRICKS_Y) * y + (graphics.GraphicsDevice.Viewport.Height * PERCENTAGE_WALL_WIDTH * 3), 
                        0f
                    );
                    bricks[x, y].Size = new Vector2(
                        (graphics.GraphicsDevice.Viewport.Width - (graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH * 2)) / NUM_BRICKS_X,
                        (graphics.GraphicsDevice.Viewport.Height - (graphics.GraphicsDevice.Viewport.Height * PERCENTAGE_WALL_WIDTH * 2)) / NUM_BRICKS_Y
                    );
                }
            }


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

            Wall.Texture = Content.Load<Texture2D>("Wall");

            Texture2D brickTexture = Content.Load<Texture2D>("Brick");
            for (int y = 0; y < NUM_BRICKS_Y; y++)
            {
                for (int x = 0; x < NUM_BRICKS_X; x++)
                {
                    bricks[x, y].Texture = brickTexture;
                }
            }
            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            // TODO: Add your update logic here

            base.Update(gameTime);
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

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
