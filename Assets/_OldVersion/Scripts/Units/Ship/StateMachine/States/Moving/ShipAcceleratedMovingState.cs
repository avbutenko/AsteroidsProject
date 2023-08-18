using Assets.Scripts.Infrastructure.Services;
using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.Units.Ship;
using AsteroidsProject.Data.Units.Ship;
using UnityEngine;
using Zenject;
using AsteroidsProject.Infrastructure.StateMachine;

namespace AsteroidsProject.Units.Ship
{
    public class ShipAcceleratedMovingState : ShipBaseMovingState
    {
        private readonly ShipSettingsInstaller.ShipMovementSettings settings;
        public ShipAcceleratedMovingState(
            ITickableStateMachine stateMachine,
            IShipInputService inputService,
            IShipPresenter presenter,
            ShipSettingsInstaller.ShipMovementSettings settings,
            ITeleportationService teleportationService)
            : base(stateMachine, inputService, presenter, teleportationService)
        {
            this.settings = settings;
        }

        public override void Enter()
        {
            inputService.OnStopAcceleration += InputService_OnStopAcceleration;
            presenter.MovementDirection = presenter.Rotation * Vector3.up;
        }

        public override void Exit()
        {
            inputService.OnStopAcceleration -= InputService_OnStopAcceleration;
        }

        protected override float GetCurrentSpeed()
        {
            float speed = presenter.MovementSpeed + settings.MovementAcceleration * Time.deltaTime;
            speed = Mathf.Clamp(speed, 0f, settings.MaxMovementSpeed);
            return speed;
        }

        private void InputService_OnStopAcceleration()
        {
            stateMachine.Enter<ShipInertMovingState>();
        }

        public class Factory : PlaceholderFactory<ITickableStateMachine, ShipAcceleratedMovingState>
        {
        }
    }
}