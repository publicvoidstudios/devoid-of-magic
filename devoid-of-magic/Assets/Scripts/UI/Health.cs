using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
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
        text.text = player.currentHealth.ToString() + "/" + player.maxHealth.ToString();
        if (player.currentHealth < 0)
        {
            text.text = "0" + "/" + player.maxHealth;
        }
    }
}
