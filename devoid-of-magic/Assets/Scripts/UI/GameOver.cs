using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{

    public GameObject gameOverPanel;
    [SerializeField]
    TMP_Text youEarned;
    GoldCounter counter;
    private void Start()
    {
        counter = GameObject.FindGameObjectWithTag("Counter").GetComponent<GoldCounter>();
    }
    private void Update()
    {
        youEarned.text = "Gold earned: " + counter.earnedGold.ToString();
    }
    public void Again()
    {
        AudioManager audio = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
        audio.Play("Click");
        LevelLoader levelLoader = GameObject.FindGameObjectWithTag("LvlLoader").GetComponent<LevelLoader>();
        levelLoader.LoadLevel(1);
    }
    public void Pussy()
    {
        Application.Quit();
    }
}
