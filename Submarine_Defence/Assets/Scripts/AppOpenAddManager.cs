
using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AppOpenAddManager 
{
    #if UNITY_ANDROID
    public const string AD_UNIT_ID = "ca-app-pub-9032617439824601/8083739034";
    #elif UNITY_IOS
    public const string AD_UNIT_ID = "ca-app-pub-9032617439824601/4706907244";
    #else
    private const string AD_UNIT_ID = "unexpected_platform";
    #endif

    private static AppOpenAddManager instance;

    private AppOpenAd ad;

    private bool isShowingAd = false;

    public static AppOpenAddManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AppOpenAddManager();
            }

            return instance;
        }
    }

    public void ShowAdIfAvailable()
    {
        if (!IsAdAvailable || isShowingAd)
        {
            return;
        }

        ad.OnAdDidDismissFullScreenContent += HandleAdDidDismissFullScreenContent;
        ad.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresentFullScreenContent;
        ad.OnAdDidPresentFullScreenContent += HandleAdDidPresentFullScreenContent;
        ad.OnAdDidRecordImpression += HandleAdDidRecordImpression;
        ad.OnPaidEvent += HandlePaidEvent;

        ad.Show();
    }

    private void HandleAdDidDismissFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("Closed app open ad");
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        isShowingAd = false;
        LoadAd();
    }

    private void HandleAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args)
    {
        Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        LoadAd();
    }

    private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("Displayed app open ad");
        isShowingAd = true;
    }

    private void HandleAdDidRecordImpression(object sender, EventArgs args)
    {
        Debug.Log("Recorded ad impression");
    }

    private void HandlePaidEvent(object sender, AdValueEventArgs args)
    {
        Debug.LogFormat("Received paid event. (currency: {0}, value: {1}",
                args.AdValue.CurrencyCode, args.AdValue.Value);
    }


    private DateTime loadTime;

    private bool IsAdAvailable
    {
        get
        {
            return ad != null && (System.DateTime.UtcNow - loadTime).TotalHours < 4;
        }
    }

    public void LoadAd()
    {
        if (IsAdAvailable)
        {
            return;
        }

        AdRequest request = new AdRequest.Builder().Build();
        AppOpenAd.LoadAd(AD_UNIT_ID, ScreenOrientation.Portrait, request, ((appOpenAd, error) =>
        {
            if (error != null)
            {
                Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
                return;
            }

            ad = appOpenAd;
            loadTime = DateTime.UtcNow;
           // ShowAdIfAvailable();
        }));

      //  ShowAdIfAvailable();
    }
}