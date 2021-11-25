using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackground : MonoBehaviour
{
    [SerializeField]
    Player data;
    [SerializeField]
    GameObject[] backgrounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(data.level >= 0 && data.level <= 2)
        {
            backgrounds[0].SetActive(true);
            backgrounds[1].SetActive(false);
            backgrounds[2].SetActive(false);
        }
        if (data.level >= 3 && data.level <= 4)
        {
            backgrounds[0].SetActive(false);
            backgrounds[1].SetActive(true);
            backgrounds[2].SetActive(false);
        }
        if (data.level >= 5)
        {
            backgrounds[0].SetActive(false);
            backgrounds[1].SetActive(false);
            backgrounds[2].SetActive(true);
        }
    }
}
