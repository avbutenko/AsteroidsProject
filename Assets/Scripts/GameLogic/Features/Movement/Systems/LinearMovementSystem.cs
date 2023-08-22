using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class LinearMovementSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            //var filter = world
            //    .Filter<Position>()
            //    .Inc<Movement>()
            //    .Inc<MovementSpeed>()
            //    .Exc<Acceleration>()
            //    .End();
        }
    }
}

