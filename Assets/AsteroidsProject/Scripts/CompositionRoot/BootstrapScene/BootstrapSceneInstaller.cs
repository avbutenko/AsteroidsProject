using AsteroidsProject.GameLogic.EntryPoint.BootstrapScene;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class BootstrapSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BootstrapSceneEntryPoint>().AsSingle();
        }
    }
}