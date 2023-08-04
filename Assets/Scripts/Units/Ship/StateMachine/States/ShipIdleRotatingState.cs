using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.StateMachine;
using AsteroidsProject.Infrastructure.Units.Ship;
using AsteroidsProject.Settings.Units.Ship;
using Zenject;

namespace AsteroidsProject.Units.Ship
{
    public class ShipIdleRotatingState : ITickableState
    {
        private readonly IShipStateMachine stateMachine;
        private readonly IShipInputService inputService;
        private readonly IShipPresenter presenter;
        private readonly ShipSettingsInstaller.ShipMovementSettings settings;
        private readonly IRotationService rotationService;

        public ShipIdleRotatingState(
            IShipStateMachine stateMachine,
            IShipInputService inputService,
            IShipPresenter presenter,
            ShipSettingsInstaller.ShipMovementSettings settings,
            IRotationService rotationService)
        {
            this.stateMachine = stateMachine;
            this.inputService = inputService;
            this.presenter = presenter;
            this.settings = settings;
            this.rotationService = rotationService;
        }

        public void Enter()
        {
            inputService.OnAcceleration += InputService_OnAcceleration;
            inputService.OnStopRotation += InputService_OnStopRotation;
        }

        public void Tick()
        {
            presenter.Rotation = rotationService.GetNewRotation(presenter.Rotation, inputService.RotationDirection, settings.RotationSpeed);
        }

        public void Exit()
        {
            inputService.OnAcceleration -= InputService_OnAcceleration;
            inputService.OnStopRotation -= InputService_OnStopRotation;
        }

        private void InputService_OnAcceleration()
        {
            stateMachine.Enter<ShipAcceleratedMovingState>();
        }

        private void InputService_OnStopRotation()
        {
            stateMachine.Enter<ShipIdleState>();
        }

        public class Factory : PlaceholderFactory<IShipStateMachine, ShipIdleRotatingState>
        {
        }
    }
}