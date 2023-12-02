using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace AsteroidsProject.UI.LoadingScreen
{
    public class LoadingScreenView : MonoBehaviour, ILoadingScreenView
    {
        [SerializeField] private CanvasGroup canvas;

        public void Show()
        {
            gameObject.SetActive(true);
            canvas.alpha = 1;
        }

        public async void Hide()
        {
            await Disappear();
        }

        private async UniTask Disappear()
        {
            while (canvas.alpha > 0)
            {
                canvas.alpha -= 0.03f;
                await UniTask.Delay(TimeSpan.FromSeconds(0.03f), ignoreTimeScale: false);
            }

            gameObject.SetActive(false);
        }

        public void DontDestroyOnLoad()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}