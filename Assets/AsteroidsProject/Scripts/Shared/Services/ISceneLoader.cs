using Cysharp.Threading.Tasks;

namespace AsteroidsProject.Shared
{
    public interface ISceneLoader
    {
        public UniTask LoadSceneAsync(string scene);
    }
}