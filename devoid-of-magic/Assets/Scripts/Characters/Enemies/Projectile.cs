using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float speed;
    Vector2 currentPoint;
    [SerializeField] Vector2 targetPoint;
    private Transform player;
    public Player target;
    public int damage;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPoint = player.position;
        targetPoint.y += 1f;
    }

    // Update is called once per frame
    void Update()
    {
        currentPoint = transform.position;
        
        transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
        if(currentPoint == targetPoint)
        {
            anim.Play("bullet_explode");
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !target.isDead)
        {
            target.DamageRecount(damage + Random.Range(-2, 3));
            Destroy(gameObject);
        }
    }

}
