using Leopotam.EcsLite;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IGameObjectFactory
    {
        public Task<EntityWithGameObject> InstantiateAsync(string prefabAddress,
            Vector2 position, Quaternion rotation, Transform parentTransform, EcsWorld world);
    }
}