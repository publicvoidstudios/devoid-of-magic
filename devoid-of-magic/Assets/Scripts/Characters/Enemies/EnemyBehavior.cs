using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed;
    public float distanceOfPatrol;
    public Transform point;
    bool isMovingRight;
    public Transform player;
    public float stopDistance;
    bool patrol = false;
    bool attack = false;
    bool regroup = false;
    private SpriteRenderer sprite;
    private Animator anim;

    void Start()
    {
        
    }

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    void Update()
    {       
        //Patrolling Stance
        if(Vector2.Distance(transform.position, point.position) < distanceOfPatrol && attack == false)
        {
            patrol = true;
        }
        //Attacking Stance
        if (Vector2.Distance(transform.position, player.position) < stopDistance)
        {
            attack = true;
            patrol = false;
            regroup = false;
        }
        //Regrouping Stance
        if (Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            regroup = true;
            attack = false;
        }
        if (patrol == true)
        {
            Patrol();
            State = States.walk;
        }
        else if (attack == true)
        {
            Attack();
            State = States.attack;
        }
        else if(regroup == true)
        {
            Regroup();
            State = States.walk;
        }
    }

     void Patrol()
    {
        speed = 1;
        if(transform.position.x > point.position.x + distanceOfPatrol)
        {
            isMovingRight = false;
        }
        else if(transform.position.x < point.position.x - distanceOfPatrol)
        {
            isMovingRight = true;            
        }
        if (isMovingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            sprite.flipX = transform.position.x > 0.0f;
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            sprite.flipX = transform.position.x < 0.0f;
        }
        
    }

    void Attack()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        speed = 0;
        if (transform.position.x >= player.position.x)
        {
            sprite.flipX = transform.position.x < 0.0f;
        }
        if (transform.position.x <= player.position.x)
        {
            sprite.flipX = transform.position.x > 0.0f;
        }
    }

    void Regroup()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
        speed = 1;
    } 

    void Stop()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position, 0);
    }

    public enum States
    {
        idle,
        walk,
        attack
    }
  
}
