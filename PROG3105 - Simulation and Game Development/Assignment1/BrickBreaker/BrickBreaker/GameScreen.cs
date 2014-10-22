using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    class GameScreen : AbstractScreen
    {

        public HUD hud;

        Wall topWall;
        Wall leftWall;
        Wall rightWall;

        int numBricksX;
        int numBricksY;
        Brick[,] bricks;

        const float PERCENTAGE_WALL_WIDTH = 0.1f;

        Ball ball;

        Paddle paddle;

        Rectangle gameBounds;

        public GameScreen(BrickBreakerGame game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            InitializeComponents();
            InitializeObjectDrawList();
            InitializeState();
        }

        private void InitializeComponents()
        {
            topWall = new Wall(
            new Vector2(ScreenBounds.Width, ScreenBounds.Height * PERCENTAGE_WALL_WIDTH),
            new Vector3(0f, 0f, 0f),
            Color.Green
                );

            leftWall = new Wall(
            new Vector2(ScreenBounds.Width * PERCENTAGE_WALL_WIDTH, ScreenBounds.Height),
            new Vector3(0f, 0f, 0f),
            Color.Green
            );

            rightWall = new Wall(
                new Vector2(ScreenBounds.Width * PERCENTAGE_WALL_WIDTH, ScreenBounds.Height),
                new Vector3(ScreenBounds.Width - (ScreenBounds.Width * PERCENTAGE_WALL_WIDTH), 0, 0),
                Color.Green);


            gameBounds = new Rectangle((int)(ScreenBounds.Width * PERCENTAGE_WALL_WIDTH), (int)(ScreenBounds.Height * PERCENTAGE_WALL_WIDTH * 1.5f), (int)Math.Round(ScreenBounds.Width - 2 * (ScreenBounds.Width * PERCENTAGE_WALL_WIDTH)), (int)Math.Round(ScreenBounds.Height - (ScreenBounds.Height * PERCENTAGE_WALL_WIDTH)));

            Int32.TryParse(GameHandle.Configuration.GetString("NUM_BRICKS_X"), out numBricksX);
            Int32.TryParse(GameHandle.Configuration.GetString("NUM_BRICKS_Y"), out numBricksY);

            Rectangle brickBounds = new Rectangle(gameBounds.Left, (int)Math.Round(gameBounds.Top * 1.1), gameBounds.Width, (int)Math.Round(gameBounds.Height / 4f));

            bricks = new Brick[numBricksX, numBricksY];

            for (int y = 0; y < numBricksY; y++)
            {

                for (int x = 0; x < numBricksX; x++)
                {
                    bricks[x, y] = new Brick();
                    bricks[x, y].Position = new Vector3(
                        brickBounds.Left + x * (brickBounds.Width / numBricksX),
                        brickBounds.Top + y * (brickBounds.Height / numBricksY),
                        0f
                    );
                    bricks[x, y].Size = new Vector2(
                        (brickBounds.Width / numBricksX),
                        brickBounds.Height / numBricksY
                    );

                    // TODO Add proper points values
                    bricks[x, y].BreakValue = numBricksY - y;
                }
            }

            ball = new Ball();

            LaunchBall();

            paddle = new Paddle();
            paddle.Size = new Vector2(80, 10);
            paddle.Position = new Vector3(ScreenBounds.Width / 2 - paddle.Size.X / 2, ScreenBounds.Height - 80 - paddle.Size.Y / 2, 0);

            hud = new HUD(GameHandle.Env);
            hud.Position = new Vector3((ScreenBounds.Width * PERCENTAGE_WALL_WIDTH) / 2, (ScreenBounds.Height * PERCENTAGE_WALL_WIDTH) / 4, 0);

            
        }

        private void InitializeObjectDrawList()
        {
            ObjectDrawList.Clear();

            ObjectDrawList.Add(topWall);
            ObjectDrawList.Add(leftWall);
            ObjectDrawList.Add(rightWall);

            foreach (Brick brick in bricks)
            {
                ObjectDrawList.Add(brick);
            }

            ObjectDrawList.Add(ball);
            ObjectDrawList.Add(paddle);
            ObjectDrawList.Add(hud);
        }

        private void InitializeState()
        {
            UpdateBallState();
            UpdatePaddleState();
        }


        public override void LoadContent()
        {
            topWall.Texture = GameHandle.Content.Load<Texture2D>("Wall-top");
            leftWall.Texture = GameHandle.Content.Load<Texture2D>("Wall-side");
            rightWall.Texture = GameHandle.Content.Load<Texture2D>("Wall-side");

            Texture2D brickTexture = GameHandle.Content.Load<Texture2D>("Brick");
            for (int y = 0; y < numBricksY; y++)
            {
                for (int x = 0; x < numBricksX; x++)
                {
                    bricks[x, y].Texture = brickTexture;
                }
            }

            ball.Texture = GameHandle.Content.Load<Texture2D>("Ball");

            paddle.Texture = GameHandle.Content.Load<Texture2D>("Ball");


            hud.Font = GameHandle.Content.Load<SpriteFont>("menufont");
        }

        public override void Update()
        {
            base.Update();

            ProcessEnvironment();
        }

        protected override void ProcessInput()
        {
            // Debug
            if (kbdHelper.KeyUp(Keys.L))
            {
                GameHandle.LoadGame();
            }

            if (kbdHelper.KeyUp(Keys.P))
            {
                GameHandle.SwitchScreens(SCREEN_IDS.PAUSE_SCREEN);
            }

            if (kbdHelper.KeyDown(Keys.A) || kbdHelper.KeyDown(Keys.Left))
            {
               
                    paddle.MoveLeft();
                
            }
            else if (kbdHelper.KeyDown(Keys.D) || kbdHelper.KeyDown(Keys.Right))
            {
                paddle.MoveRight();
            }
            else if (kbdHelper.KeyUp(Keys.A) || kbdHelper.KeyUp(Keys.D) || kbdHelper.KeyUp(Keys.Left) || kbdHelper.KeyUp(Keys.Right))
            {
                paddle.MoveStop();
            }


            base.ProcessInput();
        }

        protected void ProcessEnvironment()
        {
            DetectCollisions();

            UpdateState();
        }


        private void LaunchBall()
        {
            int ballPosX = 0;
            int ballPosY = 0;
            int ballPosZ = 0;

            int.TryParse(GameHandle.Configuration.GetString("BALL_POS_X"), out ballPosX);
            int.TryParse(GameHandle.Configuration.GetString("BALL_POS_Y"), out ballPosY);
            int.TryParse(GameHandle.Configuration.GetString("BALL_POS_Z"), out ballPosZ);

            //ball.Position = new Vector3(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Width - graphics.GraphicsDevice.Viewport.Width * PERCENTAGE_WALL_WIDTH, 0.0f);
            ball.Position = new Vector3(ballPosX, ballPosY, ballPosZ);
            ball.Size = new Vector2(50, 50);
            ball.Move = new Vector3(2f, 4f, 0f);
            //ball.Move = new Vector3(1f, 0f, 0);
        }

        private bool CheckIfOnMap(AbstractGameObject obj)
        {
            bool isOnMap = false;

            if (ScreenBounds.Contains(new Point((int)ball.Position.X, (int)ball.Position.Y)))
            {
                isOnMap = true;
            }

            return isOnMap;
        }

        private void UpdateState()
        {
            UpdateLivesState();
            UpdateScoreState();
            UpdateBricksState();
            UpdateBallState();
            UpdatePaddleState();
        }

        private void UpdateLivesState()
        {
            if (!CheckIfOnMap(ball))
            {
                GameHandle.Env.Lives--;

                if (GameHandle.Env.Lives > 0)
                {
                    LaunchBall();
                }
                else
                {
                    GameHandle.EndGame(false);
                }
            }
        }

        private void UpdateScoreState()
        {
            int score = 0;
            foreach (Brick brick in bricks)
            {
                if (!brick.Show)
                {
                    score += brick.BreakValue;
                }
            }

            GameHandle.Env.Points = score;
        }

        private void UpdateBricksState()
        {
            for (int x = 0; x < numBricksX; x++)
            {
                for (int y = 0; y < numBricksY; y++)
                {
                    if (bricks[x, y].Show)
                    {
                        GameHandle.Env.Bricks[x, y] = true;
                    }
                    else
                    {
                        GameHandle.Env.Bricks[x, y] = false;
                    }
                }
            }
        }

        // TODO Refactor
        private void UpdateBallState()
        {
            GameHandle.Env.BallPosition = new SaveableVector3(ball.Position);
            GameHandle.Env.BallMovement = new SaveableVector3(ball.Move);
        }

        private void UpdatePaddleState()
        {
            GameHandle.Env.PaddlePosition = new SaveableVector3(paddle.Position);
        }

        private void UpdateWinState()
        {
            bool hasWon = true;

            foreach (Brick brick in bricks)
            {
                if (brick.Show)
                {
                    hasWon = false;
                }
            }

            if (hasWon)
            {
                GameHandle.EndGame(true);
            }
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

            if (!gameBounds.Contains(new Vector2(paddle.Position.X, paddle.Position.Y)) || !gameBounds.Contains(new Vector2(paddle.Position.X + paddle.Size.X, paddle.Position.Y)))
            {
                paddle.MoveStop();
            }
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

        public override void OnShow()
        {
            base.OnShow();

            StateRefresh();

            if (!GameHandle.Env.GameStarted)
            {
                LaunchBall();
            }
        }


        public override void StateRefresh()
        {
            //TODO Fix ball and paddle bug when resetting state. 
            base.StateRefresh();

            hud.env = GameHandle.Env;
            {
                for (int x = 0; x < numBricksX; x++)
                {
                    for (int y = 0; y < numBricksY; y++)
                    {
                        if (GameHandle.Env.Bricks[x, y])
                        {
                            bricks[x, y].Show = true;
                        }
                        else
                        {
                            bricks[x, y].Show = false;
                        }
                    }
                }
            }
            ball.Position = GameHandle.Env.BallPosition.ToVector3();
            ball.Move = GameHandle.Env.BallMovement.ToVector3();

            paddle.Position = GameHandle.Env.PaddlePosition.ToVector3();


        }

    }
}
