using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{

#if UNITY_IOS
    private string gameId = "4619272";
#elif UNITY_ANDROID
    private string gameId = "4619273";
#endif

    Button myButton;
    public string myPlacementId = "Rewarded_Android";    
    Player player;

    public RewardType reward;
    public bool watched = false;
    public enum RewardType
    {
        Tools,
        Life,
        Upgrade,
        Magic
    } 

    void OnEnable()
    {
        myButton = GetComponent<Button>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        

        // Set interactivity to be dependent on the Placement’s status:
        myButton.interactable = Advertisement.IsReady(myPlacementId);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, false); //Set to false when publishing!
    }

    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo()
    {
        if(!watched)
            Advertisement.Show(myPlacementId);
    }
    void OnDisable()
    {
        myButton.onClick.RemoveAllListeners();
        Advertisement.RemoveListener(this);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            watched = true;
            // Reward the user for watching the ad to completion.
            if (reward == RewardType.Tools)
                player.tools++;
            if(reward == RewardType.Life)
            {
                player.currentHealth = player.maxHealth;
                player.isDead = false;
                GameObject panel = GameObject.FindGameObjectWithTag("OverPanel");
                panel.SetActive(false);
                player.respawned = true;
                player.anim.Play("Idle");
                player.anim.SetBool("Dead", false);
            }
            if(reward == RewardType.Upgrade)
            {
                Ability_Switcher_UI absw = GameObject.FindGameObjectWithTag("ABSW").GetComponent<Ability_Switcher_UI>();
                if(absw != null)
                {
                    if (absw.currentAbility == 0) //Armor
                    {
                        player.armorLevel++;
                    }
                    if (absw.currentAbility == 1) //Axe
                    {
                        player.axeLevel++;
                    }
                    if (absw.currentAbility == 2) //Bow
                    {
                        player.bowLevel++;
                    }
                    if (absw.currentAbility == 3) //HP
                    {
                        player.strengthLevel++;
                    }
                }                
            }
            if(reward == RewardType.Magic)
            {
                player.magicDamage++;
            }
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.        
        }
        else if (showResult == ShowResult.Failed)
        {

        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}
