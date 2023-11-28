using AsteroidsProject.GameLogic.Features.Events.OnCollision;
using AsteroidsProject.Services;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class EcsFixedUpdateSystemsInstaller : Installer<EcsFixedUpdateSystemsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CollisionSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<EcsFixedUpdateSystemsProvider>().AsSingle();
        }
    }
}