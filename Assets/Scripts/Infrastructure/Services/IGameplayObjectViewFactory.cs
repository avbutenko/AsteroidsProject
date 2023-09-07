using Leopotam.EcsLite;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Infrastructure.Services
{
    public interface IGameplayObjectViewFactory
    {
        public Task<GameObject> InstantiateAsync(string prefabAddress,
            Vector2 position, Quaternion rotation, Transform parentTransform, EcsWorld world);
    }
}