using AsteroidsProject.GameLogic.SceneRunners.BootstrapScene;
using Zenject;

namespace Assets.AsteroidsProject.Scripts.CompositionRoot
{
    public class BootstrapSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BootstrapSceneRunner>().AsSingle();
        }
    }
}