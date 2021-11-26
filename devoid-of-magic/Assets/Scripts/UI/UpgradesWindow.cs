using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesWindow : MonoBehaviour
{
    [SerializeField] 
    Canvas canvas;
    [SerializeField]
    Ability_Switcher_UI absw;
    [SerializeField]
    TMP_Text abilityNameText;
    [SerializeField]
    TMP_Text abilityDescriptionText;
    [SerializeField]
    TMP_Text abilityLevelText;
    [SerializeField]
    TMP_Text goldPriceText;
    [SerializeField]
    TMP_Text currentStatsText;
    [SerializeField]
    TMP_Text nextStatsText;
    [SerializeField]
    TMP_Text playerNameText;
    [SerializeField]
    TMP_Text playerGoldText;
    [SerializeField]
    GameObject player;
    Player playerScript;
    PlayerCombat combatScript;
    public int current;
    public int next;
    public int basicPrice = 100;
    public int totalPrice;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        combatScript = player.GetComponent<PlayerCombat>();        
    }

    // Update is called once per frame
    private void Update()
    {
        playerNameText.text = playerScript.player_name;
        playerGoldText.text = playerScript.gold.ToString();
        if (absw.currentAbility == 0)
        {
            abilityNameText.text = "Armor";
            abilityDescriptionText.text = "Armor get's twice reduced damage, this can save your life!";
            abilityLevelText.text = playerScript.armorLevel.ToString();
            totalPrice = basicPrice * playerScript.armorLevel;
            goldPriceText.text = totalPrice.ToString();
            current = playerScript.maxArmor;
            currentStatsText.text = current.ToString();
            next = current + 4;
            nextStatsText.text = next.ToString();
        }
        if (absw.currentAbility == 1)
        {
            abilityNameText.text = "Axe Damage";
            abilityDescriptionText.text = "Slash you'r enemies with fearsome melee weapon";
            abilityLevelText.text = playerScript.axeLevel.ToString();
            totalPrice = basicPrice * playerScript.axeLevel;
            goldPriceText.text = totalPrice.ToString();
            current = combatScript.damageMelee + (playerScript.axeLevel * 3);
            currentStatsText.text = current.ToString();
            next = current + 3;
            nextStatsText.text = next.ToString();
            
        }
        if (absw.currentAbility == 2)
        {
            abilityNameText.text = "Bow Damage";
            abilityDescriptionText.text = "Keep shooting, don't even let them get close to you!";
            abilityLevelText.text = playerScript.bowLevel.ToString();
            totalPrice = basicPrice * playerScript.bowLevel;
            goldPriceText.text = totalPrice.ToString();
            current = combatScript.damageRanged + (playerScript.bowLevel * 2);
            currentStatsText.text = current.ToString();
            next = current + 2;
            nextStatsText.text = next.ToString();
        }        
        if (absw.currentAbility == 3)
        {
            abilityNameText.text = "Health";
            abilityDescriptionText.text = "If your armor is broken, there is only you left";
            abilityLevelText.text = playerScript.strengthLevel.ToString();
            totalPrice = basicPrice * playerScript.strengthLevel;
            goldPriceText.text = totalPrice.ToString();
            current = playerScript.maxHealth;
            currentStatsText.text = current.ToString();
            next = current + 6;
            nextStatsText.text = next.ToString();
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
    public void Upgrade()
    {        
        if (absw.currentAbility == 1)
        {
            if(playerScript.gold >= totalPrice)
            {
                playerScript.gold -= totalPrice;
                playerScript.axeLevel++;
            }
            if(playerScript.gold < totalPrice)
            {
                Debug.Log("Insufficient gold");
            }
        }
        if (absw.currentAbility == 2)
        {
            if (playerScript.gold >= totalPrice)
            {
                playerScript.gold -= totalPrice;
                playerScript.bowLevel++;
            }
            if (playerScript.gold < totalPrice)
            {
                Debug.Log("Insufficient gold");
            }
        }
        if (absw.currentAbility == 0)
        {
            if (playerScript.gold >= totalPrice)
            {
                playerScript.gold -= totalPrice;
                ++playerScript.armorLevel;
                playerScript.currentArmor += 4;
            }
            if (playerScript.gold < totalPrice)
            {
                Debug.Log("Insufficient gold");
            }
        }
        if (absw.currentAbility == 3)
        {
            if (playerScript.gold >= totalPrice)
            {
                playerScript.gold -= totalPrice;
                playerScript.strengthLevel++;
            }
            if (playerScript.gold < totalPrice)
            {
                Debug.Log("Insufficient gold");
            }
        }
    }
}
