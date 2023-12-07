﻿using Zenject;
using Leopotam.EcsLite.UnityEditor;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.GameLogic.Features.Spawn.Obstacles.Asteroid;
using AsteroidsProject.GameLogic.Features.Spawn.Obstacles.AsteroidFragment;
using AsteroidsProject.GameLogic.Features.Spawn.Units.Ufo;
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
using AsteroidsProject.GameLogic.Features.Movement.FollowTarget;
using AsteroidsProject.GameLogic.Features.Rotation.Permanent;
using AsteroidsProject.GameLogic.Features.Rotation.Basic;
using AsteroidsProject.GameLogic.Features.Weapons.BulletGun;
using AsteroidsProject.GameLogic.Features.Weapons.LaserGun;
using AsteroidsProject.GameLogic.Features.Events.OnOutOfLevel;
using AsteroidsProject.GameLogic.Features.Events.OnSpawn;
using AsteroidsProject.GameLogic.Features.Events.OnAttack;
using AsteroidsProject.GameLogic.Features.Events.OnDeath;
using AsteroidsProject.GameLogic.Features.UI.BroadcastGamePauseEventToUI;
using AsteroidsProject.GameLogic.Features.UI.BroadcastGameOverEventToUI;
using AsteroidsProject.GameLogic.Features.UI.BroadcastDataToPlayerShipStatsScreen;
using AsteroidsProject.GameLogic.Features.Teleportation;
using AsteroidsProject.GameLogic.Features.Lifetime;
using AsteroidsProject.GameLogic.Features.MaxVelocityMagnitude;
using AsteroidsProject.GameLogic.Features.UpdateGameObjectView;
using AsteroidsProject.GameLogic.Features.Damage;
using AsteroidsProject.GameLogic.Features.Score;
using AsteroidsProject.GameLogic.Features.AttackCoolDown;
using AsteroidsProject.GameLogic.Features.Ammo.AutoRefill;
using AsteroidsProject.GameLogic.Features.Ammo.Max;
using AsteroidsProject.GameLogic.Features.Ammo.ChangeAmount;
using AsteroidsProject.GameLogic.Features.InvalidOwner;
using AsteroidsProject.GameLogic.Features.InvalidGO;
using AsteroidsProject.Services;


namespace AsteroidsProject.CompositionRoot
{
    public class EcsUpdateSystemsInstaller : Installer<EcsUpdateSystemsInstaller>
    {
        public override void InstallBindings()
        {
            //Spawn Entities
            Container.BindInterfacesAndSelfTo<SpawnAsteroidSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnAsteroidFragmentSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPlayerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnUfoSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPrimaryWeaponSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnSecondaryWeaponSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnProjectileSystem>().AsSingle();

            //Initializing
            Container.BindInterfacesAndSelfTo<RandomizePositionSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizeVelocitySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizePermanentRotationDirectionSysytem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizeRotationSpeedSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SetFollowTargetSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnSpawnSystem>().AsSingle();

            //Spawn Prefabs
            Container.BindInterfacesAndSelfTo<SpawnEntityViewSystem>().AsSingle();

            //Player Input
            Container.BindInterfacesAndSelfTo<EcsDeleteHereSystem<CAttackRequest>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputSystem>().AsSingle();

            //Pause
            Container.BindInterfacesAndSelfTo<BroadcastGamePauseEventToUISystem>().AsSingle();

            //Weapons
            Container.BindInterfacesAndSelfTo<BulletGunAttackSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserGunAttackSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnAttackSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AttackCoolDownSystem>().AsSingle();

            //Ammo
            Container.BindInterfacesAndSelfTo<ChangeAmmoAmountSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AmmoAutoRefillSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AmmoAutoRefillCoolDownSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AmmoMaxSystem>().AsSingle();

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
            Container.BindInterfacesAndSelfTo<AlignRotationWithTargetSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AlignVelocityWithTargetSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<OutOfLevelCheckSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnOutOfLevelSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<TeleportationSystem>().AsSingle();

            //Lifecycle
            Container.BindInterfacesAndSelfTo<LifetimeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DamageSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnDeathSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<InvalidOwnerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<InvalidGOSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<BroadcastGameOverEventToUISystem>().AsSingle();

            //UpdateView
            Container.BindInterfacesAndSelfTo<UpdateGameObjectViewRotationSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpdateGameObjectViewPositionSystem>().AsSingle();

            //UI
            Container.BindInterfacesAndSelfTo<BroadcastDataToPlayerShipStatsScreenSystem>().AsSingle();

#if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<EcsSystemsDebugSystem>().AsSingle().WithArguments(new object[] { "Update Systems" });
#endif
            Container.BindInterfacesAndSelfTo<EcsUpdateSystemsProvider>().AsSingle();
        }
    }
}