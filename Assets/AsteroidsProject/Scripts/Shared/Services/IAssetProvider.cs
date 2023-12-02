using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace AsteroidsProject.Shared
{
    public interface IAssetProvider
    {
        public UniTask<T> LoadAsync<T>(string address) where T : class;
        public UniTask<T[]> LoadAsync<T>(List<string> addresses) where T : class;
        public UniTask PreLoadAsyncByLabel(string label);
        public UniTask<T> ResourceLoadAsync<T>(string resourceAddress) where T : class;
    }
}