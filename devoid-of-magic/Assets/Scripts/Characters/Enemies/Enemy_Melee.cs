using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public float speed;
    public int hp;
    public float distanceOfPatrol;
    public float meleeAttackDistance;
    bool isMovingRight;
    public Transform player;
    public float stopDistance;
    bool patrol = false;
    bool attack = false;
    bool regroup = false;
    public Vector2 spawnPosition;
    public GameObject target;
    public Player playerScript;
    public int damage;
    public float adjustableDistance;
    Enemy commonScript;
    public float verticalDistance; 
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        commonScript = GetComponent<Enemy>();
        spawnPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("Player");
        playerScript = target.GetComponentInChildren<Player>();
        stopDistance = playerScript.range;
        Patrol();
    }
    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
    void Update()
    {
        verticalDistance = transform.position.y - player.position.y - 1;
        meleeAttackDistance = transform.position.x - player.position.x;
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
            //sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.localScale = new Vector3(1, 1, 1);
            State = States.walk;
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            //sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(-1, 1, 1);
            State = States.walk;
        }

    }
    public void DamagePlayer()
    {
        playerScript.DamageRecount(damage + Random.Range(-2, 3));
    }
    void Attack()
    {
        if (Mathf.Abs(meleeAttackDistance) < adjustableDistance && Mathf.Abs(verticalDistance) < 1 && !playerScript.isDead)
        {
            speed = 0;
            State = States.attack;
        }
        else if (Mathf.Abs(meleeAttackDistance) > adjustableDistance && !playerScript.isDead)
        {
            speed = 1; 
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            if (transform.position.x >= player.position.x) //Face Left
            {
                transform.localScale = new Vector3(-1, 1, 1);
                State = States.walk;
            }
            if (transform.position.x <= player.position.x) //Face Right
            {
                transform.localScale = new Vector3(1, 1, 1);
                State = States.walk;
            }
        }
        else if (Mathf.Abs(meleeAttackDistance) < adjustableDistance && Mathf.Abs(verticalDistance) > 1 || playerScript.isDead)
        {
            speed = 0;
            State = States.idle;
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
