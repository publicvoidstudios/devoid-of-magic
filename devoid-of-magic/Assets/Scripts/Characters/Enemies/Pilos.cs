using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilos : MonoBehaviour
{
    [SerializeField] public float speed;
    Vector2 spawnPoint;
    Vector2 currentPoint;
    Vector2 targetPoint;
    Transform player;
    public Player target;
    [SerializeField] public int damage;
    Vector2 targetDir;
    Vector2 forward;
    float angle;
    public float distTravelled;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPoint = player.position;
        targetDir = player.transform.position - transform.position;
        forward = transform.right;
        targetDir.y += 1f;
        targetPoint.y += 1f;
    }

    // Update is called once per frame
    void Update()
    {
        angle = Vector2.SignedAngle(targetDir, forward);
        currentPoint = transform.position;
        distTravelled = Mathf.Abs(spawnPoint.x) - Mathf.Abs(currentPoint.x);
        if (spawnPoint.x > player.position.x)
        {
            //transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
            if (angle != 180)
            {
                transform.rotation = Quaternion.Euler(0, 0, -angle);
            }
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
        }
        if (spawnPoint.x < player.position.x)
        {
            //transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            if (angle != 180)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180 - angle);
            }
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
        }
        if(currentPoint == targetPoint)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !target.isDead)
        {
            AudioManager audio = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
            audio.Play("ArrowHit");
            target.DamageRecount(damage + Random.Range(-2, 3));
            Destroy(gameObject);
        }
    }
}
