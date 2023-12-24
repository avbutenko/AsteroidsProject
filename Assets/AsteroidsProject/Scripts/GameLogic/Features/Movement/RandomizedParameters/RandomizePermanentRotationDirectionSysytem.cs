using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class RandomizePermanentRotationDirectionSysytem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CRandomizePermanentRotationDirectionRequest> requestPool;
        private EcsPool<CPermanentRotationDirection> directionPool;
        private EcsPool<CRotation> rotationPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CRandomizePermanentRotationDirectionRequest>().End();
            requestPool = world.GetPool<CRandomizePermanentRotationDirectionRequest>();
            directionPool = world.GetPool<CPermanentRotationDirection>();
            rotationPool = world.GetPool<CRotation>();
        }

        public void Run(IEcsSystems systems)
        {
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