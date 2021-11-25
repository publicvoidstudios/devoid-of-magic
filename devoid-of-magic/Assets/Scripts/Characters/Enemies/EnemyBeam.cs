using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeam : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    Boss boss;
    [SerializeField]
    Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        boss = GetComponentInParent<Boss>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(collision.name + "damaged");
            if(enemy != null)
            {
                player.DamageRecount(enemy.laserDamage);
            }
            if (boss != null)
            {
                player.DamageRecount(boss.laserDamage);
            }            
            return;
        }
    }
}
