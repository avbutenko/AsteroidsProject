using AsteroidsProject.Infrastructure.Services;
using UnityEngine.InputSystem;
using Zenject;
using System;

namespace AsteroidsProject.EngineRelated.Services
{
    public class InputService : IInitializable, IDisposable, IInputService
    {
        private InputActions inputActions;

        public void Initialize()
        {
            inputActions = new InputActions();
            inputActions.Ship.Enable();
        }

        public bool IsAccelerating => inputActions.Ship.Accelerate.phase == InputActionPhase.Performed;
        public bool IsRotating => inputActions.Ship.Rotate.phase == InputActionPhase.Performed;
        public float RotationDirection => inputActions.Ship.Rotate.ReadValue<float>();

        public void Dispose()
        {
            inputActions.Ship.Disable();
        }
    }
}