using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int sublevel;
    public int progress;
    public int axeLevel;
    public int bowLevel;
    public int armorLevel;
    public int strengthLevel;
    public int gold;
    public string player_name;
    public int tools;
    public int currentArmor;
    public int magicDamage;
    public bool tutorial_complete;

    public PlayerData(Player player)
    {
        level = player.level;
        sublevel = player.sublevel;
        progress = player.progress;
        axeLevel = player.axeLevel;
        bowLevel = player.bowLevel;
        armorLevel = player.armorLevel;
        strengthLevel = player.strengthLevel;
        gold = player.gold;
        player_name = player.player_name;
        tools = player.tools;
        currentArmor = player.currentArmor;
        magicDamage = player.magicDamage;
        tutorial_complete = player.tutorial_complete;
    }
}
