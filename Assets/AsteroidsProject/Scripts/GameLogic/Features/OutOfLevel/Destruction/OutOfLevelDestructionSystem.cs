using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.OutOfLevel.Destruction
{
    public class OutOfLevelDestructionSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var filter = world.Filter<COutOfLevelEvent>()
                              .Inc<COnOutOfLevelDestroyTag>()
                              .End();

            var eventPool = world.GetPool<COutOfLevelEvent>();
            var requestPool = world.GetPool<CDestroyRequest>();

            foreach (var entity in filter)
            {
                eventPool.Del(entity);
                requestPool.Add(entity);
            }
        }
    }
}