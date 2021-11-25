using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trainer : MonoBehaviour
{
    [SerializeField] 
    Animator trainerAnim;
    [SerializeField] Animator upgradesUIAnim;
    [SerializeField] Animator talkButtonAnim;
    [SerializeField] TMP_Text speechText;
    Player playerScript;
    float nextPhraseTime = 0;
    public string[] phrases;
    public bool upgradesActivated = false;


    // Start is called before the first frame update
    void Start()
    {        
        trainerAnim.SetBool("PlayerInRange", false);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();        
    }

    // Update is called once per frame
    void Update()
    {        
        if (Time.time >= nextPhraseTime)
        {
            string randomPhrase = phrases[Random.Range(0, phrases.Length)];
            speechText.text = randomPhrase;
            nextPhraseTime = Time.time + Random.Range(5, 11);
        }
        playerScript.currentHealth = playerScript.maxHealth;
    }

    public void TalkButtonPressed()
    {
        upgradesActivated = true;
        trainerAnim.SetBool("PlayerInRange", false);
        talkButtonAnim.SetBool("Active", false);
        upgradesUIAnim.SetBool("Active", true);
    }

    public void UpgradesFinished()
    {
        upgradesActivated = false;
        upgradesUIAnim.SetBool("Active", false);
        talkButtonAnim.SetBool("Active", true);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            trainerAnim.SetBool("PlayerInRange", true);
            talkButtonAnim.SetBool("Active", true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            trainerAnim.SetBool("PlayerInRange", false);
            talkButtonAnim.SetBool("Active", false);
        }
    }
}
