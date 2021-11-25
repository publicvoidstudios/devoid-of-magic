using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalDetector : MonoBehaviour
{
    [SerializeField]
    public int levelID;
    [SerializeField]
    public bool bossLevel;
    public int goalKills;

    public int killedEnemies;

    public bool savedChild;

    public bool foundOrb;

    public bool levelComplete;

    Player player;
    [SerializeField]
    GameObject panel;
    [SerializeField]
    TMP_Text description;
    [SerializeField]
    TMP_Text rewardAmount;
    [SerializeField]
    GameObject[] rewardImages;
    private int reward;
    private int rewardTools;
    

    void Start()
    {
        reward = 0;
        rewardTools = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        levelComplete = false;        
    }

    void Update()
    {
        if (player.progress < levelID) // First visit
        {
            if (player.level == 1)
            {
                if (player.sublevel == 1)
                {
                    goalKills = 20;
                }
                if (player.sublevel == 2)
                {
                    goalKills = 30;
                }
                if (player.sublevel == 3)
                {
                    goalKills = 1;
                }
            }
            if (player.level == 3)
            {
                if (player.sublevel == 1)
                {
                    goalKills = 20;
                }
                if (player.sublevel == 2)
                {
                    goalKills = 30;
                }
                if (player.sublevel == 3)
                {
                    goalKills = 1;
                }
            }
            if (player.level == 5)
            {
                if (player.sublevel == 1)
                {
                    goalKills = 20;
                }
                if (player.sublevel == 2)
                {
                    goalKills = 30;
                }
                if (player.sublevel == 3)
                {
                    goalKills = 1;
                }
            }
        }
        if (player.progress >= levelID) //Reentering level
        {
            if (!bossLevel)
            {
                goalKills = 999999999;
            }
            if (bossLevel)
            {
                goalKills = 1;
            }
        }

        if (player.progress < levelID) //First
        {
            if (player.level == 1 && goalKills == killedEnemies)
            {
                levelComplete = true;
            }
            if (player.level == 3 && goalKills <= killedEnemies)
            {
                if (player.sublevel == 1 || player.sublevel == 2)
                {
                    levelComplete = true;
                }
                if (player.sublevel == 3 && savedChild && foundOrb)
                {
                    levelComplete = true;
                }
            }
            if (player.level == 5 && goalKills == killedEnemies)
            {
                if (player.sublevel == 1 || player.sublevel == 2)
                {
                    levelComplete = true;
                }
            }
            if (levelComplete)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    Enemy enemyScr = enemy.GetComponent<Enemy>();
                    enemyScr.DestroyEndGame();
                }
                if (player.level == 1)
                {
                    panel.SetActive(true);
                    if (player.sublevel == 1)
                    {
                        description.text = "You have shaken their self-confidence, but that is unlikely to stop them. You have a difficult task ahead of you. Their leader is behind the pirate attacks. Bubarossa, that's what they call him. It will be possible to get to him only at night. Then you have to strike. Return to the village and prepare properly, the fight will not be easy.";
                        rewardImages[0].SetActive(true);
                        rewardImages[1].SetActive(false);
                        rewardAmount.text = "1000";
                        reward = 1000;
                    }
                    if (player.sublevel == 2)
                    {
                        description.text = "The path is clear, now you can get close to Bubarossa. Sharpen your ax sharper, and put on better armor. And if you are not confident in your abilities, a bow will be a good decision. Although I don't think a pirate doesn't have a pistol in his bosom. In any case, come back, you have done well.";
                        rewardImages[0].SetActive(true);
                        rewardImages[1].SetActive(false);
                        rewardAmount.text = "2000";
                        reward = 2000;
                    }
                    if (player.sublevel == 3)
                    {
                        description.text = "Bubarossa is dead. Everyone gets what they deserve. And we are infinitely grateful to you for our salvation. Your well-deserved reward awaits you in the village. As we are.";
                        rewardImages[0].SetActive(true);
                        rewardImages[1].SetActive(false);
                        rewardAmount.text = "3000";
                        reward = 3000;
                    }

                }
                if (player.level == 3)
                {
                    panel.SetActive(true);
                    if (player.sublevel == 1)
                    {
                        description.text = "You haven't found the child yet you should keep searching. Go back to the village and start over.";
                        rewardImages[0].SetActive(false);
                        rewardImages[1].SetActive(true);
                        rewardAmount.text = "1";
                        rewardTools = 1;
                    }
                    if (player.sublevel == 2)
                    {
                        description.text = "The child has to be somewhere close. Keep searching, and be careful, the worst thing can happen is that the child was captured by snow peoples God. Polar Ursa.";
                        rewardImages[0].SetActive(false);
                        rewardImages[1].SetActive(true);
                        rewardAmount.text = "2";
                        rewardTools = 2;
                    }
                    if (player.sublevel == 3)
                    {
                        description.text = "Our fears just confirmed. Luckyly you were there and had enough strength to defeat this monster. The child is saved, bring it home, and this orb... You need to figure out what it is for.";
                        rewardImages[0].SetActive(false);
                        rewardImages[1].SetActive(true);
                        rewardAmount.text = "3";
                        rewardTools = 3;
                    }
                }
                if (player.level == 5)
                {
                    panel.SetActive(true);
                    if (player.sublevel == 1)
                    {
                        description.text = "Those were just scouts. More of them will come soon. Unless you will behead them.";
                        rewardImages[0].SetActive(true);
                        rewardImages[1].SetActive(false);
                        rewardAmount.text = "500";
                        reward = 500;
                    }
                    if (player.sublevel == 2)
                    {
                        description.text = "Wizard just found a way to stop this. Your next target will be a demon lord himself. I hope you will survive. This village depends on you now.";
                        rewardImages[0].SetActive(true);
                        rewardImages[1].SetActive(false);
                        rewardAmount.text = "2000";
                        reward = 2000;
                    }
                    if (player.sublevel == 3)
                    {
                        description.text = "I can't beleive this. You did it. This is the end. We are safe. Now you can finally rest. And if you wish you can pay remaining demons a visit in an endless battle between good and evil. As a true warrior.";
                        rewardImages[0].SetActive(true);
                        rewardImages[1].SetActive(false);
                        rewardAmount.text = "5000";
                        reward = 5000;
                    }
                }
            }
            else
            {
                panel.SetActive(false);
            }
        }
        if (player.progress >= levelID) //Reentering
        {
            if (goalKills == killedEnemies && levelID != 6 && levelID != 10)
            {
                levelComplete = true;
            }
            if (levelID == 6 && goalKills <= killedEnemies && savedChild && foundOrb)
            {
                levelComplete = true;
            }
            if (levelComplete)
            {
                panel.SetActive(true);
                description.text = "I really hope you enjoy beating those who are already weaker than you. Please feel free doing this again and again. You dirty bully.";
                rewardImages[0].SetActive(true);
                rewardImages[1].SetActive(false);
                rewardAmount.text = "500";
                reward = 500;
            }
        }
    }

    public void RaiseSublevel()
    {
        if(player.progress < levelID) //First visit
        {
            player.gold += reward;
            player.tools += rewardTools;
            ++player.sublevel;
            if (player.sublevel == 4)
            {
                player.level++;
                player.sublevel = 0;
            }
            player.progress = levelID;
            player.Save();
            killedEnemies = 0;
            levelComplete = false;
            panel.SetActive(false);
            return;
        }
        if(player.progress >= levelID) //Reentering level
        {
            player.gold += reward;
            player.tools += rewardTools;            
            player.Save();
            killedEnemies = 0;
            levelComplete = false;
            panel.SetActive(false);
            return;
        }
    }


}
