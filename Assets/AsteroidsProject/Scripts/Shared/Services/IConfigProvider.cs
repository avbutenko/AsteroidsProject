using Cysharp.Threading.Tasks;

namespace AsteroidsProject.Shared
{
    public interface IConfigProvider
    {
        public string GameConfigPath { get; }
        public UniTask<T> Load<T>(string configAddress) where T : class;
    }
}