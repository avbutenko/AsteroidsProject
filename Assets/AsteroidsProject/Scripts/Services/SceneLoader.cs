using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace AsteroidsProject.Services
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadSceneAsync(string scene, LoadSceneMode loadMode, bool activeOnLoad)
        {
            var handle = Addressables.LoadSceneAsync(scene, loadMode, activeOnLoad);
            handle.GetAwaiter();
            await handle.ToUniTask();
            await handle.Result.ActivateAsync().ToUniTask();
        }
    }
}