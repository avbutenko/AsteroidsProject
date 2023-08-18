using AsteroidsProject.Infrastructure.StateMachine;
using AsteroidsProject.Units.Ship;
using Zenject;

namespace AsteroidsProject.CompositionRoot.Units.Ship
{
    public class ShipMovingStateMachineInstaller : Installer<ShipMovingStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<ITickableStateMachine, ShipNotMovingState, ShipNotMovingState.Factory>();
            Container.BindFactory<ITickableStateMachine, ShipAcceleratedMovingState, ShipAcceleratedMovingState.Factory>();
            Container.BindFactory<ITickableStateMachine, ShipInertMovingState, ShipInertMovingState.Factory>();
            Container.BindInterfacesAndSelfTo<ShipMovingStateMachine>().AsSingle();
        }
    }
}