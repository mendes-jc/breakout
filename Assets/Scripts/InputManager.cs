using UnityEngine;

namespace Breakout
{
    public class InputManager : UnitySingleton<InputManager>
    {
        public float HorizontalInput { get; private set; }
        public bool LaunchBallInput { get; private set; }

        public bool InputDisabled { get; private set; }

        private void Update()
        {
            HorizontalInput = !InputDisabled ? Input.GetAxis("Horizontal") : 0;
            LaunchBallInput = !InputDisabled && Input.GetKeyDown(KeyCode.Space);
        }

        public void DisablePlayerInput()
        {
            InputDisabled = true;
        }

        public void EnablePlayerInput()
        {
            InputDisabled = false;
        }
    }
}