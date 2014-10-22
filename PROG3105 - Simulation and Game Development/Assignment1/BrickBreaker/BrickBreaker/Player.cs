using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    class Player
    {
        public int Points { get; set; }
        public int Lives { get; set; }
        public int PaddleSpeed { get; set; }

        public Player()
        {
            Points = 0;
            Lives = 0;
            PaddleSpeed = 0;
        }
    }
}
