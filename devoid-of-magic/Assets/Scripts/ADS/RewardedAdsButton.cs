using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{

#if UNITY_IOS
    private string gameId = "4619272";
#elif UNITY_ANDROID
    private string gameId = "4619273";
#endif
    public bool testMode = false;

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

    [SerializeField] Button myButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
        //Set button to this GO if buuton is null
        if (myButton == null)
            myButton = this.gameObject.GetComponent<Button>();
        // Disable the button until the ad is ready to show:
        myButton.interactable = false;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnEnable()
    {
        // Initialize the SDK if you haven't already done so:
        Advertisement.Initialize(gameId, testMode);
        LoadAd();
    }

    // Call this public method when you want to get an ad ready to show.
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            myButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            myButton.interactable = true;
        }
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Disable the button:
        myButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            watched = true;
            // Reward the user for watching the ad to completion.
            if (reward == RewardType.Tools)
                player.tools++;
            if (reward == RewardType.Life)
            {
                player.currentHealth = player.maxHealth;
                player.isDead = false;
                GameObject panel = GameObject.FindGameObjectWithTag("OverPanel");
                panel.SetActive(false);
                player.respawned = true;
                player.anim.Play("Idle");
                player.anim.SetBool("Dead", false);
            }
            if (reward == RewardType.Upgrade)
            {
                Ability_Switcher_UI absw = GameObject.FindGameObjectWithTag("ABSW").GetComponent<Ability_Switcher_UI>();
                if (absw != null)
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
            if (reward == RewardType.Magic)
                player.magicDamage++;            
        }
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // Clean up the button listeners:
        myButton.onClick.RemoveAllListeners();
    }

}
