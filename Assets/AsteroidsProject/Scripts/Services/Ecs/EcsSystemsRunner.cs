using AsteroidsProject.Shared;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using System.Collections.Generic;

namespace AsteroidsProject.Services
{
    public class EcsSystemsRunner : IEcsSystemsRunner, IRestartable
    {
        private readonly EcsUguiEmitter uguiEmitter;
        private readonly IEcsSystemsFactory systemsFactory;
        private EcsWorld world;
        private List<IEcsSystem> updateSystemsList;
        private List<IEcsSystem> fixedUpdateSystemsList;
        private IEcsSystems fixedUpdateSystems;
        private IEcsSystems updateSystems;

        public EcsSystemsRunner(EcsUguiEmitter uguiEmitter, IEcsSystemsFactory systemsFactory)
        {
            this.uguiEmitter = uguiEmitter;
            this.systemsFactory = systemsFactory;
        }

        public void PreInitSystems(List<string> updateSystemNameList, List<string> fixedUpdateSystemNameList)
        {
            updateSystemsList = InitSystems(updateSystemNameList);
            fixedUpdateSystemsList = InitSystems(fixedUpdateSystemNameList);
        }

        public void Initialize()
        {
            world = new EcsWorld();
            EcsPhysicsEvents.ecsWorld = world;
            updateSystems = ConvertSystemList(updateSystemsList);
#if UNITY_EDITOR
            updateSystems?.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif
            updateSystems?.InjectUgui(uguiEmitter);
            updateSystems?.Init();
            fixedUpdateSystems = ConvertSystemList(fixedUpdateSystemsList);
            fixedUpdateSystems?.Init();
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

        private List<IEcsSystem> InitSystems(List<string> systemsList)
        {
            if (systemsList == null)
            {
                return null;
            }
            else
            {
                var systems = new List<IEcsSystem>();

                foreach (var systemName in systemsList)
                {
                    systems.Add(systemsFactory.Create(systemName));
                }

                return systems;
            }
        }

        private IEcsSystems ConvertSystemList(List<IEcsSystem> systemsList)
        {
            if (systemsList == null)
            {
                return null;
            }
            else
            {
                var systems = new EcsSystems(world);

                foreach (var system in systemsList)
                {
                    systems.Add(system);
                }

                return systems;
            }
        }
    }
}