using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ADInit : MonoBehaviour
{
    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(InitializationStatus => { });
    }
}
