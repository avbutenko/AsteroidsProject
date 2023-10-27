using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Randomization.PermanentRotationDirection
{
    public class RandomizePermanentRotationDirectionSysytem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CRandomizePermanentRotationDirectionRequest>().End();

            var requestPool = world.GetPool<CRandomizePermanentRotationDirectionRequest>();
            var directionPool = world.GetPool<CPermanentRotationDirection>();
            var rotationPool = world.GetPool<CRotation>();

            foreach (var entity in filter)
            {
                ref var range = ref requestPool.Get(entity).Range;
                directionPool.Add(entity).Value = Random.Range(range[0], range[1] + 1);
                rotationPool.Add(entity).Value = Quaternion.identity;
                requestPool.Del(entity);
            }
        }
    }
}