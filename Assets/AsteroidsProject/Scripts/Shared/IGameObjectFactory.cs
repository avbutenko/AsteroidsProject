using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IGameObjectFactory
    {
        public Task<EntityWithGameObject> InstantiateAsync(SpawnInfo spawnInfo);
        public Task<GameObject> InstantiateGameObjectAsync(SpawnInfo spawnInfo);
    }
}