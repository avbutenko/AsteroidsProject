using Assets.Scripts.Infrastructure.Services;
using AsteroidsProject.CompositionRoot.Units.Ship;
using AsteroidsProject.Infrastructure;
using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.Units.Ship;
using AsteroidsProject.Services;
using AsteroidsProject.Settings.Units.Ship;
using AsteroidsProject.Units.Ship;
using Zenject;


namespace AsteroidsProject.CompositionRoot.GameLogic
{
    public class LocationInstaller : MonoInstaller
    {
        [Inject]
        private readonly ShipSettingsInstaller.ShipGeneralSettings shipSettings;
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
            Container.BindFactoryCustomInterface<ITransformable, IShipPresenter, ShipPresenter.Factory, IShipPresenterFactory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<ShipInstaller>(shipSettings.Prefab);
        }
    }
}