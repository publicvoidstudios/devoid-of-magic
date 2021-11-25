using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordersTrigger : MonoBehaviour
{
    public bool restricted;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC") || other.CompareTag("Trader") || other.CompareTag("Enemy"))
        {
            restricted = false;
        }
        
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC") || other.CompareTag("Trader") || other.CompareTag("Enemy"))
        {
            restricted = true;
        }
    }
}
