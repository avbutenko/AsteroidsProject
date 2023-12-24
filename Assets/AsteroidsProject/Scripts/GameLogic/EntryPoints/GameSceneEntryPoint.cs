using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;

namespace AsteroidsProject.GameLogic.EntryPoints
{
    public class GameSceneEntryPoint : SceneRunner
    {
        public GameSceneEntryPoint(
            IEcsSystemsRunner ecsSystemsRunner, IAssetProvider assetProvider, IUIProvider uiProvider,
            IGameConfigProvider configProvider, IConfigLoader configLoader)
            : base(ecsSystemsRunner, assetProvider, uiProvider, configProvider, configLoader) { }

        protected async override UniTask<SceneConfig> LoadSceneConfigAsync()
        {
            return await configLoader.Load<GameSceneConfig>(configProvider.GameConfig.ScenesConfig.GameSceneConfigLabel);
        }

        protected override void ShowInSceneUI()
        {
            //show inGameUI here
        }
    }
}