﻿using Zenject;
using AsteroidsProject.Services;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.GameLogic.Features.Spawn;
using AsteroidsProject.GameLogic.Features.Movement;
using AsteroidsProject.GameLogic.Features.Events;
using AsteroidsProject.GameLogic.Features.Weapons;
using AsteroidsProject.GameLogic.Features.Score;
using AsteroidsProject.GameLogic.Features.Lifetime;
using AsteroidsProject.GameLogic.Features.Ammo;

namespace AsteroidsProject.CompositionRoot
{
    public class ComponentConverterServiceInstaller : Installer<ComponentConverterServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerTagConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidTagConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidFragmentTagConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletGunTagConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserGunTagConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletTagConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<UfoTagConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyTagConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<VelocityConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<MaxVelocityMagnitudeConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AccelerationModifierConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeaccelerationModifierConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<RotationSpeedConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizePositionRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizeVelocityRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizePermanentRotationDirectionRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomizeRotationSpeedRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnSpawnConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnOutOfLevelConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnCollisionConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnAttackConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<TeleportationRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPrimaryWeaponRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnSecondaryWeaponRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnProjectileRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnEntityViewRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AttackCoolDownConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<DamageRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<HealthConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<CollectScoreRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnDeathConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnAsteroidFragmentsRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<LifetimeConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AmmoConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AmmoMaxConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AmmoAutoRefillConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChangeAmmoAmountRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<SetFollowTargetRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverEventConverter>().AsSingle();

            Container.BindInterfacesAndSelfTo<ComponentConverterService>().AsSingle();
        }
    }
}