using Leopotam.EcsLite;
using System;
using System.Collections.Generic;
using Zenject;

namespace AsteroidsProject.GameLogic.Core
{
    public abstract class EcsBaseStartup : IInitializable, IDisposable
    {
        private EcsWorld world;
        private readonly IEnumerable<IEcsSystem> bindedSystems;
        protected IEcsSystems systems;

        public EcsBaseStartup(EcsWorld world, IEnumerable<IEcsSystem> bindedSystems)
        {
            this.world = world;
            this.bindedSystems = bindedSystems;
        }

        public virtual void Initialize()
        {
            systems = new EcsSystems(world);
            InitializeSystems();
        }

        private void InitializeSystems()
        {
            foreach (var system in bindedSystems)
                systems.Add(system);

            systems.Init();
        }

        public virtual void Dispose()
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