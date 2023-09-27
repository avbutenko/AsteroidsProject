﻿using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Core
{
    public static class EcsWorldExtensions
    {
        public static void NewEntityWith<TComponent>(this EcsWorld world) where TComponent : struct
        {
            var entity = world.NewEntity();
            var pool = world.GetPool<TComponent>();
            pool.Add(entity);
        }

        public static void NewEntityWith<TComponent>(this EcsWorld world, TComponent value) where TComponent : struct
        {
            var entity = world.NewEntity();
            world.AddComponentToEntity(entity, value);
        }

        public static void AddComponentToEntity<TComponent>(this EcsWorld world, int entity, TComponent component)
            where TComponent : struct
        {
            var pool = world.GetPool<TComponent>();
            pool.Add(entity);
            ref var reference = ref pool.Get(entity);
            reference = component;
        }
    }
}