using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite;
using System;
using System.Collections.Generic;
using Zenject;

namespace AsteroidsProject.Ecs
{
    public sealed class EcsStartup : IInitializable, IDisposable, ITickable
    {
        private EcsWorld world;
        private IEcsSystems systems;
        private readonly IEnumerable<IEcsSystem> bindedSystems;

        public EcsStartup(IEnumerable<IEcsSystem> bindedSystems)
        {
            this.bindedSystems = bindedSystems;
        }

        public void Initialize()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);
            InitializeSystems();
        }

        private void InitializeSystems()
        {
            foreach (var system in bindedSystems)
                systems.Add(system);

            systems.ConvertScene();
            systems.Init();
        }

        public void Tick()
        {
            systems?.Run();
        }

        public void Dispose()
        {
            if (systems != null)
            {
                systems.Destroy();
                systems = null;
            }

            if (world != null)
            {
                world.Destroy();
                world = null;
            }
        }
    }
}