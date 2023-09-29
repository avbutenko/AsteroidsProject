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
using AsteroidsProject.GameLogic.Features.DeaccelerationMovement;
using AsteroidsProject.GameLogic.Features.BasicMovement;
using AsteroidsProject.GameLogic.Features.RandomizedVelocity;
using AsteroidsProject.GameLogic.Features.RandomizeRotationDirection;
using AsteroidsProject.GameLogic.Features.RandomizeRotationSpeed;
using AsteroidsProject.GameLogic.Features.SpawnWeapon;
using AsteroidsProject.GameLogic.Features.BulletGun;
using AsteroidsProject.GameLogic.Features.LaserGun;
using AsteroidsProject.GameLogic.Features.AlignVelocityWithRotation;
using AsteroidsProject.GameLogic.Features.CoolDown;
using AsteroidsProject.GameLogic.Features.AmmoRefill;

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
            Container.BindInterfacesTo<EcsStartup>().AsSingle();
        }

        private void BindEcsSystems()
        {
            BindSpawnSystems();
            BindPlayerInputSystems();
            BindMovementSystems();
            BindWeaponSystems();
            BindAmmoSystems();
            BindUpdateGameObjectViewSystems();

#if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<EcsWorldDebugSystem>().AsSingle();
#endif
        }

        private void BindSpawnSystems()
        {
            Container.BindInterfacesAndSelfTo<SpawnPlayerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnWeaponSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnAsteroidSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPrefabSystem>().AsSingle();
        }

        private void BindPlayerInputSystems()
        {
            Container.BindInterfacesAndSelfTo<EcsDeleteHereSystem<AttackRequest>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputSystem>().AsSingle();
        }

        private void BindMovementSystems()
        {
            Container.BindInterfacesAndSelfTo<RandomizeVelocitySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizeRotationDirectionSysytem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizeRotationSpeedSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AlignVelocityWithRotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AccelerationVectorSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AccelerationVelocitySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AccelerationPositionSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeaccelerationVectorSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeaccelerationVelocitySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeaccelerationPositionSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<BasicMovementSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<TeleportationCheckSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<TeleportationSystem>().AsSingle();
        }

        private void BindWeaponSystems()
        {
            Container.BindInterfacesAndSelfTo<BulletGunAttackSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserGunAttackSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<CoolDownSystem>().AsSingle();
        }

        private void BindAmmoSystems()
        {
            Container.BindInterfacesAndSelfTo<AmmoRefillSystem>().AsSingle();
        }

        private void BindUpdateGameObjectViewSystems()
        {
            Container.BindInterfacesAndSelfTo<UpdateGameObjectViewRotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpdateGameObjectViewPositionSystem>().AsSingle();
        }
    }
}