using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System;
using System.Collections.Generic;
using Zenject;

namespace AsteroidsProject.Services
{
    public class EcsSystemListProvider : IEcsSystemListProvider, IInitializable
    {
        private readonly IEcsSystemsFactory factory;
        private readonly List<Type> updateSystemTypesList;
        private readonly List<Type> fixedUpdateSystemTypesList;
        private readonly List<Type> uGUISystemTypesList;
        private List<IEcsSystem> updateSystemList;
        private List<IEcsSystem> fixedUpdateSystemList;
        private List<IEcsSystem> uguiSystemList;

        public EcsSystemListProvider(
            List<Type> updateSystemTypesList,
            List<Type> fixedUpdateSystemTypesList,
            List<Type> uGUISystemTypesList,
            IEcsSystemsFactory factory)
        {
            this.updateSystemTypesList = updateSystemTypesList;
            this.fixedUpdateSystemTypesList = fixedUpdateSystemTypesList;
            this.uGUISystemTypesList = uGUISystemTypesList;
            this.factory = factory;
        }

        public List<IEcsSystem> UpdateSystemList => updateSystemList;

        public List<IEcsSystem> FixedUpdateSystemList => fixedUpdateSystemList;

        public List<IEcsSystem> UguiSystemList => uguiSystemList;

        public void Initialize()
        {
            updateSystemList = InitSystems(updateSystemTypesList);
            fixedUpdateSystemList = InitSystems(fixedUpdateSystemTypesList);
            uguiSystemList = InitSystems(uGUISystemTypesList);
        }

        private List<IEcsSystem> InitSystems(List<Type> systemTypeList)
        {
            if (systemTypeList == null)
            {
                return null;
            }
            else
            {
                var systemList = new List<IEcsSystem>();

                foreach (var systemType in systemTypeList)
                {
                    systemList.Add(factory.Create(systemType));
                }

                return systemList;
            }
        }
    }
}