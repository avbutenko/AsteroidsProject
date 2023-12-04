using AsteroidsProject.Shared;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;
using System.Collections.Generic;

namespace AsteroidsProject.Services
{
    public class EcsSystemsRunner : IEcsSystemsRunner, IRestartable
    {
        private readonly IEcsUpdateSystemsProvider updateSystemsProvider;
        private readonly IEcsFixedUpdateSystemsProvider fixedUpdateSystemsProvider;
        private EcsWorld world;
        private IEcsSystems fixedUpdateSystems;
        private IEcsSystems updateSystems;

        public EcsSystemsRunner(IEcsUpdateSystemsProvider updateSystemsProvider, IEcsFixedUpdateSystemsProvider fixedUpdateSystemsProvider)
        {
            this.updateSystemsProvider = updateSystemsProvider;
            this.fixedUpdateSystemsProvider = fixedUpdateSystemsProvider;
        }

        public void Initialize()
        {
            world = new EcsWorld();
            EcsPhysicsEvents.ecsWorld = world;
            updateSystems = GetSystems(updateSystemsProvider.BindedSystems);
            fixedUpdateSystems = GetSystems(fixedUpdateSystemsProvider.BindedSystems);

#if UNITY_EDITOR
            updateSystems.Add(new EcsWorldDebugSystem());
#endif

            updateSystems.Init();
            fixedUpdateSystems.Init();
        }

        public void Tick()
        {
            updateSystems?.Run();
        }

        public void FixedTick()
        {
            fixedUpdateSystems?.Run();
        }

        public void Restart()
        {
            Dispose();
            Initialize();
        }

        public virtual void Dispose()
        {
            EcsPhysicsEvents.ecsWorld = null;

            if (updateSystems != null)
            {
                updateSystems.Destroy();
                updateSystems = null;
            }

            if (fixedUpdateSystems != null)
            {
                fixedUpdateSystems.Destroy();
                fixedUpdateSystems = null;
            }

            if (world != null)
            {
                world.Destroy();
                world = null;
            }
        }

        private IEcsSystems GetSystems(List<IEcsSystem> bindedSystems)
        {
            var systems = new EcsSystems(world);

            foreach (var system in bindedSystems)
            {
                systems.Add(system);
            }

            return systems;
        }
    }
}