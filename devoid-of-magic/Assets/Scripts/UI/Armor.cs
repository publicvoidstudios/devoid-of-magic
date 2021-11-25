using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Armor : MonoBehaviour
{
    public Slider slider;
    public TMP_Text text;
    public Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void SetMaxAmount(int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }
    public void SetAmount(int value)
    {
        slider.value = value;
        text.text = player.currentArmor + "/" + player.maxArmor;
    }
}