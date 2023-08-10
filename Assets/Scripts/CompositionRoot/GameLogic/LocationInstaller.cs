using Assets.Scripts.Infrastructure.Services;
using AsteroidsProject.CompositionRoot.Units.Ship;
using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.Units.Ship;
using AsteroidsProject.Services;
using AsteroidsProject.Data.Units.Ship;
using AsteroidsProject.Units.Ship;
using Zenject;


namespace AsteroidsProject.CompositionRoot.GameLogic
{
    public class LocationInstaller : MonoInstaller
    {
        [Inject]
        private readonly ShipSettingsInstaller.ShipPrefabSettings shipSettings;
        public override void InstallBindings()
        {
            BindLevelService();
            BindTeleportationService();
            BindShip();
            BindAsteroids();
            BindUfos();
        }

        private void BindUfos()
        {
            //TODO
        }

        private void BindAsteroids()
        {
            //TODO
        }

        private void BindLevelService()
        {
            Container.Bind<ILevelService>().To<LevelService>().AsSingle().NonLazy();
        }

        private void BindTeleportationService()
        {
            Container.Bind<ITeleportationService>().To<TeleportationService>().AsSingle();
        }

        private void BindShip()
        {
            Container.BindFactoryCustomInterface<IShipInitParams, IShipPresenter, ShipPresenter.Factory, IShipPresenterFactory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<ShipInstaller>(shipSettings.Prefab);
        }
    }
}