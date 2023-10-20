using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IGameObjectFactory
    {
        public Task<GameObject> CreateAsync(SpawnInfo spawnInfo);
    }
}