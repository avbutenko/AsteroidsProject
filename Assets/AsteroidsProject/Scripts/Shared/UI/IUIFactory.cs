using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IUIFactory
    {
        public IUIScreenController Create(string address);
        public UniTask<IUIScreenController> InstantiateAsync(GameObject prefab);
        public IUIScreenController Instantiate(GameObject prefab);
    }
}