using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;
using UnityEngine.Events;

public class GoogleMobileAdsDemoScript : MonoBehaviour
 {
    public static GoogleMobileAdsDemoScript Instance;
    private BannerView bannerView;
    private RewardedAd rewardedAd;
    
    [SerializeField] string bannerIdIos = "";
    [SerializeField] string interIdIos = "";
    [SerializeField] string rewardedIdIos = "";
   
    [SerializeField] string bannerIdAndroid = "";
    [SerializeField] string interIdAndroid = "";
    [SerializeField] string rewardedIdAndroid = ""; 
 
    
    Action  rewardAction;

    [SerializeField] UnityEvent makeRewardActive;
   // private string idNative = "ca-app-pub-2174206089482966/2250541704";
    private bool isInterLoaded = false;
    private bool isRewardedLoaded = false;
    public bool showBanner = true;
    public bool showAppOpen = false;

    //int rewardActionIndex = 0;


      // //TestADS
    //  private string bannerIdAndroid = "ca-app-pub-3940256099942544/6300978111";
    //  private string interIdAndroid = "ca-app-pub-3940256099942544/1033173712";
    private InterstitialAd interstitial;

    int isPro;
    bool showAdDelayFinished = true;

    public void Start()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(Instance); 
            Instance = this;
        } 
        else 
        { 
            Instance = this; 
        } 
        isPro =  PlayerPrefs.GetInt(Application.identifier + "IsPro");
        
        //Instance = this;
        if(isPro == 0){
            RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
                    .SetMaxAdContentRating(MaxAdContentRating.T)
                    .build();
            MobileAds.SetRequestConfiguration(requestConfiguration);
            MobileAds.Initialize(initStatus => {
                if(showBanner)
                    this.RequestBanner();
                this.RequestInterstitial();
                this.RequestRewarded();
                // AppOpenAddManager.Instance.LoadAd();
                // if(showAppOpen) 
                //     AppOpenAddManager.Instance.ShowAdIfAvailable();
            });    
        }else{
            MobileAds.Initialize(initStatus => {
                this.RequestRewarded();
            });    
        }

         
    }

    public void OnApplicationPause(bool paused)
    {
        // Display the app open ad when the app is foregrounded
        if (!paused && isPro==0 && showAppOpen)
        {
            AppOpenAddManager.Instance.ShowAdIfAvailable();
        }
    }


    private void RequestRewarded(){
        
        #if UNITY_ANDROID
            this.rewardedAd = new RewardedAd(rewardedIdAndroid);
        #elif UNITY_IPHONE
            this.rewardedAd = new RewardedAd(rewardedIdIos);
        #else
            string adUnitId = "unexpected_platform";
        #endif

        AdRequest request = new AdRequest.Builder().Build();
        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        this.rewardedAd.LoadAd(request);
    }

    public void UserChoseToWatchAd(Action  action)
    {
        rewardAction = action;
        if (this.rewardedAd.IsLoaded()) {
            this.rewardedAd.Show();
        }
    }


    public bool IsRewardedLoaded()
    {
        return this.rewardedAd.IsLoaded();
    }


    private void RequestBanner()
    {
        #if UNITY_ANDROID
            this.bannerView = new BannerView(bannerIdAndroid, AdSize.Banner, AdPosition.Bottom);
        #elif UNITY_IPHONE
            this.bannerView = new BannerView(bannerIdIos, AdSize.Banner, AdPosition.Bottom);
        #else
            string adUnitId = "unexpected_platform";
        #endif
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        this.bannerView.LoadAd(request);

        this.bannerView.Show();
        
    }

    void OnDestroy() {
        this.bannerView.Destroy();
    }

    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
            this.interstitial = new InterstitialAd(interIdAndroid);
        #elif UNITY_IPHONE
            this.interstitial = new InterstitialAd(interIdIos);
        #else
            string adUnitId = "unexpected_platform";
        #endif


    // Called when an ad request has successfully loaded.
    this.interstitial.OnAdLoaded += HandleOnInterLoaded;
    // // Called when an ad request failed to load.
    // this.interstitial.OnAdFailedToLoad += HandleOnInterFailedToLoad;
    // // Called when an ad is shown.
    // this.interstitial.OnAdOpening += HandleOnInterOpened;
    // // Called when the ad is closed.
    // this.interstitial.OnAdClosed += HandleOnInterClosed;
    // // Called when the ad click caused the user to leave the application.
    // this.interstitial.OnAdLeavingApplication += HandleOnInterLeavingApplication;

    AdRequest request = new AdRequest.Builder().Build();
    // Load the interstitial with the request.
    this.interstitial.LoadAd(request);
    }

    public void ShowIntertitialAd(){
        if(IsInterLoaded() && isPro == 0 && showAdDelayFinished){
            this.interstitial.Show();
            isInterLoaded = false;
            showAdDelayFinished = false;
            StartCoroutine(DelayAddShow());
            RequestInterstitial();
        }
    }

    IEnumerator DelayAddShow(){
        yield return new WaitForSeconds(10);
        showAdDelayFinished = true;
    }

    public void HandleOnInterLoaded(object sender, EventArgs args)
    {
        isInterLoaded = true;
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public bool IsInterLoaded(){
        return isInterLoaded;
    }

    AdRequest AdRequestBuild ()
	{
		return new AdRequest.Builder ().Build ();
	}  



    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        makeRewardActive.Invoke();
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        // MonoBehaviour.print(
        //     "HandleRewardedAdFailedToLoad event received with message: "
        //                      + args.Message);
        this.RequestRewarded();
        
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
       
        
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
                             this.RequestRewarded();
       // notRewardAction.Invoke();
        
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        this.RequestRewarded();
      //  notRewardAction.Invoke();
      
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);

        //LobyMenuManager.Instance.AddReward();
        rewardAction.Invoke();
        this.RequestRewarded();
       
    }
}
