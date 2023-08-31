using AsteroidsProject.EngineRelated.Services;
using AsteroidsProject.Infrastructure.Services;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle().NonLazy();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<ITimeService>().To<TimeService>().AsSingle();
            Container.Bind<ILevelService>().To<LevelService>().AsSingle();
            Container.Bind<ITeleportationService>().To<TeleportationService>().AsSingle();
        }
    }
}