using AsteroidsProject.Services;
using AsteroidsProject.UI.LoadingScreen;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingScreenPresenterFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingScreenService>().AsSingle();
        }
    }
}