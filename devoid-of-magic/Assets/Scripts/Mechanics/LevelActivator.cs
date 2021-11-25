using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActivator : MonoBehaviour
{
    Player player;
    [SerializeField]
    GameObject child;
    [SerializeField]
    GameObject wizard;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.level >= 4)
        {
            wizard.SetActive(true);
            child.SetActive(true);
        }
        else
        {
            wizard.SetActive(false);
            child.SetActive(false);
        }
    }
}
