using Zenject;
using Leopotam.EcsLite.UnityEditor;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.GameLogic.Features.Spawn.Obstacles.Asteroid;
using AsteroidsProject.GameLogic.Features.Spawn.Projectiles;
using AsteroidsProject.GameLogic.Features.Spawn.Player;
using AsteroidsProject.GameLogic.Features.Spawn.EntityView;
using AsteroidsProject.GameLogic.Features.Spawn.Weapons;
using AsteroidsProject.GameLogic.Features.Randomization.PermanentRotationDirection;
using AsteroidsProject.GameLogic.Features.Randomization.Position;
using AsteroidsProject.GameLogic.Features.Randomization.RotationSpeed;
using AsteroidsProject.GameLogic.Features.Randomization.Velocity;
using AsteroidsProject.GameLogic.Features.PlayerInput;
using AsteroidsProject.GameLogic.Features.Movement.Acceleration;
using AsteroidsProject.GameLogic.Features.Movement.Deacceleration;
using AsteroidsProject.GameLogic.Features.Movement.Basic;
using AsteroidsProject.GameLogic.Features.Rotation.Permanent;
using AsteroidsProject.GameLogic.Features.Rotation.Basic;
using AsteroidsProject.GameLogic.Features.Weapons.BulletGun;
using AsteroidsProject.GameLogic.Features.Weapons.LaserGun;
using AsteroidsProject.GameLogic.Features.Weapons.AmmoAutoRefill;
using AsteroidsProject.GameLogic.Features.CoolDown;
using AsteroidsProject.GameLogic.Features.Events.OnOutOfLevel;
using AsteroidsProject.GameLogic.Features.Events.OnSpawn;
using AsteroidsProject.GameLogic.Features.Events.OnAttack;
using AsteroidsProject.GameLogic.Features.Teleportation;
using AsteroidsProject.GameLogic.Features.Lifetime;
using AsteroidsProject.GameLogic.Features.MaxVelocityMagnitude;
using AsteroidsProject.GameLogic.Features.UpdateGameObjectView;
using AsteroidsProject.GameLogic.Features.Damage;
using AsteroidsProject.GameLogic.Features.Score;
using AsteroidsProject.GameLogic.Features.Events.OnDeath;

namespace AsteroidsProject.CompositionRoot
{
    public class EcsUpdateSystemsInstaller : Installer<EcsUpdateSystemsInstaller>
    {
        public override void InstallBindings()
        {
            //Spawn Entities
            Container.BindInterfacesAndSelfTo<SpawnAsteroidSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPlayerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPrimaryWeaponSystem>().AsSingle();
            //Container.BindInterfacesAndSelfTo<SpawnSecondaryWeaponSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnProjectileSystem>().AsSingle();

            //Initializing
            Container.BindInterfacesAndSelfTo<RandomizePositionSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizeVelocitySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizePermanentRotationDirectionSysytem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizeRotationSpeedSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnSpawnSystem>().AsSingle();

            //Spawn Prefabs
            Container.BindInterfacesAndSelfTo<SpawnEntityViewSystem>().AsSingle();

            //Player Input
            Container.BindInterfacesAndSelfTo<EcsDeleteHereSystem<CAttackRequest>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputSystem>().AsSingle();

            //Weapons
            Container.BindInterfacesAndSelfTo<BulletGunAttackSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserGunAttackSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnAttackSystem>().AsSingle();

            //Movement
            Container.BindInterfacesAndSelfTo<MaxVelocityMagnitudeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PermanentRotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<BasicRotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AccelerationVectorSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AccelerationVelocitySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AccelerationPositionSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeaccelerationVectorSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeaccelerationVelocitySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeaccelerationPositionSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<BasicMovementSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<OutOfLevelCheckSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnOutOfLevelSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<TeleportationSystem>().AsSingle();

            //Lifecycle
            Container.BindInterfacesAndSelfTo<CoolDownSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AmmoAutoRefillSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<LifetimeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DamageSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnDeathSystem>().AsSingle();

            //UpdateView
            Container.BindInterfacesAndSelfTo<UpdateGameObjectViewRotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpdateGameObjectViewPositionSystem>().AsSingle();

#if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<EcsWorldDebugSystem>().AsSingle();
#endif
            Container.BindInterfacesAndSelfTo<EcsUpdateSystemsRunner>().AsSingle();
        }
    }
}