using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickBreaker
{
    class KeyboardHelper
    {
        private KeyboardState oldKeyboardState;
        private KeyboardState newKeyboardState;

        public KeyboardHelper(KeyboardState oldKeyboardState, KeyboardState newKeyboardState)
        {
            this.oldKeyboardState = oldKeyboardState;
            this.newKeyboardState = newKeyboardState;
        }

        public bool KeyUp(Keys k)
        {
            bool keyReleased = false;

            if (oldKeyboardState.IsKeyDown(k) && newKeyboardState.IsKeyUp(k))
            {
                keyReleased = true;
            }

            return keyReleased;
        }

        public bool KeyDown(Keys k)
        {
            bool keyPressed = false;

            if (oldKeyboardState.IsKeyUp(k) && newKeyboardState.IsKeyDown(k))
            {
                keyPressed = true;
            }

            return keyPressed;
        }
    }
}
