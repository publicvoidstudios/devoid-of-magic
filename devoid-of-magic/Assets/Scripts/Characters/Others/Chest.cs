using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator animator;
    float speed;
    public float adjustableDistance;
    public float meleeAttackDistance;
    public int rand;
    public int damage;
    bool isMimic;
    bool triggered;
    [SerializeField]
    GameObject coin;
    [SerializeField]
    Enemy enemyScr;
    Transform player;
    Player playerScript;
    Rigidbody2D rb;
    CapsuleCollider2D ccd;
    public static Chest Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ccd = GetComponent<CapsuleCollider2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        transform.gameObject.tag = "Untagged";
        animator = GetComponent<Animator>();
        StartCoroutine(Randomizer());
        enemyScr.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("SetState", 2f);
    }
    private States State
    {
        get { return (States)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void SetState()
    {
        rb.bodyType = RigidbodyType2D.Static;
        ccd.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        meleeAttackDistance = transform.position.x - player.position.x;
        if (isMimic)
        {
            ccd.enabled = true;
            rb.bodyType = RigidbodyType2D.Dynamic;            
            enemyScr.enabled = true;
            if (enemyScr.alive)
            {
                Attack();
            }
            else
            {
                animator.SetBool("Dead", true);
            }
            
        }
    }
    void Attack()
    {
        if (Mathf.Abs(meleeAttackDistance) < adjustableDistance)
        {
            speed = 0;
            State = States.attack;
        }
        else
        {
            speed = 1;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            if (transform.position.x >= player.position.x) //Face Left
            {
                transform.localScale = new Vector3(1, 1, 1);
                State = States.walk;
            }
            if (transform.position.x <= player.position.x) //Face Right
            {
                transform.localScale = new Vector3(-1, 1, 1);
                State = States.walk;
            }
        }
    }

    public void DamagePlayer()
    {
        playerScript.DamageRecount(damage);
    }

    IEnumerator Randomizer()
    {
        rand = Random.Range(0, 3);
        yield return new WaitForSeconds(1f);
        if (triggered)
        {
            yield break;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            triggered = true;
            if(rand > 0)
            {
                isMimic = false;
                animator.SetBool("Open", true);
                ccd.enabled = false;
                rb.bodyType = RigidbodyType2D.Static;
            }
            if(rand == 0)
            {
                animator.SetBool("Open", false);
                isMimic = true;
                gameObject.tag = "Enemy";
                ccd.enabled = true;
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
    public void GiveCoin()
    {        
        for(int i = 0; i < rand; i++)
        {
            Vector2 spawnPos = new Vector2(transform.position.x + Random.Range(-0.01f, 0.01f), transform.position.y);
            Instantiate(coin, spawnPos, Quaternion.identity);
        }        
    }

    public void DestroyChest()
    {
        Destroy(gameObject);
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
