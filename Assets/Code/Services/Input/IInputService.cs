using UnityEngine;

namespace Services.Input
{
    public interface IInputService
    {
        Vector2 Direction { get; }
        void SetJoystick(Joystick joystick);
    }
}