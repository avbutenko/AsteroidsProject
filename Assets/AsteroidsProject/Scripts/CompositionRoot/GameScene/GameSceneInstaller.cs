using Zenject;
using AsteroidsProject.Shared;
using AsteroidsProject.Services;
using AsteroidsProject.MonoBehaviours;
using AsteroidsProject.GameLogic.EntryPoint.GameScene;
using AsteroidsProject.UI.MainMenuScreen;
using AsteroidsProject.UI.GameOverScreen;

namespace AsteroidsProject.CompositionRoot
{
    public class GameSceneInstaller : MonoInstaller
    {
        public GameSceneData SceneData;

        public override void InstallBindings()
        {
            Container.Bind<IGameSceneData>().FromInstance(SceneData).AsSingle();
            Container.Bind<ILevelService>().To<LevelService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
            Container.Bind<ITimeService>().To<TimeService>().AsSingle();
            Container.Bind<IGameObjectPool>().To<GameObjectPool>().AsSingle();
            Container.Bind<IActiveGOMappingService>().To<ActiveGOMappingService>().AsSingle();
            Container.Bind<IEntityGameObjectFactory>().To<EntityGameObjectFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<UIScreenViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuScreenPresenterFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverScreenPresenterFactory>().AsSingle();
            Container.Bind<IUIService>().To<UIService>().AsSingle();

            Container
                .BindInterfacesAndSelfTo<ComponentConverterService>()
                .FromSubContainerResolve()
                .ByInstaller<ComponentConverterServiceInstaller>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<EcsUpdateSystemsProvider>()
                .FromSubContainerResolve()
                .ByInstaller<EcsUpdateSystemsInstaller>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<EcsFixedUpdateSystemsProvider>()
                .FromSubContainerResolve()
                .ByInstaller<EcsFixedUpdateSystemsInstaller>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<ConfigProvider>().AsSingle();
            Container.Bind<IEcsSystemsRunner>().To<EcsSystemsRunner>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameSceneEntryPoint>().AsSingle();
        }
    }
}