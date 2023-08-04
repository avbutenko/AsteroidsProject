using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.StateMachine;
using AsteroidsProject.Infrastructure.Units.Ship;
using Zenject;

namespace AsteroidsProject.Units.Ship
{
    public class ShipIdleState : ITickableState
    {
        private readonly IShipStateMachine stateMachine;
        private readonly IShipInputService inputService;

        public ShipIdleState(IShipStateMachine stateMachine, IShipInputService inputService)
        {
            this.stateMachine = stateMachine;
            this.inputService = inputService;
        }

        public void Enter()
        {
            inputService.OnAcceleration += InputService_OnAcceleration;
            inputService.OnRotation += InputService_OnRotation;
        }

        public void Tick()
        {
        }

        public void Exit()
        {
            inputService.OnAcceleration -= InputService_OnAcceleration;
            inputService.OnRotation -= InputService_OnRotation;
        }

        private void InputService_OnAcceleration()
        {
            stateMachine.Enter<ShipAcceleratedMovingState>();
        }

        private void InputService_OnRotation()
        {
            stateMachine.Enter<ShipIdleRotatingState>();
        }

        public class Factory : PlaceholderFactory<IShipStateMachine, ShipIdleState>
        {
        }
    }
}
