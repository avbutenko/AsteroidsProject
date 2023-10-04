using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.GameLogic.Features.BulletHitSystem;
using AsteroidsProject.GameLogic.Features.LaserHitSystem;
using AsteroidsProject.GameLogic.Features.PlayerHitSystem;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class EcsFixedUpdateSystemsInstaller : Installer<EcsFixedUpdateSystemsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerHitSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletHitSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserHitSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<EcsFixedUpdateSystemsRunner>().AsSingle();
        }
    }
}