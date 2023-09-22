﻿using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.LaserGun
{
    public class LaserGunAttackSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<LaserGunTag>()
                              .Inc<AttackRequest>()
                              .End();

            var attackRequestPool = world.GetPool<AttackRequest>();

            foreach (var entity in filter)
            {
                Debug.Log("Spawn Laser!!!");
                attackRequestPool.Del(entity);
            }
        }
    }
}