using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnLevel : MonoBehaviour
{
    Player player;
    [SerializeField] GameObject visibles;
    [SerializeField] DayTime dayTime;
    void Start()
    {
        dayTime = GameObject.FindGameObjectWithTag("DayTime").GetComponent<DayTime>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if(!dayTime.day)
        {
            visibles.SetActive(true);
        }
        else
        {
            visibles.SetActive(false);
        }
    }
}
