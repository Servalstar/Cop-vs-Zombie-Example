using UnityEngine;

namespace Services.Input
{
    public class StandaloneInputService : InputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public override Vector2 Direction
        {
            get
            {
                Vector2 direction = JoystickDirection();

                if (direction == Vector2.zero)
                    direction = UnityAxis();

                return direction;
            }
        }

        private Vector2 UnityAxis() =>
            new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
    }
}