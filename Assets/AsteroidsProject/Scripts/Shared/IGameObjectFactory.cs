using System.Threading.Tasks;

namespace AsteroidsProject.Shared
{
    public interface IGameObjectFactory
    {
        public Task<EntityWithGameObject> InstantiateAsync(SpawnInfo spawnInfo);
    }
}