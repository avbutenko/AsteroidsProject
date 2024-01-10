using Cysharp.Threading.Tasks;
using System;

namespace AsteroidsProject.Shared
{
    public interface IUIProvider : IHaveUIRoot, IDisposable
    {
        public IUIScreenController LoadingScreen { get; }
        public UniTask InitAllSceneUIByLabel(string label);
        public T Get<T>() where T : IUIScreenController;
    }
}