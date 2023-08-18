using AsteroidsProject.Infrastructure.Services;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}