using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite.Unity.Ugui;

namespace AsteroidsProject.GameLogic.EntryPoints
{
    public class GameSceneEntryPoint : SceneRunner
    {
        public GameSceneEntryPoint(EcsUguiEmitter uiRoot,
            IEcsSystemsRunner ecsSystemsRunner, IAssetProvider assetProvider, IUIProvider uiProvider,
            IGameConfigProvider configProvider, IConfigLoader configLoader)
            : base(uiRoot, ecsSystemsRunner, assetProvider, uiProvider, configProvider, configLoader) { }

        protected async override UniTask<SceneConfig> LoadSceneConfigAsync()
        {
            return await configLoader.Load<GameSceneConfig>(configProvider.GameConfig.ScenesConfig.GameSceneConfigLabel);
        }
    }
}