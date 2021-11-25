using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Player player;
    private void Start()
    {
        panel.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(ActivatePanel());
    }

    IEnumerator ActivatePanel()
    {
        yield return new WaitForSeconds(0.5f);
        if (player.level == 0)
        {
            panel.SetActive(true);
        }
        else if (player.level > 0)
        {
            panel.SetActive(false);
        }
        yield break;
    }
}
