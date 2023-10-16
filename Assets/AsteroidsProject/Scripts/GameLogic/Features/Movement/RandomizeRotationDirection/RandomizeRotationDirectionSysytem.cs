using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.RandomizeRotationDirection
{
    public class RandomizeRotationDirectionSysytem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CRandomizeRotationDirectionRequest>().End();

            var requestPool = world.GetPool<CRandomizeRotationDirectionRequest>();
            var pool = world.GetPool<CRotationDirection>();

            foreach (var entity in filter)
            {
                ref var range = ref requestPool.Get(entity).Range;
                pool.Add(entity).Value = Random.Range(range[0], range[1] + 1);
                requestPool.Del(entity);
            }
        }
    }
}