using System.Collections;
using UnityEngine;

namespace AsteroidsProject.Infrastructure.Services
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}