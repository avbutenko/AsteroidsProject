using Assets.Scripts.Infrastructure.Services;
using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.Units.Ship;
using AsteroidsProject.Settings.Units.Ship;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.Units.Ship
{
    public class ShipAcceleratedMovingState : ShipBaseMovingState
    {
        public ShipAcceleratedMovingState(
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
            inputService.OnStopAcceleration += InputService_OnStopAcceleration;
        }

        public override void Exit()
        {
            inputService.OnStopAcceleration -= InputService_OnStopAcceleration;
        }

        protected override float GetCurrentSpeed()
        {
            float speed = presenter.Speed + settings.MovementAcceleration * Time.deltaTime;
            speed = Mathf.Clamp(speed, 0f, settings.MaxMovementSpeed);
            return speed;
        }

        private void InputService_OnStopAcceleration()
        {
            stateMachine.Enter<ShipInertMovingState>();
        }

        public class Factory : PlaceholderFactory<IShipStateMachine, ShipAcceleratedMovingState>
        {
        }
    }
}