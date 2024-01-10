using System;
using System.Collections.Generic;
using Zenject;

namespace AsteroidsProject.Shared
{
    public interface IEcsSystemsRunner : IInitializable, ITickable, IFixedTickable, IDisposable, IHaveUIRoot
    {
        public void PreInitSystems(List<string> updateSystemNameList, List<string> fixedUpdateSystemNameList, List<string> uguiSystemNameList);
        public void Restart();
    }
}