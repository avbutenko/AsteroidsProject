using Leopotam.EcsLite.Unity.Ugui;

namespace AsteroidsProject.Shared
{
    public interface IHaveUIRoot
    {
        public EcsUguiEmitter UIRoot { get; set; }
    }
}