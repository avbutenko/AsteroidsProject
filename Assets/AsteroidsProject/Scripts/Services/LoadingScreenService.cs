using AsteroidsProject.Shared;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.Services
{
    public class LoadingScreenService : ILoadingScreen, IInitializable
    {
        private ILoadingScreen loadingScreen;

        public void Initialize()
        {
            var obj = Resources.Load("LoadingScreen");
            var go = Object.Instantiate(obj as GameObject);
            loadingScreen = go.GetComponent<ILoadingScreen>();
            Object.DontDestroyOnLoad(go);
        }

        public void Show()
        {
            loadingScreen.Show();
        }

        public void Hide()
        {
            loadingScreen.Hide();
        }
    }
}