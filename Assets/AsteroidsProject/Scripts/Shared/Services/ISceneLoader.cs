using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace AsteroidsProject.Shared
{
    public interface ISceneLoader
    {
        public UniTask LoadSceneAsync(string scene, LoadSceneMode loadMode, bool activeOnLoad);
    }
}