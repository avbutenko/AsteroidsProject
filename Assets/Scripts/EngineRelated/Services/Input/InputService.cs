using AsteroidsProject.Infrastructure.Services;
using UnityEngine.InputSystem;
using Zenject;
using System;

namespace AsteroidsProject.EngineRelated.Services
{
    public class InputService : IInitializable, IDisposable, IInputService, InputActions.IShipActions
    {
        private InputActions inputActions;
        private bool isAccelerating;
        private bool isInerting;
        private bool isRotating;

        public void Initialize()
        {
            inputActions = new InputActions();
            inputActions.Ship.SetCallbacks(this);
            inputActions.Ship.Enable();
        }

        public void OnAccelerate(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    isAccelerating = true;
                    isInerting = false;
                    break;
                case InputActionPhase.Canceled:
                    isAccelerating = false;
                    isInerting = true;
                    break;
            }
        }

        public void OnRotate(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    isRotating = true;
                    break;
                case InputActionPhase.Canceled:
                    isRotating = false;
                    break;
            }
        }

        public bool IsAccelerating => isAccelerating;
        public bool IsInerting => isInerting;
        public bool IsRotating => isRotating;

        public float RotationDirection => inputActions.Ship.Rotate.ReadValue<float>();

        public void Dispose()
        {
            inputActions.Ship.Disable();
        }
    }
}