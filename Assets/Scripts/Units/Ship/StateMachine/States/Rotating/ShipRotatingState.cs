using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.StateMachine;
using AsteroidsProject.Infrastructure.Units.Ship;
using AsteroidsProject.Data.Units.Ship;
using Zenject;

namespace AsteroidsProject.Units.Ship
{
    public class ShipRotatingState : ITickableState
    {
        private readonly ITickableStateMachine stateMachine;
        private readonly IShipInputService inputService;
        private readonly IShipPresenter presenter;
        private readonly ShipSettingsInstaller.ShipMovementSettings settings;
        private readonly IRotationService rotationService;

        public ShipRotatingState(
            ITickableStateMachine stateMachine,
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
            inputService.OnStopRotation += InputService_OnStopRotation;
        }

        public void Tick()
        {
            presenter.Rotation = rotationService.GetNewRotation(presenter.Rotation, inputService.RotationDirection, settings.RotationSpeed);
        }

        public void Exit()
        {
            inputService.OnStopRotation -= InputService_OnStopRotation;
        }

        private void InputService_OnStopRotation()
        {
            stateMachine.Enter<ShipNotRotatingState>();
        }

        public class Factory : PlaceholderFactory<ITickableStateMachine, ShipRotatingState>
        {
        }
    }
}