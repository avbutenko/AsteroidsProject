using Zenject;
using AsteroidsProject.Services;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.GameLogic.Features.Spawn.Weapons;
using AsteroidsProject.GameLogic.Features.Movement.Acceleration;
using AsteroidsProject.GameLogic.Features.Movement.Deacceleration;
using AsteroidsProject.GameLogic.Features.Events.OnSpawn;
using AsteroidsProject.GameLogic.Features.Events.OnOutOfLevel;
using AsteroidsProject.GameLogic.Features.MaxVelocityMagnitude;
using AsteroidsProject.GameLogic.Features.Weapons.BulletGun;
using AsteroidsProject.GameLogic.Features.Weapons.LaserGun;
using AsteroidsProject.GameLogic.Features.Projectiles.Bullet;
using AsteroidsProject.GameLogic.Features.Events.OnCollision;
using AsteroidsProject.GameLogic.Features.Damage;
using AsteroidsProject.GameLogic.Features.Events.OnAttack;
using AsteroidsProject.GameLogic.Core.Assets.AsteroidsProject.Scripts.GameLogic.Core.Score;
using AsteroidsProject.GameLogic.Features.Events.OnDeath;
using AsteroidsProject.GameLogic.Features.Score;

namespace AsteroidsProject.CompositionRoot
{
    public class ComponentConverterServiceInstaller : Installer<ComponentConverterServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerTagConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidTagConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletGunTagConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserGunTagConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletTagConverter>().AsSingle();
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
            Container.BindInterfacesAndSelfTo<CoolDownConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<DamageRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<HealthConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<CollectScoreRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnDeathConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnFragmentsRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<CScoreConverter>().AsSingle();

            Container.BindInterfacesAndSelfTo<ComponentConverterService>().AsSingle();
        }
    }
}