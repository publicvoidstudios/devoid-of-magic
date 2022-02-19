using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsBanner : MonoBehaviour
{

    public string gameId = "4619273";
    public string placementId = "Banner_Android";
    public bool testMode = false;

    void OnEnable ()
    {
        // Initialize the SDK if you haven't already done so:
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowBannerWhenReady());
    }

    void OnDisable()
    {
        Advertisement.Banner.Hide();
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(placementId);
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
    }
}