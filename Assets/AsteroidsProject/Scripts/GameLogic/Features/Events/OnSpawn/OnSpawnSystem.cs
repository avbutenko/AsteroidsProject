using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events.OnSpawn
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
                ref var components = ref spawnPool.Get(entity).AddToSelfComponents;
                world.AddRawComponentsToEntity(entity, components);
                spawnPool.Del(entity);
            }
        }
    }
}