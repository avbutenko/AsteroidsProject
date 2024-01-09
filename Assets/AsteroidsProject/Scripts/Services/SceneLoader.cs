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
            await Addressables.LoadSceneAsync(scene, LoadSceneMode.Single, true);
        }
    }
}