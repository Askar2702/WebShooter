using GamePush;
using UnityEngine;
using TMPro;

public class GameScoreAdsManager : MonoBehaviour
{
    public static GameScoreAdsManager instance;
    public bool isReady { get; private set; }

    [SerializeField] private TextMeshProUGUI check;



    
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
        //GP_Ads.ShowFullscreen();
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
        check.text = isReady.ToString();
    }
    public void ShowReward(string idOrTag)
    {
        GP_Ads.ShowRewarded(idOrTag);
    }
    private void OnRewarded( string message)
    {
        WeaponShop.instance.BuyWeapon(int.Parse(message));

    }
}
