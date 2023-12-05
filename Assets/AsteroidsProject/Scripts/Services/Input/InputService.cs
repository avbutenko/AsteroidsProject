using AsteroidsProject.Shared;
using UnityEngine.InputSystem;
using Zenject;
using System;

namespace AsteroidsProject.Services
{
    public class InputService : IInitializable, IDisposable, IInputService
    {
        private InputActions inputActions;

        public void Initialize()
        {
            inputActions = new InputActions();
            inputActions.Ship.Enable();
            inputActions.UI.Enable();
        }

        public bool IsAccelerating => inputActions.Ship.Accelerate.phase == InputActionPhase.Performed;
        public bool IsDeaccelerating => inputActions.Ship.Accelerate.WasReleasedThisFrame() == true;
        public bool IsRotating => inputActions.Ship.Rotate.ReadValue<float>() != 0f;
        public float RotationDirection => inputActions.Ship.Rotate.ReadValue<float>();
        public bool IsPrimaryWeaponAttackPerformed => inputActions.Ship.PrimaryWeaponAttack.phase == InputActionPhase.Performed;
        public bool IsSecondaryWeaponAttackPerformed => inputActions.Ship.SecondaryWeaponAttack.phase == InputActionPhase.Performed;
        public bool IsPausePerformed => inputActions.UI.Pause.phase == InputActionPhase.Performed;

        public void Dispose()
        {
            inputActions.Ship.Disable();
            inputActions.UI.Disable();
        }
    }
}