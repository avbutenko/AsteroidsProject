using Assets.Scripts.Infrastructure.Services;
using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.Units.Ship;
using AsteroidsProject.Settings.Units.Ship;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.Units.Ship
{
    public class ShipInertMovingState : ShipBaseMovingState
    {
        public ShipInertMovingState(
            IShipStateMachine stateMachine,
            IShipInputService inputService,
            IShipPresenter presenter,
            ShipSettingsInstaller.ShipMovementSettings settings,
            IRotationService rotationService,
            ITeleportationService teleportationService)
            : base(stateMachine, inputService, presenter, settings, rotationService, teleportationService)
        {
        }

        public override void Enter()
        {
            inputService.OnAcceleration += InputService_OnAcceleration;
        }

        public override void Tick()
        {
            if (GetCurrentSpeed() > 0)
            {
                base.Tick();
            }
            else
            {
                if (inputService.RotationDirection == 0)
                {
                    stateMachine.Enter<ShipIdleState>();
                }
                else
                {
                    stateMachine.Enter<ShipIdleRotatingState>();
                }
            }
        }

        public override void Exit()
        {
            inputService.OnAcceleration -= InputService_OnAcceleration;
        }

        protected override float GetCurrentSpeed()
        {
            return presenter.Speed - settings.MovementAcceleration * Time.deltaTime;
        }

        private void InputService_OnAcceleration()
        {
            stateMachine.Enter<ShipAcceleratedMovingState>();
        }

        public class Factory : PlaceholderFactory<IShipStateMachine, ShipInertMovingState>
        {
        }
    }
}