using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.SpawnPrefab
{
    public class SpawnPrefabSystemOLD : BaseSpawnSystem, IEcsRunSystem
    {
        public SpawnPrefabSystemOLD(IGameObjectFactory factory) : base(factory) { }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<SpawnPrefabRequestOLD>().End();
            var spawnPrefabPool = world.GetPool<SpawnPrefabRequestOLD>();

            foreach (var entity in filter)
            {
                ref var prefabAddress = ref spawnPrefabPool.Get(entity).SpawnInfo.PrefabAddress;
                ref var position = ref spawnPrefabPool.Get(entity).SpawnInfo.Position;
                ref var rotation = ref spawnPrefabPool.Get(entity).SpawnInfo.Rotation;
                ref var parent = ref spawnPrefabPool.Get(entity).SpawnInfo.Parent;

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