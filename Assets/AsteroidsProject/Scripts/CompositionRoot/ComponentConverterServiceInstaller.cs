using AsteroidsProject.GameLogic.Core;
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
            Container.BindInterfacesAndSelfTo<VelocityConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<MaxVelocityMagnitudeConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPrefabRequestConverter>().AsSingle();
            Container.BindInterfacesAndSelfTo<ComponentConverterService>().AsSingle();
        }
    }
}