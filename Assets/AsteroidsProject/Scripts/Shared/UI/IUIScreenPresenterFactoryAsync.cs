using Cysharp.Threading.Tasks;

namespace AsteroidsProject.Shared
{
    public interface IUIScreenPresenterFactoryAsync
    {
        public UniTask<IUIScreenPresenter> CreateAsync();
    }
}