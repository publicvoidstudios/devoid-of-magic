using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float collectDistance;
    public int worth;
    [SerializeField] Player playerScript;
    [SerializeField] Transform player;
    Rigidbody2D rb;
    GoalDetector gd;
    // Start is called before the first frame update
    void Start()
    {
        gd = GameObject.FindGameObjectWithTag("GD").GetComponent<GoalDetector>();
        rb = GetComponent<Rigidbody2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        worth = (Random.Range(1, 21) * gd.levelID);
        AudioManager audio = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
        audio.Play("Coin");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) < collectDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    public void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript.gold += worth;
            Destroy(gameObject);
        }
    }
}
