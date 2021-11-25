using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MagicUpgrades : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;
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
    public int current;
    public int next;
    public int basicPrice = 100;
    public int totalPrice;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        playerNameText.text = playerScript.player_name;
        playerGoldText.text = playerScript.gold.ToString();
        abilityLevelText.text = playerScript.magicDamage.ToString();
        totalPrice = basicPrice * playerScript.magicDamage;
        goldPriceText.text = totalPrice.ToString();
        current = playerScript.magicDamage;
        currentStatsText.text = current.ToString();
        next = current + 1;
        nextStatsText.text = next.ToString();
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
        if (playerScript.gold >= totalPrice)
        {
            playerScript.gold -= totalPrice;
            playerScript.magicDamage++;
        }
        if (playerScript.gold < totalPrice)
        {
            Debug.Log("Insufficient gold");
        }        
    }

    public void VideoUpgrade()
    {
        Debug.Log("Interstitial AD will be played.");
    }
}
