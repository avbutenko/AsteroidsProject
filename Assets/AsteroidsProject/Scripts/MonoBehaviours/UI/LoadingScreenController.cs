﻿using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace AsteroidsProject.UI
{
    public class LoadingScreenController : BaseScreenController, ILoadingScreenController
    {
        [SerializeField] private CanvasGroup canvas;

        public async override void Hide()
        {
            await Disappear();
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            canvas.alpha = 1;
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
    }
}