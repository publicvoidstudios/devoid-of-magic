using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Crow[] birds;
    float randY;
    Vector2 whereToSpawn;
    float nextSpawn = 0.0f;
    [SerializeField]
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + Random.Range(5, 20);
            randY = Random.Range(player.position.y + 1f,player.position.y + 2.86f);
            whereToSpawn = new Vector2(transform.position.x, randY);
            Instantiate(birds[Random.Range(0, birds.Length)], whereToSpawn, Quaternion.identity);
        }
    }
}
