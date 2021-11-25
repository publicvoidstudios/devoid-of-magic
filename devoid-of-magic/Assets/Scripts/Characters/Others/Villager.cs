using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    [SerializeField] private float speed; //villager speed
    private bool walkedRight;
    public int randNumber;
    private Animator anim;
    private SpriteRenderer sprite;

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
    }
    private void Update()
    {
        if (randNumber == 1) //���� ����������� ����� 1 - ���� �����
        {
            WalkLeft();
            return;
        }

        if (randNumber == 2) //���� ����������� ����� 2 - ���� ������
        {
            WalkRight();
            return;
        }

        if (randNumber == 3) //���� ����������� ����� 3 - �������
        {
            Stop();
            return;
        }
    }
    IEnumerator LoopRandomizer() //IEnumerator ������� ��������� ��������� ������ Randomizer ����� ��������� ���������� �������
    {
        for (; ; )
        {
            Randomizer();
            yield return new WaitForSeconds(Random.Range(2, 5));
        }
    }
    private void Randomizer() //��������� ��������� �����
    {
        randNumber = Random.Range(1, 4);
    }
    private void WalkRight()
    {      
        State = States.walk;        
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        walkedRight = true;
    }
    private void WalkLeft()
    {
        State = States.walk;
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
        walkedRight = false;
    }
    private void Stop()
    {
        State = States.idle;
        transform.position = Vector3.MoveTowards(transform.position, transform.position, 0 * Time.deltaTime);
        if (walkedRight)
        {
            sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (!walkedRight)
        {
            sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public enum States
    {
        idle,
        walk
    }
}
