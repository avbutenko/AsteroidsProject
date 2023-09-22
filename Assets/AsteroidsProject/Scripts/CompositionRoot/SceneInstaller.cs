using Zenject;
using Leopotam.EcsLite.UnityEditor;
using AsteroidsProject.Shared;
using AsteroidsProject.Services;
using AsteroidsProject.MonoBehaviours;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.GameLogic.Features.Input;
using AsteroidsProject.GameLogic.Features.Rotation;
using AsteroidsProject.GameLogic.Features.SpawnPlayer;
using AsteroidsProject.GameLogic.Features.SpawnAsteroid;
using AsteroidsProject.GameLogic.Features.SpawnPrefab;
using AsteroidsProject.GameLogic.Features.Teleportation;
using AsteroidsProject.GameLogic.Features.UpdateGameObjectView;
using AsteroidsProject.GameLogic.Features.AccelerationMovement;
using AsteroidsProject.GameLogic.Features.BasicMovement;
using AsteroidsProject.GameLogic.Features.RandomizedVelocity;
using AsteroidsProject.GameLogic.Features.RandomizeRotationDirection;
using AsteroidsProject.GameLogic.Features.RandomizeRotationSpeed;
using AsteroidsProject.GameLogic.Features.PrimaryWeapon;
using AsteroidsProject.GameLogic.Features.BulletGun;
using AsteroidsProject.GameLogic.Features.SecondaryWeapon;
using AsteroidsProject.GameLogic.Features.LaserGun;

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
            Container.Bind<IGameObjectFactory>().To<GameplayObjectViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPlayerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPrimaryWeaponSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnSecondaryWeaponSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnAsteroidSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPrefabSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizeVelocitySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizeRotationDirectionSysytem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizeRotationSpeedSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<EcsDeleteHereSystem<AccelerationRequest>>().AsSingle();
            Container.BindInterfacesAndSelfTo<EcsDeleteHereSystem<PrimaryWeaponAttackRequest>>().AsSingle();
            Container.BindInterfacesAndSelfTo<EcsDeleteHereSystem<SecondaryWeaponAttackRequest>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AccelerationVectorSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AccelerationVelocitySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeaccelerationVectorSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeaccelerationVelocitySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AccelerationPositionSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<BasicMovementSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<TeleportationCheckSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<TeleportationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PrimaryWeaponAttackSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SecondaryWeaponAttackSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletGunAttackSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserGunAttackSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpdateGameObjectViewRotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpdateGameObjectViewPositionSystem>().AsSingle();

#if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<EcsWorldDebugSystem>().AsSingle();
#endif
            Container.BindInterfacesTo<EcsStartup>().AsSingle();
        }
    }
}