﻿using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System;
using System.Reflection;

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

        public static void AddRawComponentToEntity(this EcsWorld world, int entity, object component)
        {
            var componentType = component.GetType();
            var pool = world.GetPoolByType(componentType);

            if (pool == null)
            {
                var method = world.GetType().GetMethod(nameof(EcsWorld.GetPool));
                var generic = method.MakeGenericMethod(componentType);
                pool = (IEcsPool)generic.Invoke(world, null);
            }

            pool.AddRaw(entity, component);
        }
    }
}