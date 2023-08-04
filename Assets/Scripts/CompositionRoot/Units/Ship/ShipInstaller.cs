using AsteroidsProject.Infrastructure;
using AsteroidsProject.Infrastructure.Units.Ship;
using AsteroidsProject.Units.Ship;
using AsteroidsProject.Views;
using Zenject;

namespace AsteroidsProject.CompositionRoot.Units.Ship
{
    public class ShipInstaller : Installer<ShipInstaller>
    {
        [Inject]
        private readonly ITransformable creationParameters;

        public override void InstallBindings()
        {
            BindMVP();
            BindShipStateMachine();
        }

        private void BindMVP()
        {
            Container.Bind<IShipModel>().To<ShipModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShipView>().FromNewComponentOnRoot().AsSingle();
            Container.BindInstance(creationParameters).WhenInjectedInto<ShipPresenter>();
            Container.BindInterfacesAndSelfTo<ShipPresenter>().AsSingle();
        }

        private void BindShipStateMachine()
        {
            Container
                .Bind<IShipStateMachine>()
                .FromSubContainerResolve()
                .ByInstaller<ShipStateMachineInstaller>()
                .AsSingle();
        }
    }
}