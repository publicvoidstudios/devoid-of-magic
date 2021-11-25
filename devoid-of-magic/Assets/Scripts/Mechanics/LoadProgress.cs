using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadProgress : MonoBehaviour
{
    Player source;
    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        source.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
