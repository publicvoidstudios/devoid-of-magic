using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDHandler : MonoBehaviour
{
    [SerializeField]
    Animator HUDAnimator;
    [SerializeField] Trainer trainerScript;
    [SerializeField] Blacksmith blacksmithScript;
    [SerializeField] Trader traderScript;
    [SerializeField] Questor[] questorScript;
    Player player;
    [SerializeField] GameObject map;
    [SerializeField] TMP_Text charName;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        charName.text = player.player_name;
        if (trainerScript.upgradesActivated == true || blacksmithScript.upgradesActivated == true || traderScript.upgradesActivated == true || questorScript[0].upgradesActivated || questorScript[1].upgradesActivated || questorScript[2].upgradesActivated)
        {
            HUDAnimator.SetBool("UpgradesActive", true);
        }
        if (trainerScript.upgradesActivated == false && blacksmithScript.upgradesActivated == false && traderScript.upgradesActivated == false && !questorScript[0].upgradesActivated && !questorScript[1].upgradesActivated && !questorScript[2].upgradesActivated)
        {
            HUDAnimator.SetBool("UpgradesActive", false);
        }


        if (player.activeQuest)
        {
            map.SetActive(true);
        }
        else
        {
            map.SetActive(false);
        }
    }
}
