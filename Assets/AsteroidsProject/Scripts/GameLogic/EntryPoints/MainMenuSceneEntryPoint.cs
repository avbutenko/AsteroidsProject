using AsteroidsProject.GameLogic.EntryPoints;
using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;

namespace AsteroidsProject.GameLogic.EntryPoint.MainMenuScene
{
    public class MainMenuSceneEntryPoint : SceneRunner
    {
        public MainMenuSceneEntryPoint(
            IEcsSystemsRunner ecsSystemsRunner, IAssetProvider assetProvider, IUIProvider uiProvider,
            IGameConfigProvider configProvider, IConfigLoader configLoader)
            : base(ecsSystemsRunner, assetProvider, uiProvider, configProvider, configLoader) { }

        protected async override UniTask<SceneConfig> LoadSceneConfigAsync()
        {
            return await configLoader.Load<SceneConfig>(configProvider.GameConfig.ScenesConfig.MainMenuSceneConfigLabel);
        }

        protected override void ShowInSceneUI()
        {
            uiProvider.Get<IMainMenuScreenController>().Show();
        }
    }
}