using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IAssetProvider
    {
        public UniTask<T> LoadAsync<T>(string address) where T : Object;
    }
}