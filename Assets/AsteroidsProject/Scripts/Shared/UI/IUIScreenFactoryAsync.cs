using Cysharp.Threading.Tasks;

namespace AsteroidsProject.Shared
{
    public interface IUIScreenFactoryAsync
    {
        public UniTask<IUIScreenPresenter> CreateAsync();
    }
}