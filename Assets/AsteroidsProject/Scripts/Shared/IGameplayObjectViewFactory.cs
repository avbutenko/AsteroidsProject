using Leopotam.EcsLite;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IGameplayObjectViewFactory
    {
        public Task<EntityLinkedToView> InstantiateAsync(string prefabAddress,
            Vector2 position, Quaternion rotation, Transform parentTransform, EcsWorld world);
    }
}