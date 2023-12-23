using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace AsteroidsProject.Shared
{
    public interface IAssetProvider : IDisposable
    {
        public UniTask<T> LoadAsync<T>(string address) where T : class;
        public UniTask<T[]> LoadAsync<T>(List<string> addresses) where T : class;
        public UniTask<T[]> LoadByLabelAsync<T>(string label) where T : class;
        public UniTask PreLoadAllByLabelAsync(string label);
        public UniTask<T> ResourceLoadAsync<T>(string resourceAddress) where T : class;
        public T ResourceLoad<T>(string resourceAddress) where T : class;
    }
}