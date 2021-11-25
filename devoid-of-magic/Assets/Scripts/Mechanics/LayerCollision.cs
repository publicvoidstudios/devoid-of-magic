using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCollision : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject[] enemy;
    [SerializeField] public GameObject[] enemies;
    [SerializeField] public GameObject[] dropExample;
    void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        dropExample = GameObject.FindGameObjectsWithTag("Drop");
        foreach(GameObject e in enemies)
        {
            Physics2D.IgnoreLayerCollision(e.layer, e.layer, true);
        }
        //foreach (GameObject e in enemies)
        //{
        //    Physics2D.IgnoreLayerCollision(e.layer, player.layer, true);
        //}
        foreach (GameObject e in enemy)
        {
            foreach(GameObject d in dropExample)
            {
                Physics2D.IgnoreLayerCollision(e.layer, d.layer, true);
            }
        }
    }
}
