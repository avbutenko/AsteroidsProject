using Leopotam.EcsLite;
using System.Collections.Generic;

namespace AsteroidsProject.Shared
{
    public interface IEcsSystemListProvider
    {
        public List<IEcsSystem> UpdateSystemList { get; }
        public List<IEcsSystem> FixedUpdateSystemList { get; }
        public List<IEcsSystem> UguiSystemList { get; }
    }
}