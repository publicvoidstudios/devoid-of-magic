using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
public class ADRewardStartingLocation : MonoBehaviour
{
    private RewardedAd rewardedAd;
    public Player player;

    private void Start()
    {
        //Rewarded AD Request
        RequestRewarded();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void RequestRewarded()
    {
            string adUnitId;
        #if UNITY_ANDROID
            adUnitId = "ca-app-pub-5678296141046279/2921457097";
        #elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
        #else
            adUnitId = "unexpected_platform";
        #endif

        rewardedAd = new RewardedAd(adUnitId);
        // Called when the user should be rewarded for interacting with the ad.
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
    }
    private void HandleUserEarnedReward(object sender, Reward e)
    {
        player.tools++;
        RequestRewarded();
    }

    public void ShowAd()
    {
        AudioManager audio = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
        audio.Play("Click");
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }
}