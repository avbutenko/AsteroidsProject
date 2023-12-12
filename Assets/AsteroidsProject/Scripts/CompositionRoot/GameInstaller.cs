using AsteroidsProject.Services;
using AsteroidsProject.UI.LoadingScreen;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameConfigProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingScreenFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingScreenService>().AsSingle();

            Container
                .BindInterfacesAndSelfTo<ComponentConverterService>()
                .FromSubContainerResolve()
                .ByInstaller<ComponentConverterServiceInstaller>()
                .AsSingle();
        }
    }
}