using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Ground : MonoBehaviour
{
    [SerializeField] private float speed; //Animal's speed
    private bool walkedRight;
    public int randNumber;
    private Animator anim;
    private SpriteRenderer sprite;
    bool inRange;
    [SerializeField] Transform player;
    public bool isInteractible;
    [SerializeField] BordersTrigger bordersTrigger;

    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        State = States.idle;
        StartCoroutine(LoopRandomizer());
        inRange = false;
    }
    private void Update()
    {
        if (inRange == true && isInteractible)
        {
            State = States.bark;
            Bark();
            return;
        }
        if (inRange == false || !isInteractible)
        {
            if (randNumber == 1) //Если рандомайзер выдал 1 - идем влево
            {
                WalkLeft();
                return;
            }            

            if (randNumber == 2) //Если рандомайзер выдал 2 - идем вправо
            {
                WalkRight();
                return;
            }            

            if (randNumber == 3) //Если рандомайзер выдал 3 - постоим
            {
                Stop();
                return;
            }
        }
        
    }
    IEnumerator LoopRandomizer() //IEnumerator который циклом запускает выполение метода Randomizer через случайные промежутки времени
    {
        for (; ; )
        {
            Randomizer();
            yield return new WaitForSeconds(Random.Range(2, 5));
        }
    }
    private void Randomizer() //Генератор случайных чисел
    {
        randNumber = Random.Range(1, 4);
    }
    private void WalkRight()
    {
        State = States.walk;
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        //sprite.flipX = transform.position.x > 0.0f;
        sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        walkedRight = true;
    }
    private void WalkLeft()
    {
        State = States.walk;
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        //sprite.flipX = transform.position.x < 0.0f;
        sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
        walkedRight = false;
    }
    private void Stop()
    {
        State = States.idle;
        transform.position = Vector3.MoveTowards(transform.position, transform.position, 0 * Time.deltaTime);
        if (walkedRight)
        {
            //sprite.flipX = transform.position.x > 0.0f;
            sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (!walkedRight)
        {
            //sprite.flipX = transform.position.x < 0.0f;
            sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void Bark()
    {
        State = States.bark;
        transform.position = Vector3.MoveTowards(transform.position, transform.position, 0 * Time.deltaTime);
        if (transform.position.x < player.position.x)
        {
            sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (transform.position.x > player.position.x)
        {
            sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            inRange = false;
        }
    }
    public enum States
    {
        idle,
        walk,
        bark
    }
}
