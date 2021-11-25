using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class LightSphere : MonoBehaviour
{
    GameObject player;
    Player playerScript;
    Light2D pointLight;
    Vector2 playerPosition;
    float radius = 1f, angularSpeed;
    float positionX, positionY, angle;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        pointLight = GetComponentInChildren<Light2D>();
    }
    void Update()
    {
        pointLight.intensity = 0.5f + playerScript.magicDamage * 0.1f;
        pointLight.pointLightOuterRadius = 2f + playerScript.magicDamage * 0.1f;
        angularSpeed = 1 + playerScript.magicDamage * 0.1f;
        playerPosition.x = player.transform.position.x;
        playerPosition.y = player.transform.position.y;
        positionX = playerPosition.x + Mathf.Cos(angle) * radius;
        positionY = playerPosition.y + Mathf.Sin(angle) * radius;
        transform.position = new Vector2(positionX, positionY);
        angle = angle + Time.deltaTime * angularSpeed;
        if(angle >= 360f)
        {
            angle = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.GetDamage(playerScript.magicDamage * Random.Range(1, 4));
        }
    }
}
