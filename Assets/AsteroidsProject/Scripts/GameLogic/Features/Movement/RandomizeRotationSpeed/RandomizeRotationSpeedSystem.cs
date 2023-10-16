using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.RandomizeRotationSpeed
{
    public class RandomizeRotationSpeedSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CRandomizeRotationSpeedRequest>().End();

            var requestPool = world.GetPool<CRandomizeRotationSpeedRequest>();
            var pool = world.GetPool<CRotationSpeed>();

            foreach (var entity in filter)
            {
                ref var range = ref requestPool.Get(entity).Range;
                pool.Add(entity).Value = Random.Range(range[0], range[1]);
                requestPool.Del(entity);
            }
        }
    }
}