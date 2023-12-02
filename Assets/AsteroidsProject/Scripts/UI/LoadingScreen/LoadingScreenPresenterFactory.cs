using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.UI.LoadingScreen
{
    public class LoadingScreenPresenterFactory : IUIScreenPresenterFactory
    {
        private const string prefabAddress = "LoadingScreen";

        public IUIScreenPresenter Create()
        {
            var view = GetView();
            return new LoadingScreenPresenter(view);
        }

        private ILoadingScreenView GetView()
        {
            var prefab = Resources.Load<GameObject>(prefabAddress);
            var instance = Object.Instantiate(prefab);
            instance.SetActive(false);
            var view = instance.GetComponent<ILoadingScreenView>();
            return view;
        }
    }
}