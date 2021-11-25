using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBlocker : MonoBehaviour
{
    [SerializeField]
    float pullForce;
    [SerializeField]
    Rigidbody2D playerRB2D;
    [SerializeField]
    float lightningRate;
    [SerializeField]
    int lightningDamage;
    float nextLightningTime = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.DamageRecount(lightningDamage);
            if(collision.transform.position.x > transform.position.x)
            {
                playerRB2D = collision.GetComponent<Rigidbody2D>();
                playerRB2D.AddForce(transform.right * pullForce, ForceMode2D.Force);
                Debug.Log("Pull" + pullForce);
            }
            if (collision.transform.position.x < transform.position.x)
            {
                playerRB2D = collision.GetComponent<Rigidbody2D>();
                playerRB2D.AddForce(transform.right * -pullForce, ForceMode2D.Force);
                Debug.Log("Pull" + -pullForce);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if(Time.time >= nextLightningTime)
            {
                player.DamageRecount(lightningDamage);
                nextLightningTime = Time.time + lightningRate;
            }            
        }
    }
}
