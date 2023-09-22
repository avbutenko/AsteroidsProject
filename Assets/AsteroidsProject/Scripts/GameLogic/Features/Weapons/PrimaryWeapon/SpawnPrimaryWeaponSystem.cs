using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.PrimaryWeapon
{
    public class SpawnPrimaryWeaponSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IConfigProvider configProvider;
        private PlayerConfig config;

        public SpawnPrimaryWeaponSystem(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public async void Init(IEcsSystems systems)
        {
            config = await configProvider.Load<PlayerConfig>("Configs/PlayerConfig.json");
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<PlayerTag>()
                              .Inc<LinkToGameObject>()
                              .Inc<SpawnPrimaryWeaponRequest>()
                              .End();

            var spawnPrimaryWeaponRequestPool = world.GetPool<SpawnPrimaryWeaponRequest>();
            var linkToGameObjectPool = world.GetPool<LinkToGameObject>();

            foreach (var entity in filter)
            {
                ref var link = ref linkToGameObjectPool.Get(entity);
                var weaponSlot = link.View.GameObject.GetComponent<IPlayerGameObject>().PrimaryWeaponSlot;

                world.NewEntityWith(new SpawnPrefab
                {
                    PrefabAddress = config.PrimaryWeaponPrefabAddress,
                    Position = Vector2.zero,
                    Rotation = Quaternion.identity,
                    Parent = weaponSlot,
                    OwnerEntity = world.PackEntity(entity)
                });

                spawnPrimaryWeaponRequestPool.Del(entity);
            }
        }
    }
}

