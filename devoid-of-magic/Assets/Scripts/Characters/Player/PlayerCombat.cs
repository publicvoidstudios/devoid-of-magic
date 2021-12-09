using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.28f;
    public LayerMask enemyLayer;
    public int damageMelee;
    public int damageRanged;
    public Enemy enemyScript;
    public GameObject arrow;
    public Transform arrowSpawnPoint;
    public float attackRate = 1f;
    float nextAttackTime = 0;
    public Player playerScript;
    public Player_Projectile projectile;
    private GameObject[] enemiesArr;
    public GameObject nearest;
    public bool attacking;

    private void Start()
    {
        animator = GetComponent<Animator>(); // Automatically get Animator from this GameObject
        attackPoint = GameObject.Find("AttackPoint").transform; // Searches GameObject and attaches it's Transform to our variable
        playerScript = GetComponent<Player>();
        projectile = arrow.GetComponent<Player_Projectile>();
        
    }
    // Update is called once per frame
    void Update()
    {
        attacking = animator.GetBool("Attacking");
        enemiesArr = GameObject.FindGameObjectsWithTag("Enemy");
        FindNearest();
        TurnToClosestEnemy();
        if(Time.time >= nextAttackTime && !playerScript.isDead) // Cooldown between attacks
        {
            if (Input.GetKeyDown(KeyCode.X)) // Melee attack
            {
                Attack();
                nextAttackTime = Time.time + attackRate;// Cooldown refresher
            }
            if (Input.GetKeyDown(KeyCode.T)) // Ranged attack
            {
                AttackRanged();
                nextAttackTime = Time.time + attackRate;// Cooldown refresher
            }
        }        
    }

    private void TransformLocalScale(float X)
    {
        transform.localScale = new Vector3(X, 1, 1);
    }

    public void TurnToClosestEnemy()
    {
        if (attacking)
        {
            if(transform.position.x > nearest.transform.position.x)
            {
                TransformLocalScale(-1f);
                // turn left
            }
            if(transform.position.x < nearest.transform.position.x)
            {
                TransformLocalScale(1f);
                //turn right
            }
        }
    }

    public void AxeButton()
    {
        if (Time.time >= nextAttackTime && !playerScript.isDead) // Cooldown between attacks
        {
            Attack();
            nextAttackTime = Time.time + attackRate;// Cooldown refresher
        }
    }

    public void BowButton()
    {
        if (Time.time >= nextAttackTime && !playerScript.isDead) // Cooldown between attacks
        {
            AttackRanged();
            nextAttackTime = Time.time + attackRate;// Cooldown refresher
        }
    }

    public void NullifySpeed()
    {
        playerScript.speed = 0;
    }

    public void NormalizeSpeed()
    {
        playerScript.speed = 3;
    }

    public void Attack()
    {
        //Randomize attack
        int rand = Random.Range(0, 2);
        //Play Animation
        if (rand == 0)
        {
            animator.SetTrigger("Attack");
        }
        if (rand == 1)
        {
            animator.SetTrigger("Attack2");
        }
    }

    public void AttackMeleeInAnim()
    {
        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        //Damage them 
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy[] enemies = enemy.GetComponents<Enemy>();
            foreach (Enemy enemy1 in enemies)
            {
                enemy1.GetDamage(damageMelee + (playerScript.axeLevel * 3) + Random.Range(-playerScript.axeLevel, playerScript.axeLevel));
                Debug.Log(enemy.name + " damaged by AXE");
            }
        }
        foreach (Collider2D enemy in hitEnemies)
        {
            Boss boss = enemy.GetComponent<Boss>();
            if(boss != null)
            {
                boss.GetDamage(damageMelee + (playerScript.axeLevel * 3) + Random.Range(-playerScript.axeLevel, playerScript.axeLevel));
                Debug.Log(boss.name + " damaged by AXE");
            }            
        }
    }
    public void IsAttacking(int state)
    {
        if(state == 0)
        {
            animator.SetBool("Attacking", false);
        }
        if(state == 1)
        {
            animator.SetBool("Attacking", true);
        }
        
    }
    void AttackRanged()
    {
        //Animation
        animator.SetTrigger("Bow");
    }
    public void InstantiateArrow() //Is called as an Event in animation
    {
        //Set arrow damage
        projectile.projectile_damage = damageRanged + (playerScript.bowLevel * 3) + Random.Range(-playerScript.bowLevel, playerScript.bowLevel);
        //Instantiates an arrow from Prefab
        Instantiate(arrow, arrowSpawnPoint.position, Quaternion.AngleAxis(projectile.angle, Vector3.right));
    }
    GameObject FindNearest()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
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
        return nearest;
    }
    private void OnDrawGizmosSelected() //To see actual range of melee attack
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
