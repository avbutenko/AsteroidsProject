using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.RandomizedVelocity
{
    public class RandomizeVelocitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IConfigProvider configProvider;
        private float[] XRange;
        private float[] YRange;

        public RandomizeVelocitySystem(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public async void Init(IEcsSystems systems)
        {
            var config = await configProvider.Load<AsteroidConfig>("Configs/AsteroidConfig.json");

            XRange = config.VelocityRange.XRange;
            YRange = config.VelocityRange.YRange;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<RandomizeVelocityRequest>()
                              .Inc<Velocity>()
                              .End();

            var randomizeVelocityRequestPool = world.GetPool<RandomizeVelocityRequest>();
            var velocityPool = world.GetPool<Velocity>();

            foreach (var entity in filter)
            {
                ref var velocity = ref velocityPool.Get(entity).Value;

                velocity.x = Random.Range(XRange[0], XRange[1]);
                velocity.y = Random.Range(YRange[0], YRange[1]);

                randomizeVelocityRequestPool.Del(entity);
            }
        }
    }
}