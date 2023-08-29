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
            Container.BindInterfacesAndSelfTo<DeleteHereSystem<RotateRequest>>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeleteHereSystem<AccelerationRequest>>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeleteHereSystem<InertionRequest>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AccelerationModifierSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<InertionModifierSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<UniformAccelerationMovementSystem>().AsSingle();


#if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<EcsWorldDebugSystem>().AsSingle();
#endif

            Container.BindInterfacesTo<EcsStartup>().AsSingle();
        }
    }
}