using AsteroidsProject.GameLogic.Features.Ammo;
using AsteroidsProject.GameLogic.Features.Events;
using AsteroidsProject.GameLogic.Features.Lifetime;
using AsteroidsProject.GameLogic.Features.Movement;
using AsteroidsProject.GameLogic.Features.PlayerInput;
using AsteroidsProject.GameLogic.Features.Score;
using AsteroidsProject.GameLogic.Features.Spawn;
using AsteroidsProject.GameLogic.Features.UI;
using AsteroidsProject.GameLogic.Features.Weapons;
using AsteroidsProject.Services;
using System;
using System.Collections.Generic;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class GameSceneSystemsInstaller : Installer<GameSceneSystemsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EcsSystemListProvider>()
                     .AsSingle()
                     .WithArguments(SetUpdateSystems(), SetFixedUpdateSystems(), SetGUISystems());
        }

        private List<Type> SetFixedUpdateSystems()
        {
            return new List<Type>
            {
                typeof(OnCollisionSystem),
            };
        }

        private List<Type> SetGUISystems()
        {
            return new List<Type>
            {
                typeof(GamePauseScreenControlSystem),
                typeof(GameOverScreenControlSystem),
                typeof(PlayerSecondaryWeaponScreenControlSystem),
                typeof(PlayerShipStatsScreenControlSystem),
            };
        }

        private List<Type> SetUpdateSystems()
        {
            return new List<Type>
            {
                //Spawn Entities
                typeof(SpawnAsteroidSystem),
                typeof(SpawnAsteroidFragmentSystem),
                typeof(SpawnPlayerSystem),
                typeof(SpawnUfoSystem),
                typeof(SpawnPrimaryWeaponSystem),
                typeof(SpawnSecondaryWeaponSystem),
                typeof(SpawnProjectileSystem),
                typeof(OnSpawnSystem),
                typeof(SpawnEntityViewSystem),

                //Initializing
                typeof(RandomizePositionSystem),
                typeof(RandomizeVelocitySystem),
                typeof(RandomizePermanentRotationDirectionSysytem),
                typeof(RandomizeRotationSpeedSystem),
                typeof(SetFollowTargetSystem),

                //Player Input
                typeof(ClearPlayerInputSystem),
                typeof(PlayerInputSystem),

                //Weapons
                typeof(BulletGunAttackSystem),
                typeof(LaserGunAttackSystem),
                typeof(OnAttackSystem),
                typeof(AttackCoolDownSystem),

                //Ammo
                typeof(ChangeAmmoAmountSystem),
                typeof(CheckForAmmoAutoRefillSystem),
                typeof(ClearChangeAmmoAmountRequestSystem),
                typeof(AmmoAutoRefillCoolDownSystem),
                typeof(AmmoAutoRefillSystem),
                typeof(AmmoMaxSystem),

                //Movement
                typeof(MaxVelocityMagnitudeSystem),
                typeof(PermanentRotationSystem),
                typeof(BasicRotationSystem),
                typeof(AccelerationVectorSystem),
                typeof(AccelerationVelocitySystem),
                typeof(AccelerationPositionSystem),
                typeof(DeaccelerationVectorSystem),
                typeof(DeaccelerationVelocitySystem),
                typeof(DeaccelerationPositionSystem),
                typeof(BasicMovementSystem),
                typeof(AlignRotationWithTargetSystem),
                typeof(AlignVelocityWithTargetSystem),
                typeof(OutOfLevelCheckSystem),
                typeof(OnOutOfLevelSystem),
                typeof(TeleportationSystem),
                typeof(UpdateGameObjectViewRotationSystem),
                typeof(UpdateGameObjectViewPositionSystem),

                //Lifetime
                typeof(LifetimeSystem),

                //Events
                typeof(DamageSystem),
                typeof(OnDeathSystem),
                typeof(InvalidOwnerSystem),
                typeof(InvalidGOSystem),
                typeof(OnGamePauseSystem),
                typeof(OnGameOverSystem),
                typeof(OnGameRestartSystem),

                //Score
                typeof(ScoreSystem),
            };
        }
    }
}