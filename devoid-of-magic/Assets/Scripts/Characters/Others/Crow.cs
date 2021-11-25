using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    [SerializeField] Vector2 startingPoint;
    [SerializeField] Vector2 currentPoint;
    private int randSpeed;
    
    void Start()
    {
        startingPoint = transform.position;
        StartCoroutine(LoopRandomizer());
    }
    IEnumerator LoopRandomizer() //IEnumerator который циклом запускает выполение метода Randomizer через случайные промежутки времени
    {
        for (; ; )
        {
            Randomizer();
            yield return new WaitForSeconds(60f);
        }
    }
    private void Randomizer() //Генератор случайных чисел
    {
        randSpeed = Random.Range(2, 7);
    }

    // Update is called once per frame
    void Update()
    {
        currentPoint = transform.position;
        Fly();
    }
    public void Fly()
    {
        transform.position = new Vector2(transform.position.x + randSpeed * Time.deltaTime, transform.position.y);
        if(startingPoint.x + 50 < currentPoint.x)
        {
            Destroy(gameObject);
        }
    }



}
