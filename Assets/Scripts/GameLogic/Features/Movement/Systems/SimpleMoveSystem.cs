using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class SimpleMoveSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
        }
    }
}

