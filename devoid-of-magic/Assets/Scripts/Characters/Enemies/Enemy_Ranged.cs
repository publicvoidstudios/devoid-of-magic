using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ranged : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public float speed;
    public float distanceOfPatrol;
    public float enemyPlayerDistance;
    public float rangedDistance;
    bool isMovingRight;
    public Transform player;
    public float stopDistance;
    bool patrol = false;
    bool attack = false;
    bool regroup = false;
    public Vector2 spawnPosition;
    public GameObject projectile;
    public Transform projectileStartingPoint;
    private bool fireCooldown;
    private GameObject target;
    public Player playerScript;
    Enemy commonScript;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        commonScript = GetComponent<Enemy>();
        target = GameObject.FindGameObjectWithTag("Player");
        playerScript = target.GetComponentInChildren<Player>();
        stopDistance = playerScript.range;
        spawnPosition = transform.position;
        player = target.transform;
        StartCoroutine(FireCooldown());
    }
    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
    void Update()
    {
        enemyPlayerDistance = transform.position.x - player.position.x;        
        //Patrolling Stance
        if (Vector2.Distance(transform.position, spawnPosition) < distanceOfPatrol && attack == false && commonScript.alive)
        {
            patrol = true;
        }
        //Attacking Stance
        if (Vector2.Distance(transform.position, player.position) < stopDistance && commonScript.alive)
        {
            attack = true;
            patrol = false;
            regroup = false;
        }
        //Regrouping Stance
        if (Vector2.Distance(transform.position, player.position) > stopDistance && commonScript.alive)
        {
            regroup = true;
            attack = false;
        }
        if (patrol == true && commonScript.alive)
        {
            Patrol();
        }
        else if (attack == true && commonScript.alive)
        {
            Attack();
        }
        else if (regroup == true && commonScript.alive)
        {
            Regroup();
        }
        if (!commonScript.alive)
        {
            DeathAnim();
        }
    }
    private void DeathAnim()
    {
        anim.SetBool("Dead", true);
        State = States.dead;
    }
    void Patrol()
    {
        speed = 1;
        if (transform.position.x > spawnPosition.x + distanceOfPatrol)
        {
            isMovingRight = false;
        }
        else if (transform.position.x < spawnPosition.x - distanceOfPatrol)
        {
            isMovingRight = true;
        }
        if (isMovingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
            State = States.walk;
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            State = States.walk;
        }    
    }
    void Attack()
    {
        if (Mathf.Abs(enemyPlayerDistance) < rangedDistance && !playerScript.isDead)
        {
            speed = 0;
            State = States.attack;
            MakeAShot();
            if (transform.position.x >= player.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (transform.position.x <= player.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (Mathf.Abs(enemyPlayerDistance) > rangedDistance && !playerScript.isDead)
        {
            if (transform.position.x >= player.position.x)
            {
                EngagePlayer();
                State = States.walk;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (transform.position.x <= player.position.x)
            {
                EngagePlayer();
                State = States.walk;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        if (playerScript.isDead)
        {
            speed = 0;
            State = States.idle;
        }
    }
    void EngagePlayer()
    {
        speed = 1;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
    private void MakeAShot()
    {
        if (fireCooldown == true)
        {
            Instantiate(projectile, projectileStartingPoint.position, Quaternion.identity);
        }
    }
    IEnumerator FireCooldown() //IEnumerator который циклом запускает выполение метода Randomizer через случайные промежутки времени
    {
        for (; ; )
        {
            fireCooldown = true;
            yield return new WaitForSeconds(1 / 6);
            fireCooldown = false;
            yield return new WaitForSeconds(5f / 6f);
        }
    }
    void Regroup()
    {
        transform.position = Vector2.MoveTowards(transform.position, spawnPosition, speed * Time.deltaTime);
        speed = 1;
        State = States.walk;
        if (transform.position.x > spawnPosition.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (transform.position.x < spawnPosition.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public enum States
    {
        idle,
        walk,
        attack,
        hurt,
        dead
    }
}
