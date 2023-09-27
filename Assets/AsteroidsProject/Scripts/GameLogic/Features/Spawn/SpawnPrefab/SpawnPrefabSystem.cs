using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.SpawnPrefab
{
    public class SpawnPrefabSystem : BaseSpawnSystem, IEcsRunSystem
    {
        public SpawnPrefabSystem(IGameObjectFactory factory) : base(factory) { }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<Core.SpawnPrefab>().End();
            var spawnPrefabPool = world.GetPool<Core.SpawnPrefab>();

            foreach (var entity in filter)
            {
                ref var prefabAddress = ref spawnPrefabPool.Get(entity).PrefabAddress;
                ref var position = ref spawnPrefabPool.Get(entity).Position;
                ref var rotation = ref spawnPrefabPool.Get(entity).Rotation;
                ref var parent = ref spawnPrefabPool.Get(entity).Parent;

                SpawnPrefab(new SpawnInfo
                {
                    PrefabAddress = prefabAddress,
                    Position = position,
                    Rotation = rotation,
                    Parent = parent,
                    World = world
                }); ;

                world.DelEntity(entity);
            }
        }

        private async void SpawnPrefab(SpawnInfo info)
        {
            await Spawn(info);
        }
    }
}