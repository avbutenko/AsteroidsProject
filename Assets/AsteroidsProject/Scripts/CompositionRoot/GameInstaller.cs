using AsteroidsProject.Services;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TimeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameConfigProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<UIProvider>().AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<ComponentConverterService>()
                .FromSubContainerResolve()
                .ByInstaller<ComponentConverterServiceInstaller>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<ConfigLoader>().AsSingle();
        }
    }
}