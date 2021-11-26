using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pirate_Boss : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public float speed;
    public float enemyPlayerDistance;
    public float rangedDistance;
    public float meleeDistance;
    public Transform player;
    public float stopDistance;
    public GameObject projectile;
    public Transform projectileStartingPoint;
    public bool fireCooldown;
    public bool healCooldown;
    private GameObject target;
    public Player playerScript;
    public int damage;
    private Boss enemyScript;
    public int healingPower;
    public int rand;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public bool healing;
    public bool attacking;
    public GameObject HPCanvas;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        enemyScript = GetComponent<Boss>();
        target = GameObject.FindGameObjectWithTag("Player");
        playerScript = target.GetComponent<Player>();
        player = target.transform;
        StartCoroutine(FireCooldown());
        StartCoroutine(HealCooldown());
        timeBtwShots = startTimeBtwShots;
        stopDistance = playerScript.range;
    }
    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
    void Update()
    {
        enemyPlayerDistance = transform.position.x - player.position.x;
        if (Vector2.Distance(transform.position, player.position) < stopDistance)
        {
            HPCanvas.SetActive(true);
            attacking = true;
        }
        if(attacking && !playerScript.isDead)
        {
            Attack();
        }
        if (playerScript.isDead)
        {
            speed = 0;
            State = States.idle;
        }
    }

    void Attack()
    {
        if (!healing)
        {
            if (enemyScript.ratio >= 0.67f) //Stage 1 (Melee attacks)
            {
                if (Mathf.Abs(enemyPlayerDistance) <= meleeDistance) //If close to player
                {
                    speed = 0;
                    State = States.attack1;
                    //rand = Random.Range(0, 2);
                    //if(rand == 0)
                    //{
                    //    State = States.attack1;
                    //}
                    //if(rand == 1)
                    //{
                    //    State = States.attack2;
                    //}
                    TransformLocalscale();
                }
                if (Mathf.Abs(enemyPlayerDistance) > meleeDistance) //Engage player
                {
                    EngagePlayer();
                }
            }

            if (enemyScript.ratio > 0.34f && enemyScript.ratio < 0.67f) //Stage 2 (Ranged attacks)
            {
                if (Mathf.Abs(enemyPlayerDistance) < rangedDistance)
                {
                    speed = 0;
                    State = States.attack3;
                    MakeAShot();
                    TransformLocalscale();
                }
                else
                {
                    EngagePlayer();
                }
            }

            if (enemyScript.ratio > 0f && enemyScript.ratio <= 0.33f) //Stage 3 (Both)
            {
                if (Mathf.Abs(enemyPlayerDistance) < meleeDistance) //If close to player
                {
                    speed = 0;
                    State = States.attack2;
                    TransformLocalscale();
                }
                if (Mathf.Abs(enemyPlayerDistance) < rangedDistance && Mathf.Abs(enemyPlayerDistance) > meleeDistance)
                {
                    speed = 0;
                    TransformLocalscale();
                    State = States.attack3;
                    MakeAShot();
                }
                if(Mathf.Abs(enemyPlayerDistance) > rangedDistance)
                {
                    EngagePlayer();
                }
            }
        }        
    }

    void EngagePlayer()
    {
        speed = 1;
        State = States.walk;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        TransformLocalscale();
    }
    private void TransformLocalscale() //Turn this object to face Player
    {
        if (transform.position.x >= player.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (transform.position.x <= player.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    public void DamagePlayer()
    {
        playerScript.DamageRecount(damage + Random.Range(-2, 3));
    }
    private void MakeAShot()
    {
        if(timeBtwShots <= 0)
        {
            Instantiate(projectile, projectileStartingPoint.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        //if (fireCooldown == true)
        //{
        //    Instantiate(projectile, projectileStartingPoint.position, Quaternion.identity);            
        //}
    }
    IEnumerator FireCooldown()
    {
        for (; ; )
        {
            fireCooldown = true;
            yield return new WaitForSeconds(1f / 6f);
            fireCooldown = false;
            yield return new WaitForSeconds(5f / 6f);
        }
    }
    IEnumerator HealCooldown()
    {
        for (; ; )
        {
            healCooldown = true;
            healing = true;
            if (enemyScript.hp < enemyScript.maxhp)
            {
                State = States.drink;                
            }
            else
            {
                healCooldown = false;
                healing = false;
            }
            yield return new WaitForSeconds(10f / 6f);
            healCooldown = false;
            healing = false;
            yield return new WaitForSeconds(50f / 6f);
        }
    }
    public void Drink()
    {
        enemyScript.hp += healingPower;
        healCooldown = false;
        healing = false;
    }
    IEnumerator Rand()
    {
        for (; ; )
        {
            healCooldown = true;
            yield return new WaitForSeconds(1f / 6f);
            healCooldown = false;
            yield return new WaitForSeconds(59f / 6f);
        }
    }

    public void Heal()
    {
        if (healCooldown)
        {
            if(enemyScript.hp < enemyScript.maxhp)
            {
                State = States.drink;
                enemyScript.hp += healingPower;
            }
        }
        else
        {
            Attack();
        }
    }

    public enum States
    {
        idle,
        walk,
        attack1,
        attack2,
        attack3,
        hurt,
        drink,
        dead
    }

}
