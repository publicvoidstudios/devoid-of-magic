using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestWindow : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;
    Player playerScript;
    [SerializeField]
    Button acceptButton;
    [SerializeField]
    TMP_Text buttonText;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void SaveChanges()
    {
        playerScript.Save();
    }

    public void CanvasEnabled(bool enabled)
    {
        canvas.enabled = enabled;
    }
    public void AcceptQuest()
    {        
        playerScript.level += 1;
        playerScript.sublevel = 1;
        acceptButton.interactable = false;
        buttonText.text = "ACCEPTED";
    }
}
