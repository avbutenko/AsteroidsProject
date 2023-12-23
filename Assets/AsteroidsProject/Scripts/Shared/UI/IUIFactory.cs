using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IUIFactory
    {
        public IUIScreenController Create(string address);
        public UniTask<IUIScreenController> CreateAsync(string address);
        public IUIScreenController Instantiate(GameObject prefab);
    }
}