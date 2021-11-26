using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBoss : MonoBehaviour
{
    [SerializeField] GameObject[] allEnemies;
    [SerializeField] GameObject demon;
    [SerializeField] Transform spawnPoint;
    [SerializeField] private Animator anim;
    public float speed;
    public float enemyPlayerDistance;
    public float rangedDistance;
    public float meleeDistance;
    public Transform player;
    public float stopDistance;
    public bool healCooldown;
    private GameObject target;
    public Player playerScript;
    public int damage;
    private Boss enemyScript;
    public int healingPower;
    public int rand;
    public bool healing;
    public bool attacking;
    public GameObject HPCanvas;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        enemyScript = GetComponent<Boss>();
        target = GameObject.FindGameObjectWithTag("Player");
        playerScript = target.GetComponent<Player>();
        player = target.transform;
        StartCoroutine(HealCooldown());
        stopDistance = playerScript.range;
    }
    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
    void Update()
    {
        if (!enemyScript.alive)
        {
            GoalDetector gd = GameObject.FindGameObjectWithTag("GD").GetComponent<GoalDetector>();
            gd.levelComplete = true;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); 
            foreach (GameObject e in enemies)
            {
                Enemy[] enemyScript = e.GetComponents<Enemy>();
                foreach (Enemy enemy in enemyScript)
                {
                    enemy.DestroyEndGame();
                }
            }
        }
        enemyPlayerDistance = transform.position.x - player.position.x;
        if (Vector2.Distance(transform.position, player.position) < stopDistance)
        {
            HPCanvas.SetActive(true);
            attacking = true;
        }
        if (attacking && !playerScript.isDead)
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
                    TransformLocalscale();
                }
                if (Mathf.Abs(enemyPlayerDistance) > meleeDistance) //Engage player
                {
                    EngagePlayer();
                }
            }

            if (enemyScript.ratio > 0.34f && enemyScript.ratio < 0.67f) //Stage 2 (2 melee attacks + summoning 1 demon)
            {
                if (Mathf.Abs(enemyPlayerDistance) < meleeDistance) //If close to player
                {
                    speed = 0;
                    State = States.attack1;
                    TransformLocalscale();
                }
                if (Mathf.Abs(enemyPlayerDistance) < rangedDistance && Mathf.Abs(enemyPlayerDistance) > meleeDistance) //Further
                {
                    speed = 0;
                    State = States.attack2;
                    TransformLocalscale();                    
                }
                if (Mathf.Abs(enemyPlayerDistance) > rangedDistance) // So long, meet demons
                {
                    State = States.attack3;
                }
            }

            if (enemyScript.ratio > 0f && enemyScript.ratio <= 0.33f) //Stage 3 (summoning all)
            {
                speed = 0;
                State = States.attack4;
            }
        }
    }

    public void SummonDemon()
    {
        Instantiate(demon, spawnPoint.position, Quaternion.identity);
    }

    public void SummonRandom()
    {
        int chance = Random.Range(0, 2);
        if(chance > 0)
        {
            int rand = Random.Range(0, allEnemies.Length);
            Instantiate(allEnemies[rand], spawnPoint.position, Quaternion.identity);
        }
    }
    void EngagePlayer()
    {
        speed = 2;
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
        enemyScript.hp += healingPower + Random.Range(-20, 30);
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
            if (enemyScript.hp < enemyScript.maxhp)
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
        attack4,
        hurt,
        drink,
        dead
    }
}
