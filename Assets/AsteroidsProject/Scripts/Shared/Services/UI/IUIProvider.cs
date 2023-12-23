using Cysharp.Threading.Tasks;

namespace AsteroidsProject.Shared
{
    public interface IUIProvider
    {
        public IUIScreenController LoadingScreen { get; }
        public UniTask PreInitUIByLabel(string label);
        public T Get<T>() where T : IUIScreenController;
    }
}