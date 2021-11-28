using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
public class ADReward : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private Player player;
    private GameObject panel;
    private void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        //Rewarded AD Request
        RequestRewarded();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void RequestRewarded()
    {
            string adUnitId;
        #if UNITY_ANDROID
            adUnitId = "ca-app-pub-9536718512266906/7690133275";
        #elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
        #else
            adUnitId = "unexpected_platform";
        #endif

        rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad is shown.
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        print("HandleRewardedAdFailedToShow event received with message: " + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        print("HandleRewardedAdClosed event received");
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        player.currentHealth = player.maxHealth;
        player.isDead = false;
        panel = GameObject.FindGameObjectWithTag("OverPanel");
        panel.SetActive(false);
        player.respawned = true;
    }

    public void ShowAd()
    {
        AudioManager audio = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
        audio.Play("Click");
        if (!player.respawned)
        {
            if (rewardedAd.IsLoaded())
            {
                rewardedAd.Show();
            }
        }
        else
        {
            Debug.Log("No more attempts");
        }
        
    }
}
