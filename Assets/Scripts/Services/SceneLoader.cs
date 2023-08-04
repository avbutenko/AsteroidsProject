﻿using AsteroidsProject.Infrastructure.Services;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AsteroidsProject.Services
{
    public class SceneLoader : ISceneLoader
    {

        private readonly ICoroutineRunner coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) =>
            this.coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null) =>
            coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}