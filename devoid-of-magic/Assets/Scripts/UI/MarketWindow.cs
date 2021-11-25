using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketWindow : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    Ability_Switcher_UI absw;
    [SerializeField]
    TMP_Text lotNameText;
    [SerializeField]
    TMP_Text lotDescriptionText;
    [SerializeField]
    TMP_Text YWGText;
    [SerializeField]
    TMP_Text goldPriceText;
    [SerializeField]
    TMP_Text playerNameText;
    [SerializeField]
    TMP_Text playerGoldText;
    [SerializeField]
    TMP_Text playerToolsText;
    [SerializeField]
    GameObject[] buttons;
    [SerializeField]
    GameObject player;
    Player playerScript;
    [SerializeField]
    int priceGold;
    [SerializeField]
    GameObject goldImage;
    [SerializeField]
    GameObject warning;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        playerNameText.text = playerScript.player_name;
    }

    // Update is called once per frame
    private void Update()
    {
        playerGoldText.text = playerScript.gold.ToString();
        playerToolsText.text = playerScript.tools.ToString();
        if (absw.currentAbility == 0) // 1 tool
        {
            lotNameText.text = "Tool";
            lotDescriptionText.text = "A simple tool, which is used to repair broken armor. Bring it to Smithy the Blacksmith and he will bring new life in your armor. Unfortunately this is not sturdy tool, so it will break after use.";
            YWGText.text = "You will get: 1 Tool";
            goldPriceText.text = "Watch 1 AD";
            goldImage.SetActive(false);
            buttons[0].SetActive(true);
            buttons[1].SetActive(false);

        }
        if (absw.currentAbility == 1) // Tool pack
        {
            lotNameText.text = "Tool pack";
            lotDescriptionText.text = "A bunch of simple tools, which are used to repair broken armor. Bring it to Smithy the Blacksmith and he will bring new life in your armor. Unfortunately those are not sturdy tools, so each one of them will break after use.";
            YWGText.text = "You will get: 10 Tools";
            goldPriceText.text = priceGold.ToString();
            goldImage.SetActive(true);            
            buttons[0].SetActive(false);
            buttons[1].SetActive(true);
        }        
    }

    public void SaveChanges()
    {
        playerScript.Save();
    }

    public void CanvasEnabled(bool enabled)
    {
        canvas.enabled = enabled;
    }
    public void Buy()
    {
        if (playerScript.gold >= priceGold)
        {
            playerScript.gold -= priceGold;
            playerScript.tools += 10;
        }
        else
        {
            warning.SetActive(true);
        }
    }
}
