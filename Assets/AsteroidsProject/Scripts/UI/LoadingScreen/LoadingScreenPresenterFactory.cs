using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.UI.LoadingScreen
{
    public class LoadingScreenPresenterFactory : IUIScreenPresenterFactory
    {
        private const string prefabAddress = "LoadingScreen";

        public IUIScreenPresenter Create()
        {
            var prefab = Resources.Load<GameObject>(prefabAddress);
            var presenter = Object.Instantiate(prefab);
            return presenter.GetComponent<IUIScreenPresenter>();
        }
    }
}