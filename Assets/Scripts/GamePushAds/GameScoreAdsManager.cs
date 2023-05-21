using GamePush;
using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class GameScoreAdsManager : MonoBehaviour
{
    public static GameScoreAdsManager instance;
    public bool isReady { get; private set; }




    
    private void Awake()
    {
        if (!instance) instance = this;
    }
    private void OnEnable()
    {
        GP_SDK.OnReady += OnSDKReady;
        GP_Ads.OnRewardedReward += OnRewarded;
        GP_Ads.OnAdsStart += OffSound;
        GP_Ads.OnAdsClose += OnSound;
        GP_Ads.OnPreloaderStart += OffSound;
        GP_Ads.OnPreloaderClose += OnSound;
        GP_Ads.OnRewardedStart += OffSound;
        GP_Ads.OnRewardedClose += OnSound;
        GP_Ads.OnFullscreenStart += OffSound;
        GP_Ads.OnFullscreenClose += OnSound;
    }

    private void OnSound(bool arg0)
    {
        AudioListener.volume = 1;
    }

    private void OffSound()
    {
        AudioListener.volume = 0;
    }

    private void Start()
    {
        GP_Ads.ShowSticky();
        GP_Ads.ShowFullscreen();
    }
    private void OnDisable()
    {
        GP_SDK.OnReady -= OnSDKReady;
        GP_Ads.OnRewardedReward -= OnRewarded;
    }

    private void OnSDKReady()
    {
        Debug.Log("Ready");
        isReady = true;
    }
    public void ShowReward(string idOrTag)
    {
        if (WeaponShop.instance.CheckInventorySize())
            GP_Ads.ShowRewarded(idOrTag);
    }
    private void OnRewarded( string message)
    {
        WeaponShop.instance.BuyWeapon(int.Parse(message));
    }

   
}
