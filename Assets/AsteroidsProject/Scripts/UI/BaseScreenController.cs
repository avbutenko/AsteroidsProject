using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.UI
{
    public abstract class BaseScreenController : MonoBehaviour, IUIScreenController
    {
        public bool IsVisible => gameObject.activeSelf;

        public void Awake()
        {
            Hide();
        }

        public void DontDestroyOnLoad()
        {
            DontDestroyOnLoad(gameObject);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
    }
}