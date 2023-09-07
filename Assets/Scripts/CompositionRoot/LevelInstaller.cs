using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.EngineRelated.Services;
using AsteroidsProject.Features.Core;
using AsteroidsProject.Features.Rotation;
using AsteroidsProject.Features.Input;
using AsteroidsProject.Features.DeaccelerationMovement;
using AsteroidsProject.Features.ForwardAccelerationMovement;
using AsteroidsProject.Features.Spawn;
using AsteroidsProject.Features.Teleportation;
using AsteroidsProject.Ecs;
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
            Container.BindInterfacesAndSelfTo<EcsDeleteHereSystem<RotationRequest>>().AsSingle();
            Container.BindInterfacesAndSelfTo<EcsDeleteHereSystem<ForwardAccelerationRequest>>().AsSingle();
            Container.BindInterfacesAndSelfTo<EcsDeleteHereSystem<DeaccelerationRequest>>().AsSingle();
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