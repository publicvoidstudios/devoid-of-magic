using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCounter : MonoBehaviour
{
    private Player playerScript;
    private int startGold;
    private int currentGold;
    [SerializeField]
    public int earnedGold;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(DelayedStart());
    }

    // Update is called once per frame
    void Update()
    {
        currentGold = playerScript.gold;
        earnedGold = currentGold - startGold;
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1f);        
        startGold = playerScript.gold;
    }
}
