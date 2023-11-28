using System;
using Zenject;

namespace AsteroidsProject.Shared
{
    public interface IEcsSystemsRunner : IInitializable, ITickable, IFixedTickable, IDisposable { }
}