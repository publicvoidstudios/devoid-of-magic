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
        player.currentHealth = player.maxHealth;
        player.isDead = false;
        panel = GameObject.FindGameObjectWithTag("OverPanel");
        panel.SetActive(false);
        player.respawned = true;
        player.anim.Play("Idle");
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
