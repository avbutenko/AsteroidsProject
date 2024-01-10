using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite.Unity.Ugui;

namespace AsteroidsProject.GameLogic.EntryPoints
{
    public class MainMenuSceneEntryPoint : SceneRunner
    {
        public MainMenuSceneEntryPoint(EcsUguiEmitter uiRoot,
            IEcsSystemsRunner ecsSystemsRunner, IAssetProvider assetProvider, IUIProvider uiProvider,
            IGameConfigProvider configProvider, IConfigLoader configLoader)
            : base(uiRoot, ecsSystemsRunner, assetProvider, uiProvider, configProvider, configLoader) { }

        protected async override UniTask<SceneConfig> LoadSceneConfigAsync()
        {
            return await configLoader.Load<SceneConfig>(configProvider.GameConfig.ScenesConfig.MainMenuSceneConfigLabel);
        }
    }
}