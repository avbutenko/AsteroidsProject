using AsteroidsProject.Infrastructure.StateMachine;
using AsteroidsProject.Infrastructure.Units.Ship;
using Zenject;

namespace AsteroidsProject.Units.Ship.StateMachine
{
    public class ShipRotatingStateMachine : BaseTickableStateMachine, IShipRotatingStateMachine
    {
        [Inject]
        public void Construct(
            ShipNotRotatingState.Factory shipNotRotatingStateFactory,
            ShipRotatingState.Factory shipRotatingStateFactory)
        {
            RegisterState(shipNotRotatingStateFactory.Create(this));
            RegisterState(shipRotatingStateFactory.Create(this));
        }
    }
}