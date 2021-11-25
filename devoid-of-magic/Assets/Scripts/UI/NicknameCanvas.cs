using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NicknameCanvas : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Player player;
    [SerializeField] TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        if (player.level == 0 && player.progress == 0)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName()
    {
        player.player_name = inputField.text;
        player.Save();
        panel.SetActive(false);
    }
}
