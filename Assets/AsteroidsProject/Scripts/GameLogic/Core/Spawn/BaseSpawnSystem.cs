using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System.Threading.Tasks;

namespace AsteroidsProject.GameLogic.Core
{
    public abstract class BaseSpawnSystem
    {
        private readonly IGameObjectFactory factory;

        public BaseSpawnSystem(IGameObjectFactory factory)
        {
            this.factory = factory;
        }

        protected async Task<EntityWithGameObject> Spawn(SpawnInfo spawnInfo)
        {
            var entityWithGameObject = await factory.InstantiateAsync(spawnInfo);
            var gameObject = entityWithGameObject.GameObject.GetComponent<ILinkToGameObject>();
            spawnInfo.World.AddComponentToEntity(entityWithGameObject.Entity, new LinkToGameObject { View = gameObject });
            spawnInfo.World.AddComponentToEntity(entityWithGameObject.Entity, new Position { Value = spawnInfo.Position });
            spawnInfo.World.AddComponentToEntity(entityWithGameObject.Entity, new Rotation { Value = spawnInfo.Rotation });

            if (spawnInfo.OwnerEntity.Unpack(spawnInfo.World, out int ownerEntity))
            {
                spawnInfo.World.AddComponentToEntity(entityWithGameObject.Entity, new OwnerEntity { Value = spawnInfo.OwnerEntity });
            }

            return entityWithGameObject;
        }
    }
}