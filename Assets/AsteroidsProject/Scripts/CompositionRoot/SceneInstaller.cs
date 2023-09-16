using Zenject;
using Leopotam.EcsLite.UnityEditor;
using AsteroidsProject.Shared;
using AsteroidsProject.Services;
using AsteroidsProject.MonoBehaviours;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.GameLogic.Features.Input;
using AsteroidsProject.GameLogic.Features.Rotation;
using AsteroidsProject.GameLogic.Features.Acceleration;
using AsteroidsProject.GameLogic.Features.Deacceleration;
using AsteroidsProject.GameLogic.Features.SpawnPlayer;
using AsteroidsProject.GameLogic.Features.SpawnAsteroid;
using AsteroidsProject.GameLogic.Features.SpawnPrefab;
using AsteroidsProject.GameLogic.Features.Teleportation;
using AsteroidsProject.GameLogic.Features.UpdateGameplayObjectView;

namespace AsteroidsProject.CompositionRoot
{
    public class SceneInstaller : MonoInstaller
    {
        public SceneData SceneData;

        public override void InstallBindings()
        {
            Container.Bind<ILevelService>().To<LevelService>().AsSingle();
            Container.Bind<ISceneData>().FromInstance(SceneData).AsSingle();
            BindEcsSystems();
        }

        private void BindEcsSystems()
        {
            Container.Bind<IGameplayObjectViewFactory>().To<GameplayObjectViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPlayerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnAsteroidSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPrefabSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AccelerationMovementSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeaccelerationMovementSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<TeleportationCheckSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<TeleportationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpdateGameplayObjectViewRotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpdateGameplayObjectViewPositionSystem>().AsSingle();

#if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<EcsWorldDebugSystem>().AsSingle();
#endif
            Container.BindInterfacesTo<EcsStartup>().AsSingle();
        }
    }
}