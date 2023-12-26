using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace AsteroidsProject.Services
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadSceneAsync(string scene)
        {
            var handle = Addressables.LoadSceneAsync(scene, LoadSceneMode.Single, false);
            handle.GetAwaiter();
            await handle.ToUniTask();
            await handle.Result.ActivateAsync().ToUniTask();
        }
    }
}