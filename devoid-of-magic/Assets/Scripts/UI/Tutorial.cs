using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Player player;
    [SerializeField] GameObject[] tuts;
    int currentTut;
    private void Start()
    {
        panel.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(ActivatePanel());
        currentTut = 0;
    }
    private void Update()
    {
        if (currentTut > tuts.Length)
        {
            panel.SetActive(false);
        }
    }

    IEnumerator ActivatePanel()
    {
        yield return new WaitForSeconds(0.5f);
        if (player.level == 0)
        {
            panel.SetActive(true);
            tuts[0].SetActive(true);
        }
        else if (player.level > 0)
        {
            panel.SetActive(false);
        }
        yield break;
    }

    public void NextButton()
    {
        currentTut++;
        foreach (GameObject tut in tuts)
        {
            tut.SetActive(false);
        }        
        tuts[currentTut].SetActive(true);        
    }
}
