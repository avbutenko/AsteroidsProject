using Zenject;
using AsteroidsProject.Shared;
using AsteroidsProject.Services;
using AsteroidsProject.MonoBehaviours;
using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.CompositionRoot
{
    public class SceneInstaller : MonoInstaller
    {
        public SceneData SceneData;

        public override void InstallBindings()
        {
            Container.BindInstance(new EcsWorld())
                     .AsSingle();

            Container.Bind<ILevelService>()
                     .To<LevelService>()
                     .AsSingle();

            Container.Bind<ISceneData>()
                     .FromInstance(SceneData)
                     .AsSingle();

            Container
                .BindInterfacesAndSelfTo<EcsUpdateSystemsRunner>()
                .FromSubContainerResolve()
                .ByInstaller<EcsUpdateSystemsInstaller>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<EcsFixedUpdateSystemsRunner>()
                .FromSubContainerResolve()
                .ByInstaller<EcsFixedUpdateSystemsInstaller>()
                .AsSingle();
        }
    }
}