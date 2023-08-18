using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.StateMachine;
using Zenject;

namespace AsteroidsProject.Units.Ship
{
    public class ShipNotRotatingState : ITickableState
    {
        private readonly ITickableStateMachine stateMachine;
        private readonly IShipInputService inputService;

        public ShipNotRotatingState(ITickableStateMachine stateMachine, IShipInputService inputService)
        {
            this.stateMachine = stateMachine;
            this.inputService = inputService;
        }

        public void Enter()
        {
            inputService.OnRotation += InputService_OnRotation;
        }

        public void Tick()
        {
        }

        public void Exit()
        {
            inputService.OnRotation -= InputService_OnRotation;
        }

        private void InputService_OnRotation()
        {
            stateMachine.Enter<ShipRotatingState>();
        }

        public class Factory : PlaceholderFactory<ITickableStateMachine, ShipNotRotatingState>
        {
        }
    }
}