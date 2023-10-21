using AsteroidsProject.Services;
using AsteroidsProject.Shared;
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
            Container.Bind<IGameObjectPool>().To<GameObjectPool>().AsSingle();
            Container.Bind<IActiveGameObjectMapService>().To<ActiveGameObjectMapService>().AsSingle();
            Container.Bind<IGameObjectFactory>().To<GameObjectFactory>().AsSingle();

            Container
                .BindInterfacesAndSelfTo<ComponentConverterService>()
                .FromSubContainerResolve()
                .ByInstaller<ComponentConverterServiceInstaller>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<ConfigProvider>().AsSingle();
        }
    }
}