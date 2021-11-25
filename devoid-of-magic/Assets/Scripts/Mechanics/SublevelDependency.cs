using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SublevelDependency : MonoBehaviour
{
    [SerializeField] Light2D globalLight2D;
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
            globalLight2D.intensity = 0.25f;
        }
        else
        {
            globalLight2D.intensity = 0.95f;
        }
    }
}
