using AsteroidsProject.EngineRelated.Services;
using AsteroidsProject.GameLogic.Ecs;
using AsteroidsProject.GameLogic.Features.Movement;
using AsteroidsProject.GameLogic.Features.Rotation;
using AsteroidsProject.GameLogic.Features.Spawn;
using AsteroidsProject.Infrastructure.Services;
using Leopotam.EcsLite.UnityEditor;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameplayObjectViewFactory>().To<GameplayObjectViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerSpawnSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeleteHereSystem<RotateCommand>>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeleteHereSystem<AccelerationCommand>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputSystem>().AsSingle();


#if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<EcsWorldDebugSystem>().AsSingle();
#endif

            Container.BindInterfacesTo<EcsStartup>().AsSingle();
        }
    }
}