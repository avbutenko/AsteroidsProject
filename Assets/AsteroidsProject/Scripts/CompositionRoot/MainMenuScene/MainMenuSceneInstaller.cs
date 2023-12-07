using AsteroidsProject.GameLogic.EntryPoint.MainMenuScene;
using AsteroidsProject.Services;
using AsteroidsProject.Shared;
using AsteroidsProject.UI.MainMenuScreen;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuScreenFactory>().AsSingle();
            Container.Bind<IUIService>().To<UIService>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuSceneEntryPoint>().AsSingle();
        }
    }
}