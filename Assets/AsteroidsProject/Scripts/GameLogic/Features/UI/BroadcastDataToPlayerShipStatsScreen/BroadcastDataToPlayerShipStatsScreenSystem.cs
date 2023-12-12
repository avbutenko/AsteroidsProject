using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.UI.BroadcastDataToPlayerShipStatsScreen
{
    public class BroadcastDataToPlayerShipStatsScreenSystem : IEcsInitSystem, IEcsRunSystem
    {
        private Vector3 axis = Vector3.up;
        private readonly IUIService uiService;
        private IPlayerShipStatsScreenPresenter screenPresenter;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CHealth> healthPool;
        private EcsPool<CPosition> positionPool;
        private EcsPool<CRotation> rotationPool;
        private EcsPool<CVelocity> velocityPool;


        public BroadcastDataToPlayerShipStatsScreenSystem(IUIService uiService)
        {
            this.uiService = uiService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            filter = world.Filter<CPlayerTag>()
                          .Inc<CHealth>()
                          .Inc<CPosition>()
                          .Inc<CRotation>()
                          .Inc<CVelocity>()
                          .End();

            healthPool = world.GetPool<CHealth>();
            positionPool = world.GetPool<CPosition>();
            rotationPool = world.GetPool<CRotation>();
            velocityPool = world.GetPool<CVelocity>();
            screenPresenter = uiService.Get<IPlayerShipStatsScreenPresenter>();
            screenPresenter.Show();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var health = ref healthPool.Get(entity).Value;
                ref var position = ref positionPool.Get(entity).Value;
                ref var rotation = ref rotationPool.Get(entity).Value;
                ref var velocity = ref velocityPool.Get(entity).Value;

                screenPresenter.Health = health;
                screenPresenter.Position = position;
                rotation.ToAngleAxis(out var angle, out axis);
                screenPresenter.Rotation = angle; ;
                screenPresenter.Velocity = velocity.magnitude;
            }
        }
    }
}