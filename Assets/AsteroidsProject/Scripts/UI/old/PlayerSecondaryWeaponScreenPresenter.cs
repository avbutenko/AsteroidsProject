using AsteroidsProject.Shared;
using System;
//using TMPro;
//using UniRx;
//using UniRx.Extensions;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.UI.PlayerSecondaryWeaponScreen
{
    public class PlayerSecondaryWeaponScreenPresenter //: MonoBehaviour, IPlayerSecondaryWeaponScreenPresenter
    {
        //[SerializeField] private TextMeshProUGUI ammo;
        //[SerializeField] private TextMeshProUGUI ammoAutoRefillCooldown;

        //private IPlayerSecondaryWeaponScreenModel model;
        //private CompositeDisposable trash;

        //[Inject]
        //public void Construct(IPlayerSecondaryWeaponScreenModel model)
        //{
        //    this.model = model;
        //}

        //public void Awake()
        //{
        //    trash = new CompositeDisposable();
        //    Hide();
        //}

        //public void Start()
        //{
        //    model.Ammo.DistinctUntilChanged().SubscribeToText(ammo).AddTo(trash);
        //    model.AmmoAutoRefillCooldown.DistinctUntilChanged().SubscribeToText(ammoAutoRefillCooldown).AddTo(trash);
        //}

        //public int Ammo
        //{
        //    get => model.Ammo.Value;
        //    set => model.Ammo.Value = value;
        //}
        //public float AmmoAutoRefillCooldown
        //{
        //    get => model.AmmoAutoRefillCooldown.Value;
        //    set => model.AmmoAutoRefillCooldown.Value = (float)Math.Round(value, 2);
        //}

        //public bool IsVisible => gameObject.activeSelf;

        //public void Hide()
        //{
        //    gameObject.SetActive(false);
        //}

        //public void Show()
        //{
        //    gameObject.SetActive(true);
        //}

        //public void OnDestroy()
        //{
        //    trash.Dispose();
        //}
    }
}