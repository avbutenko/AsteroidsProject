using AsteroidsProject.CodeBase.Infrastructure;
using AsteroidsProject.CodeBase.Services;
using Zenject;

namespace AsteroidsProject.CodeBase.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }
    }
}
