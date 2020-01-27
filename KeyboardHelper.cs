
using Microsoft.Xna.Framework.Input;

namespace ParticleSystem
{
    public class KeyboardHelper
    {
        private static KeyboardState currentState;
        private static KeyboardState previousState;

        public static void UpdateState()
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
        }

        public static bool IsDown(Keys key)
        {
            return currentState.IsKeyDown(key);
        }

        public static bool IsPressed(Keys key)
        {
            return currentState.IsKeyDown(key) && !previousState.IsKeyDown(key);
        }
    }
}
