using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Spawn.OnSpawn
{
    public class OnSpawnSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<COnSpawn>().End();

            var spawnPool = world.GetPool<COnSpawn>();

            foreach (var entity in filter)
            {
                ref var components = ref spawnPool.Get(entity).Components;
                foreach (var component in components)
                {
                    world.AddRawComponentToEntity(entity, component);
                }
                spawnPool.Del(entity);
            }
        }
    }
}