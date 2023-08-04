using Assets.Scripts.Infrastructure.Services;
using AsteroidsProject.GameLogic;
using AsteroidsProject.Infrastructure.GameLogic;
using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Services;
using System;
using Zenject;

namespace AsteroidsProject.CompositionRoot.GameLogic
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameBootstraperFactory();
            BindCoroutineRunner();
            BindSceneLoader();
            BindLoadingCurtain();
            BindGameStateMachine();
            BindInputService();
            BindRotationService();
        }

        private void BindGameBootstraperFactory()
        {
            Container
                .BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.GameBootstraper);
        }

        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.CoroutineRunnerPath)
                .AsSingle();
        }

        private void BindSceneLoader()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }


        private void BindLoadingCurtain()
        {
            Container
                .Bind<ILoadingCurtain>()
                .To<LoadingCurtain>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.CurtainPath)
                .AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container
                .Bind<IGameStateMachine>()
                .FromSubContainerResolve()
                .ByInstaller<GameStateMachineInstaller>()
                .AsSingle();
        }

        private void BindInputService()
        {
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }

        private void BindRotationService()
        {
            Container.Bind<IRotationService>().To<RotationService>().AsSingle();
        }
    }
}
