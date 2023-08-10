using AsteroidsProject.Infrastructure.StateMachine;
using AsteroidsProject.Units.Ship;
using AsteroidsProject.Units.Ship.StateMachine;
using Zenject;

namespace AsteroidsProject.CompositionRoot.Units.Ship
{
    public class ShipRotatingStateMachineInstaller : Installer<ShipRotatingStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<ITickableStateMachine, ShipNotRotatingState, ShipNotRotatingState.Factory>();
            Container.BindFactory<ITickableStateMachine, ShipRotatingState, ShipRotatingState.Factory>();
            Container.BindInterfacesAndSelfTo<ShipRotatingStateMachine>().AsSingle();
        }
    }
}