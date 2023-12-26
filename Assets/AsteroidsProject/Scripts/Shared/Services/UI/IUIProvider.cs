using Cysharp.Threading.Tasks;
using System;

namespace AsteroidsProject.Shared
{
    public interface IUIProvider : IDisposable
    {
        public IUIScreenController LoadingScreen { get; }
        public UniTask PreInitUIByLabel(string label);
        public T Get<T>() where T : IUIScreenController;
    }
}