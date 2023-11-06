using System;
using UnityEngine;
using GoogleMobileAds.Api;
public class ADBanner : MonoBehaviour
{
    private BannerView bannerView;
    public void Start()
    {
        //Request Banner
        RequestBanner();
    }

    private void RequestBanner()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-5678296141046279/3304600470";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    private void OnEnable()
    {
        RequestBanner();
        bannerView.Show();
        Debug.Log("bannerView.Show(); called");
    }

    private void OnDisable()
    {
        bannerView.Hide();
        Debug.Log("bannerView.Hide(); called");
    }
}
