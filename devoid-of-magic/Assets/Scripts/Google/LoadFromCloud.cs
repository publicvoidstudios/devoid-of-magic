using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFromCloud : MonoBehaviour
{
    [SerializeField]
    GPGSManager gPGSManager;
    void Start()
    {
        gPGSManager.OpenSave(false);
    }
}
