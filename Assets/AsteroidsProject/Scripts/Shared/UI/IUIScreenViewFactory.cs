using Cysharp.Threading.Tasks;

namespace AsteroidsProject.Shared
{
    public interface IUIScreenViewFactory
    {
        public UniTask<IUIScreenView> CreateAsync(string prefabAddress);
    }
}