using Cysharp.Threading.Tasks;

namespace AsteroidsProject.Shared
{
    public interface IUIService
    {
        public UniTask PreLoadUI();
        public T Get<T>() where T : IUIScreenPresenter;
    }
}