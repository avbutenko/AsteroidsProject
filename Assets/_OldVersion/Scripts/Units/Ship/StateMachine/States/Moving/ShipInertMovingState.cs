using Assets.Scripts.Infrastructure.Services;
using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.Units.Ship;
using AsteroidsProject.Data.Units.Ship;
using UnityEngine;
using Zenject;
using AsteroidsProject.Infrastructure.StateMachine;

namespace AsteroidsProject.Units.Ship
{
    public class ShipInertMovingState : ShipBaseMovingState
    {
        private readonly ShipSettingsInstaller.ShipMovementSettings settings;
        public ShipInertMovingState(
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
                stateMachine.Enter<ShipNotMovingState>();
            }
        }

        public override void Exit()
        {
            inputService.OnAcceleration -= InputService_OnAcceleration;
        }

        protected override float GetCurrentSpeed()
        {
            return presenter.MovementSpeed - settings.MovementAcceleration * Time.deltaTime;
        }

        private void InputService_OnAcceleration()
        {
            stateMachine.Enter<ShipAcceleratedMovingState>();
        }

        public class Factory : PlaceholderFactory<ITickableStateMachine, ShipInertMovingState>
        {
        }
    }
}