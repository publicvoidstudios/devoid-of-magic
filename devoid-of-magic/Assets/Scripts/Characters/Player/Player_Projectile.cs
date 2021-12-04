using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Projectile : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float gravity;
    Vector2 playerPoint;
    Vector2 spawnPoint;
    Vector2 startingPoint;
    Vector2 currentPoint;
    public Transform player;
    public Enemy enemyScript;
    public int projectile_damage;
    public GameObject parent;
    public Player playerScript;
    public Boss bossScript;
    public GameObject[] enemiesArr;
    public GameObject nearest;
    public Vector2 target;
    public float angle;
    public Vector2 targetDir;
    public Vector2 forward;
    public float distTravelled;
    // Start is called before the first frame update
    void Start()
    {
        enemiesArr = GameObject.FindGameObjectsWithTag("Enemy");
        FindNearest();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startingPoint = transform.position;
        parent = GameObject.FindGameObjectWithTag("Player");        
        playerScript = parent.GetComponentInChildren<Player>();
        playerPoint = player.position;
        spawnPoint = playerScript.arrowSpawnPoint.position;
        target = new Vector2(nearest.transform.position.x, nearest.transform.position.y);
        targetDir = nearest.transform.position - transform.position;
        forward = transform.right;
        angle = Vector2.SignedAngle(targetDir, forward);
        Debug.Log(angle);
        Invoke("SelfDestruct", 2f);
    }

    GameObject FindNearest()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        if(enemiesArr.Length > 0)
        {
            foreach (GameObject e in enemiesArr)
            {
                Vector3 dif = e.transform.position - position;
                float currDist = dif.sqrMagnitude;
                if (currDist < distance)
                {
                    nearest = e;
                    distance = currDist;
                }
            }
        }
        else
        {
            nearest = gameObject;
            nearest.transform.position = new Vector3(nearest.transform.position.x + 10, nearest.transform.position.y);
        }
        return nearest;
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {        
        angle = Vector2.SignedAngle(targetDir, forward);
        currentPoint = transform.position;
        distTravelled = Mathf.Abs(startingPoint.x - currentPoint.x);
        if (spawnPoint.x > playerPoint.x)
        {
            //transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
            if (angle != 180)
            {
                transform.rotation = Quaternion.Euler(0, 0, -angle);
            }
            transform.position = Vector2.MoveTowards(transform.position, nearest.transform.position, speed * Time.deltaTime);
        }
        if (spawnPoint.x < playerPoint.x)
        {
            //transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            if (angle != 180)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180 - angle);
            }
            transform.position = Vector2.MoveTowards(transform.position, nearest.transform.position, speed * Time.deltaTime);
        }

        if (distTravelled > playerScript.range || nearest == null)
        {
            Destroy(gameObject);
        }


    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            AudioManager audio = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
            audio.Play("ArrowHit");
            enemyScript = other.gameObject.GetComponent<Enemy>();
            if(enemyScript != null)
            {
                if (enemyScript.alive)
                {
                    Debug.Log(other.name + " damaged using BOW by " + projectile_damage);
                    enemyScript.GetDamage(projectile_damage);
                    Destroy(gameObject);
                }
            }
            bossScript = other.gameObject.GetComponent<Boss>();
            if (bossScript != null)
            {
                if (bossScript.alive)
                {
                    bossScript.GetDamage(projectile_damage);
                    Destroy(gameObject);
                }
            }
        }
        else if(!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
