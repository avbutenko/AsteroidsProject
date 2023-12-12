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
#if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem>().AsSingle().WithArguments(new object[] { "FixedUpdate Systems" });
#endif
            Container.BindInterfacesAndSelfTo<EcsFixedUpdateSystemsProvider>().AsSingle();
        }
    }
}