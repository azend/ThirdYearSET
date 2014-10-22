using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace BrickBreaker
{
    [Serializable()]
    class GameState
    {
        // User scores
        public int Points { get; set; }
        public int Lives { get; set; }
        public bool[,] Bricks { get; set; }
        public SaveableVector3 BallPosition { get; set; }
        public SaveableVector3 BallMovement { get; set; }
        public SaveableVector3 PaddlePosition { get; set; }
        public bool GameStarted { get; set; }

        public GameState()
        {
            Points = 0;
            Lives = 0;
            Bricks = new bool[1, 1];
            BallPosition = new SaveableVector3(0, 0, 0);
            BallMovement = new SaveableVector3(0, 0, 0);
            PaddlePosition = new SaveableVector3(0, 0, 0);
            GameStarted = false;

            SetDefaults();
        }

        private void SetDefaults()
        {
            ResourceManager resources = new ResourceManager(typeof(Configuration));

            try
            {
                Points = Convert.ToInt32(resources.GetString("NUM_POINTS"));
                Lives = Convert.ToInt32(resources.GetString("NUM_LIVES"));

                int numBricksX = Convert.ToInt32(resources.GetString("NUM_BRICKS_X"));
                int numBricksY = Convert.ToInt32(resources.GetString("NUM_BRICKS_Y"));
                Bricks = new bool[numBricksX, numBricksY];

                for (int x = 0; x < numBricksX; x++)
                {
                    for (int y = 0; y < numBricksY; y++)
                    {
                        Bricks[x, y] = true;
                    }
                }

                int ballPosX = Convert.ToInt32(resources.GetString("BALL_POS_X"));
                int ballPosY = Convert.ToInt32(resources.GetString("BALL_POS_Y"));
                int ballPosZ = Convert.ToInt32(resources.GetString("BALL_POS_Z"));
                BallPosition = new SaveableVector3(ballPosX, ballPosY, ballPosZ);

                int ballMoveX = Convert.ToInt32(resources.GetString("BALL_MOVE_X"));
                int ballMoveY = Convert.ToInt32(resources.GetString("BALL_MOVE_Y"));
                int ballMoveZ = Convert.ToInt32(resources.GetString("BALL_MOVE_Z"));
                BallMovement = new SaveableVector3(ballMoveX, ballMoveY, ballMoveZ);

                int paddlePosX = Convert.ToInt32(resources.GetString("PADDLE_POS_X"));
                int paddlePosY = Convert.ToInt32(resources.GetString("PADDLE_POS_Y"));
                int paddlePosZ = Convert.ToInt32(resources.GetString("PADDLE_POS_Z"));
                PaddlePosition = new SaveableVector3(paddlePosX, paddlePosY, paddlePosZ);
                

            }
            catch (Exception e)
            {
                
            }
        }

        public void Save(string path)
        {
            Stream fileStream = File.OpenWrite(path);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(fileStream, this);
            fileStream.Close();

            
        }

        public static GameState Load(string path)
        {
            GameState state = null;

            if (File.Exists(path))
            {
                Stream fileStream = File.OpenRead(path);
                BinaryFormatter deserializer = new BinaryFormatter();
                state = (GameState)deserializer.Deserialize(fileStream);
                fileStream.Close();
            }

            return state;
        }
    }
}
