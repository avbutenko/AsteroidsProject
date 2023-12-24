using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class RandomizeRotationSpeedSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CRandomizeRotationSpeedRequest> requestPool;
        private EcsPool<CRotationSpeed> rotationSpeedPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CRandomizeRotationSpeedRequest>().End();
            requestPool = world.GetPool<CRandomizeRotationSpeedRequest>();
            rotationSpeedPool = world.GetPool<CRotationSpeed>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var range = ref requestPool.Get(entity).Range;
                rotationSpeedPool.Add(entity).Value = Random.Range(range[0], range[1]);
                requestPool.Del(entity);
            }
        }
    }
}