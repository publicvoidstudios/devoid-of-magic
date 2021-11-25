using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField]
    GoalDetector goalDetector; //GD
    [SerializeField] GameObject[] blockers;

    private void Start()
    {
        goalDetector = GameObject.FindGameObjectWithTag("GD").GetComponent<GoalDetector>();
        blockers = GameObject.FindGameObjectsWithTag("Blocker");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            goalDetector.foundOrb = true;
            foreach(GameObject b in blockers)
            {
                Destroy(b);
            }
            Destroy(gameObject);
        }
    }
}
