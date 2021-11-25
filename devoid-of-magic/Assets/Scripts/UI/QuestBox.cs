using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestBox : MonoBehaviour
{
    [SerializeField]
    GameObject obj1;
    [SerializeField]
    TMP_Text task1;
    [SerializeField]
    GameObject check1;
    public bool obj1Complete;
    Player playerScript;
    [SerializeField]
    GoalDetector gd;
    [SerializeField]
    GoldCounter counter;
    // Start is called before the first frame update
    void Start()
    {
        gd = GameObject.FindGameObjectWithTag("GD").GetComponent<GoalDetector>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        counter = GameObject.FindGameObjectWithTag("Counter").GetComponent<GoldCounter>();
        if(playerScript.level == 1)
        {
            obj1.SetActive(true);
            obj1Complete = false;
        }
        if(playerScript.level == 3)
        {
            obj1.SetActive(true);
        }
        if(playerScript.level == 5)
        {
            obj1.SetActive(true);
        }
        if(playerScript.level == 7)
        {
            obj1.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript.progress < gd.levelID) //First
        {
            if (playerScript.level == 1)
            {
                if (playerScript.sublevel == 1)
                {
                    if (gd.goalKills >= gd.killedEnemies)
                    {
                        task1.text = "Pirates defeated " + gd.killedEnemies + "/" + gd.goalKills;
                    }
                    if (gd.goalKills == gd.killedEnemies)
                    {
                        check1.SetActive(true);
                        obj1Complete = true;
                    }
                }
                if (playerScript.sublevel == 2)
                {
                    if (gd.goalKills >= gd.killedEnemies)
                    {
                        task1.text = "Pirates defeated " + gd.killedEnemies + "/" + gd.goalKills;
                    }
                    if (gd.goalKills == gd.killedEnemies)
                    {
                        check1.SetActive(true);
                        obj1Complete = true;
                    }
                }
                if (playerScript.sublevel == 3)
                {
                    if (gd.goalKills >= gd.killedEnemies)
                    {
                        task1.text = "Defeat Bubarossa";
                    }
                    if (gd.goalKills == gd.killedEnemies)
                    {
                        check1.SetActive(true);
                        obj1Complete = true;
                    }
                }

            }
            if (playerScript.level == 3)
            {
                if (playerScript.sublevel == 1)
                {
                    if (gd.goalKills >= gd.killedEnemies)
                    {
                        task1.text = "Snow people defeated " + gd.killedEnemies + "/" + gd.goalKills;
                    }
                    if (gd.goalKills == gd.killedEnemies)
                    {
                        check1.SetActive(true);
                        obj1Complete = true;
                    }
                }
                if (playerScript.sublevel == 2)
                {
                    if (gd.goalKills >= gd.killedEnemies)
                    {
                        task1.text = "Snow people defeated " + gd.killedEnemies + "/" + gd.goalKills;
                    }
                    if (gd.goalKills == gd.killedEnemies)
                    {
                        check1.SetActive(true);
                        obj1Complete = true;
                    }
                }
                if (playerScript.sublevel == 3)
                {
                    if (gd.goalKills >= gd.killedEnemies)
                    {
                        task1.text = "Defeat Polar Ursa";
                    }
                    if (gd.goalKills == gd.killedEnemies)
                    {
                        check1.SetActive(true);
                        obj1Complete = true;
                    }
                }
            }
            if (playerScript.level == 5)
            {
                if (playerScript.sublevel == 1)
                {
                    if (gd.goalKills >= gd.killedEnemies)
                    {
                        task1.text = "Demons defeated " + gd.killedEnemies + "/" + gd.goalKills;
                    }
                    if (gd.goalKills == gd.killedEnemies)
                    {
                        check1.SetActive(true);
                        obj1Complete = true;
                    }
                }
                if (playerScript.sublevel == 2)
                {
                    if (gd.goalKills >= gd.killedEnemies)
                    {
                        task1.text = "Demons defeated " + gd.killedEnemies + "/" + gd.goalKills;
                    }
                    if (gd.goalKills == gd.killedEnemies)
                    {
                        check1.SetActive(true);
                        obj1Complete = true;
                    }
                }
                if (playerScript.sublevel == 3)
                {
                    if (gd.goalKills >= gd.killedEnemies)
                    {
                        task1.text = "Defeat Demon Lord";
                    }
                    if (gd.goalKills == gd.killedEnemies)
                    {
                        check1.SetActive(true);
                        obj1Complete = true;
                    }
                }
            }
            if (playerScript.level == 7)
            {
            }
        }
        if(playerScript.progress >= gd.levelID) //Reentering
        {
            if (gd.bossLevel)
            {
                if (gd.goalKills >= gd.killedEnemies)
                {
                    task1.text = "Kill the boss, again...";
                }
                if (gd.goalKills == gd.killedEnemies)
                {
                    check1.SetActive(true);
                    obj1Complete = true;
                }
            }
            else
            {
                if (gd.goalKills >= gd.killedEnemies)
                {
                    task1.text = "Survive! Gold looted this far: " + counter.earnedGold;
                }
                if (gd.goalKills == gd.killedEnemies)
                {
                    check1.SetActive(true);
                    obj1Complete = true;
                }
            }            
        }
        
    }
}
