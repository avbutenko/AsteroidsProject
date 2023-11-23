using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Spawn.EntityView
{
    public class SpawnEntityViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IGameObjectFactory gameObjectFactory;
        private readonly IActiveGOMappingService activeGOMappingService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CSpawnEntityViewRequest> requestPool;
        private EcsPool<CPosition> positionPool;
        private EcsPool<CRotation> rotationPool;
        private EcsPool<CParent> parentPool;

        public SpawnEntityViewSystem(IGameObjectFactory gameObjectFactory, IActiveGOMappingService activeGOMappingService)
        {
            this.gameObjectFactory = gameObjectFactory;
            this.activeGOMappingService = activeGOMappingService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CSpawnEntityViewRequest>()
                          .Inc<CPosition>()
                          .Inc<CRotation>()
                          .End();

            requestPool = world.GetPool<CSpawnEntityViewRequest>();
            positionPool = world.GetPool<CPosition>();
            rotationPool = world.GetPool<CRotation>();
            parentPool = world.GetPool<CParent>();
        }

        public void Run(IEcsSystems systems)
        {


            foreach (var entity in filter)
            {
                ref var prefabAddress = ref requestPool.Get(entity).PrefabAddress;
                ref var position = ref positionPool.Get(entity).Value;
                ref var rotation = ref rotationPool.Get(entity).Value;

                var spawnInfo = new SpawnEntityViewInfo
                {
                    PrefabAddress = prefabAddress,
                    Position = position,
                    Rotation = rotation,
                };

                if (parentPool.Has(entity))
                {
                    ref var parent = ref parentPool.Get(entity).Value;
                    spawnInfo.Parent = parent;
                    parentPool.Del(entity);
                }

                Spawn(entity, spawnInfo);
                requestPool.Del(entity);
            }
        }

        private async void Spawn(int entity, SpawnEntityViewInfo info)
        {
            var go = await gameObjectFactory.CreateAsync(info);
            var goID = go.GetInstanceID();

            world.AddComponentToEntity(entity, new CGameObjectInstanceID { Value = goID });
            activeGOMappingService.Add(goID, new GoEntityPair { Go = go, PackedEntity = world.PackEntity(entity) });
        }
    }
}