using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.GameLogic.Features.Events.OnCollision;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class EcsFixedUpdateSystemsInstaller : Installer<EcsFixedUpdateSystemsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CollisionSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<EcsFixedUpdateSystemsRunner>().AsSingle();
        }
    }
}