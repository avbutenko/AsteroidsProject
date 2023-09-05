using AsteroidsProject.EngineRelated.Services;
using AsteroidsProject.GameLogic.Ecs;
using AsteroidsProject.GameLogic.Features.Movement;
using AsteroidsProject.GameLogic.Features.Rotation;
using AsteroidsProject.GameLogic.Features.Scale;
using AsteroidsProject.GameLogic.Features.Spawn;
using AsteroidsProject.GameLogic.Features.Teleportation;
using AsteroidsProject.Infrastructure.Services;
using Leopotam.EcsLite.UnityEditor;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILevelService>().To<LevelService>().AsSingle();
            Container.Bind<ITeleportationService>().To<TeleportationService>().AsSingle();
            BindEcsSystems();
        }

        private void BindEcsSystems()
        {
            Container.Bind<IGameplayObjectViewFactory>().To<GameplayObjectViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerSpawnSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeleteHereSystem<RotationRequest>>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeleteHereSystem<ForwardAccelerationRequest>>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeleteHereSystem<DeaccelerationRequest>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ForwardAccelerationMovementSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeaccelerationMovementSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<TeleportationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpdateViewScaleSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpdateViewRotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpdateViewPositionSystem>().AsSingle();

#if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<EcsWorldDebugSystem>().AsSingle();
#endif
            Container.BindInterfacesTo<EcsStartup>().AsSingle();
        }
    }
}