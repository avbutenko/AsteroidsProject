using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.StateMachine;
using Zenject;

namespace AsteroidsProject.Units.Ship
{
    public class ShipNotMovingState : ITickableState
    {
        private readonly ITickableStateMachine stateMachine;
        private readonly IShipInputService inputService;

        public ShipNotMovingState(ITickableStateMachine stateMachine, IShipInputService inputService)
        {
            this.stateMachine = stateMachine;
            this.inputService = inputService;
        }

        public void Enter()
        {
            inputService.OnAcceleration += InputService_OnAcceleration;
        }

        public void Tick()
        {
        }

        public void Exit()
        {
            inputService.OnAcceleration -= InputService_OnAcceleration;
        }

        private void InputService_OnAcceleration()
        {
            stateMachine.Enter<ShipAcceleratedMovingState>();
        }

        public class Factory : PlaceholderFactory<ITickableStateMachine, ShipNotMovingState>
        {
        }
    }
}
