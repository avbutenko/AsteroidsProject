using AsteroidsProject.Infrastructure.Services;
using UnityEngine.InputSystem;
using System;
using Zenject;

namespace AsteroidsProject.Services
{
    public class InputService : IInitializable, IDisposable, IShipInputService, InputActions.IShipActions
    {
        public event Action OnAcceleration;
        public event Action OnStopAcceleration;
        public event Action OnRotation;
        public event Action OnStopRotation;
        private InputActions inputActions;

        public void Initialize()
        {
            inputActions = new InputActions();
            inputActions.Ship.SetCallbacks(this);
            inputActions.Ship.Enable();
        }

        public float RotationDirection => inputActions.Ship.Rotate.ReadValue<float>();

        public void OnAccelerate(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    OnAcceleration?.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    OnStopAcceleration?.Invoke();
                    break;
            }
        }

        public void OnRotate(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    OnRotation?.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    OnStopRotation?.Invoke();
                    break;
            }
        }

        public void Dispose()
        {
            inputActions.Ship.Disable();
        }
    }
}