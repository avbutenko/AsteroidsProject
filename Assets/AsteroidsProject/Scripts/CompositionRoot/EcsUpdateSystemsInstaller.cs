using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.GameLogic.Features.AccelerationMovement;
using AsteroidsProject.GameLogic.Features.AlignVelocityWithRotation;
using AsteroidsProject.GameLogic.Features.AmmoRefill;
using AsteroidsProject.GameLogic.Features.BasicMovement;
using AsteroidsProject.GameLogic.Features.BulletGun;
using AsteroidsProject.GameLogic.Features.CoolDown;
using AsteroidsProject.GameLogic.Features.DeaccelerationMovement;
using AsteroidsProject.GameLogic.Features.Destroy;
using AsteroidsProject.GameLogic.Features.Input;
using AsteroidsProject.GameLogic.Features.LaserGun;
using AsteroidsProject.GameLogic.Features.Lifetime;
using AsteroidsProject.GameLogic.Features.RandomizeVelocity;
using AsteroidsProject.GameLogic.Features.RandomizePosition;
using AsteroidsProject.GameLogic.Features.RandomizeRotationDirection;
using AsteroidsProject.GameLogic.Features.RandomizeRotationSpeed;
using AsteroidsProject.GameLogic.Features.Rotation;
using AsteroidsProject.GameLogic.Features.SpawnAsteroid;
using AsteroidsProject.GameLogic.Features.SpawnBullet;
using AsteroidsProject.GameLogic.Features.SpawnPlayer;
using AsteroidsProject.GameLogic.Features.Teleportation;
using AsteroidsProject.GameLogic.Features.UpdateGameObjectView;
using Leopotam.EcsLite.UnityEditor;
using Zenject;
using AsteroidsProject.GameLogic.Features.PermanentRandomRotation;
using AsteroidsProject.GameLogic.Features.Spawn.PrimaryWeapon;
using AsteroidsProject.GameLogic.Features.Spawn.SecondaryWeapon;

namespace AsteroidsProject.CompositionRoot
{
    public class EcsUpdateSystemsInstaller : Installer<EcsUpdateSystemsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SpawnAsteroidTimerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnAsteroidSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPlayerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPrimaryWeaponSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnSecondaryWeaponSystem>().AsSingle();

            Container.BindInterfacesAndSelfTo<RandomizePositionSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizeVelocitySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizePermanentRotationDirectionSysytem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizeRotationSpeedSystem>().AsSingle();

            Container.BindInterfacesAndSelfTo<SpawnPrefabSystem>().AsSingle();



            BindLifecycleSystems();
            BindSpawnSystems();
            BindPlayerInputSystems();
            BindMovementSystems();
            BindWeaponSystems();
            BindAmmoSystems();
            BindUpdateGameObjectViewSystems();

#if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<EcsWorldDebugSystem>().AsSingle();
#endif

            Container.BindInterfacesAndSelfTo<EcsUpdateSystemsRunner>().AsSingle();
        }

        private void BindLifecycleSystems()
        {
            Container.BindInterfacesAndSelfTo<LifetimeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DestroySystem>().AsSingle();
        }

        private void BindSpawnSystems()
        {
            Container.BindInterfacesAndSelfTo<SpawnBulletSystem>().AsSingle();
        }

        private void BindPlayerInputSystems()
        {
            Container.BindInterfacesAndSelfTo<EcsDeleteHereSystem<AttackRequest>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputSystem>().AsSingle();
        }

        private void BindMovementSystems()
        {

            Container.BindInterfacesAndSelfTo<AlignVelocityWithRotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PermanentRotationSystem>().AsSingle();
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