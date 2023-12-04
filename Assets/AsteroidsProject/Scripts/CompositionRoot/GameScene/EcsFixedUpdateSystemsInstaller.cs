using AsteroidsProject.GameLogic.Features.Events.OnCollision;
using AsteroidsProject.Services;
using Leopotam.EcsLite.UnityEditor;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class EcsFixedUpdateSystemsInstaller : Installer<EcsFixedUpdateSystemsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CollisionSystem>().AsSingle();
#if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<EcsSystemsDebugSystem>().AsSingle().WithArguments(new object[] { "FixedUpdate Systems" });
#endif
            Container.BindInterfacesAndSelfTo<EcsFixedUpdateSystemsProvider>().AsSingle();
        }
    }
}