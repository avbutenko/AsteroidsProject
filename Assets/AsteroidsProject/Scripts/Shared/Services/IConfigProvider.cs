using Cysharp.Threading.Tasks;

namespace AsteroidsProject.Shared
{
    public interface IConfigProvider
    {
        public UniTask<T> Load<T>(string configAddress) where T : class;
    }
}