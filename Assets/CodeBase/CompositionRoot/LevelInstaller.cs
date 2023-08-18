using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;
using Zenject;

namespace AsteroidsProject.Assets.CodeBase.CompositionRoot
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AsteroidsProject.CodeBase.Infrastructure.IFactory>().To<Factory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EcsWorldDebugSystem>().AsTransient();
            Container.BindInterfacesTo<EcsStartup>().AsSingle();
        }
    }
}