﻿using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Ecs
{
    public class DeleteHereSystem<TComponent> : IEcsRunSystem where TComponent : struct
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<TComponent>().End();
            var pool = world.GetPool<TComponent>();
            foreach (var index in filter) pool.Del(index);
        }
    }
}