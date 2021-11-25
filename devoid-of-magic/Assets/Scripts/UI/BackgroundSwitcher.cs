using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSwitcher : MonoBehaviour
{
    [SerializeField] 
    GameObject[] state;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.sublevel == 2 && player.level == 1)
        {
            state[0].SetActive(false);
            state[1].SetActive(true);
        }
        else
        {
            state[0].SetActive(true);
            state[1].SetActive(false);
        }
    }
}
