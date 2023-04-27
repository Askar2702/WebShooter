using GamePush;
using UnityEngine;
using TMPro;
using System.Collections;

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
