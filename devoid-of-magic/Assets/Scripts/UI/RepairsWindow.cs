using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RepairsWindow : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;        
    [SerializeField]
    TMP_Text goldPriceText;    
    [SerializeField]
    TMP_Text playerNameText;
    [SerializeField]
    TMP_Text playerGoldText;
    [SerializeField]
    TMP_Text playerToolsText;
    [SerializeField]
    TMP_Text countdownText;
    [SerializeField]
    Slider countdownSlider;
    [SerializeField]
    Button[] buttons;
    [SerializeField]
    GameObject player;
    Player playerScript;
    int goldprice;
    [SerializeField]
    float adjustableTime;
    [SerializeField]
    float defaultTime;
    [SerializeField]
    float countdowntime;
    bool countdownStarted;
    int minutes;
    float seconds;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        playerNameText.text = playerScript.player_name;
        countdownText.text = "Repair your armor";
        countdownSlider.maxValue = adjustableTime;
        countdownSlider.value = countdownSlider.maxValue;
        countdowntime = adjustableTime;
        defaultTime = countdowntime;
    }

    // Update is called once per frame
    private void Update()
    {
        playerGoldText.text = playerScript.gold.ToString();
        playerToolsText.text = playerScript.tools.ToString();
        goldprice = playerScript.maxArmor - playerScript.currentArmor;
        goldPriceText.text = goldprice.ToString();
        if (countdownStarted == true && countdowntime > 0)
        {            
            countdowntime -= Time.deltaTime;
            minutes = Mathf.FloorToInt(countdowntime / 60);
            seconds = countdowntime - (minutes * 60);
            countdownSlider.value = defaultTime - countdowntime;
            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);            
        }
        if (countdowntime <= 0)
        {
            countdownStarted = false;
            countdowntime = defaultTime;
            playerScript.currentArmor = playerScript.maxArmor;
            countdownText.text = "DONE!";
        }
        if(playerScript.currentArmor == playerScript.maxArmor)
        {
            foreach (Button x in buttons)
            {
                x.interactable = false;
            }
        }
        else
        {
            foreach (Button x in buttons)
            {
                x.interactable = true;
            }
        }
    }
    public void CanvasEnabled(bool enabled)
    {
        canvas.enabled = enabled;
    }
    public void Repair(int currencyID)
    {        
        if (currencyID == 0) // Gold price
        {
            if(playerScript.gold >= goldprice) // If there is enough funds
            {
                countdowntime = Mathf.Abs(playerScript.currentArmor - playerScript.maxArmor);
                defaultTime = countdowntime;
                countdownSlider.maxValue = countdowntime;
                playerScript.gold -= goldprice;
                countdownStarted = true;
                buttons[0].interactable = false;
                // Set button to inactive state

            }
            if(playerScript.gold < goldprice) // If not enough gold
            {
                countdownText.text = "Not enough gold";
            }
        }
        if (currencyID == 1) // Tools price
        {
            if (playerScript.tools >= 1) // If there is enough funds
            {
                playerScript.tools--;
                //Repair armor
                countdownSlider.value = countdownSlider.maxValue;
                countdownStarted = false;
                playerScript.currentArmor = playerScript.maxArmor;
                countdownText.text = "DONE!";
            }
            if (playerScript.tools < 1) // If not enough tools
            {
                countdownText.text = "Not enough tools";
            }
        }
    }
}
