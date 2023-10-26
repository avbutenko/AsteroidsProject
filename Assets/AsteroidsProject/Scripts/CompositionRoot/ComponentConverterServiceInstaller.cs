using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.GameLogic.Features.AccelerationMovement;
using AsteroidsProject.GameLogic.Features.DeaccelerationMovement;
using AsteroidsProject.GameLogic.Features.MaxVelocityMagnitude;
using AsteroidsProject.Services;
using Zenject;

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

            Container.BindInterfacesAndSelfTo<TeleportationRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<DestructionRequestConverter>().AsSingle();

            Container.BindInterfacesAndSelfTo<SpawnPrimaryWeaponRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnSecondaryWeaponRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPrefabRequestConverter>().AsSingle();

            Container.BindInterfacesAndSelfTo<CoolDownConverter>().AsSingle();

            Container.BindInterfacesAndSelfTo<ComponentConverterService>().AsSingle();
        }
    }
}