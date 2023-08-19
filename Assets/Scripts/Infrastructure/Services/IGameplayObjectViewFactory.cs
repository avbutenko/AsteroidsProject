using AsteroidsProject.Infrastructure.Views;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Infrastructure.Services
{
    public interface IGameplayObjectViewFactory
    {
        public Task<GameObject> InstantiateAsync(string prefabAddress, Vector3 position, Quaternion rotation, Transform parentTransform);
    }
}