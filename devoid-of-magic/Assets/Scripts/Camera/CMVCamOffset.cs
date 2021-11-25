using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMVCamOffset : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera vcam;
    [SerializeField]
    Player playerScript;
    [SerializeField]
    GameObject player;
    [SerializeField]
    PlayerCombat combat;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player = GameObject.FindGameObjectWithTag("Player");
        combat = player.GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(combat.nearest != null) //Enemy is somewhere on the scene
        {
            if (Mathf.Abs(Vector2.Distance(player.transform.position, combat.nearest.transform.position)) > playerScript.range) //No enemies in range
            {
                if (playerScript.lookingRight && player.transform.position.y < 0)
                {
                    OffsetRight();
                }
                if (!playerScript.lookingRight && player.transform.position.y < 0)
                {
                    OffsetLeft();
                }
                if (playerScript.lookingRight && player.transform.position.y > 0)
                {
                    OffsetDownRight();
                }
                if (!playerScript.lookingRight && player.transform.position.y > 0)
                {
                    OffsetDownLeft();
                }
            }
            else //Enemies in range
            {
                OffsetEnemy();
            }
        }
        else //No enemies on the scene
        {
            if (playerScript.lookingRight && player.transform.position.y < 0)
            {
                OffsetRight();
            }
            if (!playerScript.lookingRight && player.transform.position.y < 0)
            {
                OffsetLeft();
            }
            if (playerScript.lookingRight && player.transform.position.y > 0)
            {
                OffsetDownRight();
            }
            if (!playerScript.lookingRight && player.transform.position.y > 0)
            {
                OffsetDownLeft();
            }
        }
    }

    private void OffsetDownRight()
    {
        var transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, new Vector3(1.6f, -0f, -10f), .02f);
    }

    private void OffsetDownLeft()
    {
        var transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, new Vector3(-1.6f, -0f, -10f), .02f);
    }
    private void OffsetEnemy()
    {
        var transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        if(player.transform.position.x > combat.nearest.transform.position.x)
        {
            transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, new Vector3(-1.6f, 2f, -10f), .02f);
        }
        else
        {
            transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, new Vector3(1.6f, 2f, -10f), .02f);
        }
        
    }
    private void OffsetRight()
    {
        var transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, new Vector3(1.6f, 2f, -10f), .02f);
    }
    private void OffsetLeft()
    {
        var transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, new Vector3(-1.6f, 2f, -10f), .02f);
    }
}
