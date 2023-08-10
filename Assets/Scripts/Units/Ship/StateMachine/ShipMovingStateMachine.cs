using AsteroidsProject.Infrastructure.StateMachine;
using AsteroidsProject.Infrastructure.Units.Ship;
using Zenject;

namespace AsteroidsProject.Units.Ship
{
    public class ShipMovingStateMachine : BaseTickableStateMachine, IShipMovingStateMachine
    {
        [Inject]
        public void Construct(
            ShipNotMovingState.Factory shipIdleStateFactory,
            ShipAcceleratedMovingState.Factory shipAcceleratedMovingStateFactory,
            ShipInertMovingState.Factory shipInertMovingStateFactory)
        {
            RegisterState(shipIdleStateFactory.Create(this));
            RegisterState(shipAcceleratedMovingStateFactory.Create(this));
            RegisterState(shipInertMovingStateFactory.Create(this));
        }
    }
}

