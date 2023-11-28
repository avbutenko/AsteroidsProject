using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IUIFactory
    {
        public UniTask<T> CreateAsync<T>(string prefabAddress);
        public UniTask<T> CreateAsync<T>(string prefabAddress, Transform parent);
    }
}