using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    GoalDetector goalDetector;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            goalDetector.savedChild = true;
            animator.SetBool("Saved", true);
        }
    }
}
