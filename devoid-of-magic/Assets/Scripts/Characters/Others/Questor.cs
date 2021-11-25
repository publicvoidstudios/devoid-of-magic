using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Questor : MonoBehaviour
{
    [SerializeField] Animator questUIAnim;
    [SerializeField] Animator talkButtonAnim;
    [SerializeField] Animator upgradesUIAnim;
    [SerializeField] TMP_Text speechText;
    float nextPhraseTime = 0;
    public string[] phrasesBeforeQuest;
    public string[] phrasesOnQuest;
    public string[] phrasesAfterQuest;
    public bool upgradesActivated = false;
    Player player;
    [SerializeField]
    int questorID;
    [SerializeField]
    GameObject questCarrier;
    public bool isWizard;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextPhraseTime)
        {
            if(player.level <= questorID) //Before quest
            {

                string randomPhrase = phrasesBeforeQuest[Random.Range(0, phrasesBeforeQuest.Length)];
                speechText.text = randomPhrase;
                nextPhraseTime = Time.time + Random.Range(5, 11);
            }
            if (player.level == questorID + 1) //Quest ongoing
            {
                string randomPhrase = phrasesOnQuest[Random.Range(0, phrasesOnQuest.Length)];
                speechText.text = randomPhrase;
                nextPhraseTime = Time.time + Random.Range(5, 11);
            }
            if (player.level >= questorID + 2) //Quest complete
            {
                string randomPhrase = phrasesAfterQuest[Random.Range(0, phrasesAfterQuest.Length)];
                speechText.text = randomPhrase;
                nextPhraseTime = Time.time + Random.Range(5, 11);
            }

        }
        if(questorID == player.level)
        {
            questCarrier.SetActive(true);
        }
        else
        {
            questCarrier.SetActive(false);
        }
    }

    public void WizardTalkButtonPressed()
    {
        if(player.level == 4)
        {
            upgradesActivated = true;
            talkButtonAnim.SetBool("Active", false);
            questUIAnim.SetBool("Active", true);
        }
        else
        {
            upgradesActivated = true;
            talkButtonAnim.SetBool("Active", false);
            upgradesUIAnim.SetBool("Active", true);
        }
    }
    public void TalkButtonPressed()
    {
        upgradesActivated = true;
        talkButtonAnim.SetBool("Active", false);
        questUIAnim.SetBool("Active", true);
    }

    public void UpgradesFinished()
    {
        upgradesActivated = false;
        questUIAnim.SetBool("Active", false);
        if(upgradesUIAnim != null && upgradesUIAnim.GetBool("Active") == true)
        {
            upgradesUIAnim.SetBool("Active", false);
        }
        talkButtonAnim.SetBool("Active", true);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && player.level == questorID)
        {
            talkButtonAnim.SetBool("Active", true);
        }
        if (other.CompareTag("Player") && isWizard)
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
