using AsteroidsProject.EngineRelated.Services;
using AsteroidsProject.GameLogic;
using AsteroidsProject.GameLogic.Features.Spawn;
using Leopotam.EcsLite.UnityEditor;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Infrastructure.Services.IGameplayObjectViewFactory>().To<GameplayObjectViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EcsWorldDebugSystem>().AsTransient();
            Container.BindInterfacesAndSelfTo<PlayerSpawnSystem>().AsTransient();
            Container.BindInterfacesTo<EcsStartup>().AsSingle();
        }
    }
}