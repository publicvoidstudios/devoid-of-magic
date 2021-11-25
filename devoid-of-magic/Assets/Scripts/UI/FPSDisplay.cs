using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text FPS;
    private FPSCounter fPSCounter;
    private void Awake()
    {
        fPSCounter = GetComponent<FPSCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        FPS.text = "FPS: "+ fPSCounter.FPS.ToString();
    }
}
