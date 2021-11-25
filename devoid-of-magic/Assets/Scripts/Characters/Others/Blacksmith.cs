using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Blacksmith : MonoBehaviour
{
    [SerializeField] Animator repairsUIAnim;
    [SerializeField] Animator talkButtonAnim;
    [SerializeField] TMP_Text speechText;
    [SerializeField] Canvas controls;
    float nextPhraseTime = 0;
    public string[] phrases;
    public bool upgradesActivated = false;
    [SerializeField]
    Player playerScript;
    bool repairing;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.currentArmor <= playerScript.maxArmor * 0.7f && !repairing)
        {
            speechText.text = "I see cracks in your armor, come closer, I can fix that";
        }
        if (playerScript.currentArmor > playerScript.maxArmor * 0.7f || repairing)
        {
            if (Time.time >= nextPhraseTime)
            {
                string randomPhrase = phrases[Random.Range(0, phrases.Length)];
                speechText.text = randomPhrase;
                nextPhraseTime = Time.time + Random.Range(5, 11);
            }
        }        
    }

    public void TalkButtonPressed()
    {
        controls.enabled = false;
        upgradesActivated = true;
        talkButtonAnim.SetBool("Active", false);
        repairsUIAnim.SetBool("Active", true);
    }

    public void RepairsFinished()
    {
        controls.enabled = true;
        upgradesActivated = false;
        repairsUIAnim.SetBool("Active", false);
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
