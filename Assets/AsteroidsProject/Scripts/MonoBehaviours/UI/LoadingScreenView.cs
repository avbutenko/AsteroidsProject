using AsteroidsProject.Shared;
using System.Collections;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public class LoadingScreenView : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] private CanvasGroup canvas;

        public void Show()
        {
            gameObject.SetActive(true);
            canvas.alpha = 1;
        }

        public void Hide()
        {
            StartCoroutine(DoFadeIn());
        }

        private IEnumerator DoFadeIn()
        {
            while (canvas.alpha > 0)
            {
                canvas.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            gameObject.SetActive(false);
        }
    }
}