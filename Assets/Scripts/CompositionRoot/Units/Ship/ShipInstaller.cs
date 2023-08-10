using AsteroidsProject.Infrastructure.Units.Ship;
using AsteroidsProject.Units.Ship;
using AsteroidsProject.Views;
using Zenject;

namespace AsteroidsProject.CompositionRoot.Units.Ship
{
    public class ShipInstaller : Installer<ShipInstaller>
    {
        [Inject]
        private readonly IShipInitParams creationParameters;

        public override void InstallBindings()
        {
            BindMVP();
            BindShipStateMachines();
        }

        private void BindMVP()
        {
            Container.Bind<IShipModel>().To<ShipModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShipView>().FromNewComponentOnRoot().AsSingle();
            Container.BindInstance(creationParameters).WhenInjectedInto<ShipPresenter>();
            Container.BindInterfacesAndSelfTo<ShipPresenter>().AsSingle();
        }

        private void BindShipStateMachines()
        {
            Container
                .Bind<IShipRotatingStateMachine>()
                .FromSubContainerResolve()
                .ByInstaller<ShipRotatingStateMachineInstaller>()
                .AsSingle();

            Container
                .Bind<IShipMovingStateMachine>()
                .FromSubContainerResolve()
                .ByInstaller<ShipMovingStateMachineInstaller>()
                .AsSingle();
        }
    }
}