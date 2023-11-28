using AsteroidsProject.GameLogic.SceneRunners.MainMenuScene;
using AsteroidsProject.MonoBehaviours;
using AsteroidsProject.Services;
using AsteroidsProject.Shared;
using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    public MainMenuSceneData SceneData;
    public override void InstallBindings()
    {
        Container.Bind<IMainMenuSceneData>().FromInstance(SceneData).AsSingle();
        Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
        Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<MainMenuSceneRunner>().AsSingle();
    }
}