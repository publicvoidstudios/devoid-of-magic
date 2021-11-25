using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stars : MonoBehaviour
{
    public GameObject[] buttons;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject b in buttons)
        {
            GameObject activeStar = b.transform.Find("DoneStar").gameObject;
            activeStar.SetActive(false);
            Button button = b.GetComponent<Button>();
            button.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.progress < 10)
        {
            GameObject star = buttons[player.progress].transform.Find("DoneStar").gameObject;
            star.SetActive(true);
            for (int i = 0; i < player.progress + 1; i++)
            {
                Button b = buttons[i].GetComponent<Button>();
                b.interactable = true;
            }
        }
        else
        {
            foreach(GameObject go in buttons)
            {
                Button b = go.GetComponent<Button>();
                b.interactable = true;
            }
        }
    }
}
