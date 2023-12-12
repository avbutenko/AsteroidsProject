using Zenject;
using AsteroidsProject.Shared;
using AsteroidsProject.Services;
using AsteroidsProject.MonoBehaviours;
using AsteroidsProject.GameLogic.EntryPoint.GameScene;
using AsteroidsProject.UI.GameOverScreen;
using AsteroidsProject.UI.GamePauseScreen;
using AsteroidsProject.UI.PlayerShipStatsScreen;
using AsteroidsProject.UI.PlayerSecondaryWeaponScreen;

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
            Container.BindInterfacesAndSelfTo<GameObjectPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<ActiveGOMappingService>().AsSingle();
            Container.Bind<IEntityGameObjectFactory>().To<EntityGameObjectFactory>().AsSingle();

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

            Container.BindInterfacesAndSelfTo<ConfigLoader>().AsSingle();
            Container.Bind(typeof(IEcsSystemsRunner), typeof(IRestartable)).To<EcsSystemsRunner>().AsSingle();
            Container.BindInterfacesAndSelfTo<RestartService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerShipStatsScreenFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerSecondaryWeaponScreenFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<GamePauseScreenFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverScreenFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<UIService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameSceneEntryPoint>().AsSingle();
        }
    }
}