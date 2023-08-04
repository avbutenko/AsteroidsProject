using AsteroidsProject.Infrastructure.Units.Ship;
using AsteroidsProject.Units.Ship;
using Zenject;

namespace AsteroidsProject.CompositionRoot.Units.Ship
{
    public class ShipStateMachineInstaller : Installer<ShipStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<IShipStateMachine, ShipIdleState, ShipIdleState.Factory>();
            Container.BindFactory<IShipStateMachine, ShipIdleRotatingState, ShipIdleRotatingState.Factory>();
            Container.BindFactory<IShipStateMachine, ShipAcceleratedMovingState, ShipAcceleratedMovingState.Factory>();
            Container.BindFactory<IShipStateMachine, ShipInertMovingState, ShipInertMovingState.Factory>();
            Container.BindInterfacesAndSelfTo<ShipStateMachine>().AsSingle();
        }
    }
}