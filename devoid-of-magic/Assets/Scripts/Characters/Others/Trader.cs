using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trader : MonoBehaviour
{
    [SerializeField] Animator salesUIAnim;
    [SerializeField] Animator talkButtonAnim;
    [SerializeField] TMP_Text speechText;
    float nextPhraseTime = 0;
    public string[] phrases;
    public bool upgradesActivated = false;
    public bool trainerOnMap = true;

    // Start is called before the first frame update
    void Start()
    {

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
    }

    public void TalkButtonPressed()
    {
        upgradesActivated = true;
        talkButtonAnim.SetBool("Active", false);
        salesUIAnim.SetBool("Active", true);
    }

    public void UpgradesFinished()
    {
        upgradesActivated = false;
        salesUIAnim.SetBool("Active", false);
        talkButtonAnim.SetBool("Active", true);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            talkButtonAnim.SetBool("Active", true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            talkButtonAnim.SetBool("Active", false);
        }
    }
}
