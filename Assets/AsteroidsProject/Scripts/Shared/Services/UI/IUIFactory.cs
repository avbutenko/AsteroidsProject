using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IUIFactory
    {
        public IUIScreenController Create(string address);
        public IUIScreenController Instantiate(GameObject prefab);
    }
}