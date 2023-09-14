﻿using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Teleportation
{
    public class TeleportationSystem : IEcsRunSystem
    {
        private readonly ILevelService level;

        public TeleportationSystem(ILevelService level)
        {
            this.level = level;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<TeleportationRequest>()
                              .Inc<Position>()
                              .Inc<Scale>()
                              .End();

            var teleportationRequestPool = world.GetPool<TeleportationRequest>();
            var positionPool = world.GetPool<Position>();
            var scalePool = world.GetPool<Scale>();

            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;
                ref var scale = ref scalePool.Get(entity).Value;

                position = level.GetOppositePosition(position, scale);
                teleportationRequestPool.Del(entity);
            }
        }
    }
}