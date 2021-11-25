using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Enemy : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text damageText;
    [SerializeField] GameObject damageTextGO;
    [SerializeField] GameObject canvas;
    [SerializeField] private Vector3 offset;
    [SerializeField] public Vector3[] offsets; 
    public int laserDamage;
    public int hp;
    public int maxhp;
    [SerializeField] GameObject gold;
    GoalDetector gd;
    Animator anim;
    public bool alive;
    [SerializeField] public bool mimic;
    private void Start()
    {
        alive = true;
        anim = GetComponent<Animator>();
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        hp += player.progress * 3;
        maxhp = hp;
        slider.gameObject.SetActive(false);
        SetHPValue(hp, maxhp);
        gd = GameObject.FindGameObjectWithTag("GD").GetComponent<GoalDetector>();
    }
    private void Update()
    {        
        if (slider != null)
        {
            slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        }
        SetHPValue(hp, maxhp);
        if (hp <= 0 && alive)
        {
            Die();
        } 
        if(transform.position.y < -6f)
        {
            Debug.Log("Character fell off the map and will be destroyed");
            Destroy(gameObject);            
        }
    }

    public void SetHPValue(int curhp, int maxihp)
    {
        if (slider != null)
        {
            slider.gameObject.SetActive(curhp < maxihp);
            slider.value = curhp;
            slider.maxValue = maxihp;
        }            
    }
    public virtual void GetDamage(int damage)
    {
        hp -= damage;
        if(canvas != null)
        {
            damageTextGO.SetActive(true);
            damageText.text = damage.ToString();
            damageText.transform.position = Camera.main.WorldToScreenPoint(transform.position + offsets[Random.Range(0, offsets.Length)]);
        }        
    }
    
    public virtual void Die()
    {
        alive = false;
        Instantiate(gold, transform.position, Quaternion.identity);
        if (!mimic)
        {
            gd.killedEnemies += 1;
        }        
        anim.SetBool("Dead", true);
        var rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        var boxc = GetComponent<BoxCollider2D>();
        boxc.enabled = false;
        var capsc = GetComponent<CapsuleCollider2D>();
        capsc.enabled = false;
        gameObject.tag = "Untagged";
        StartCoroutine(Destroy());
    }
    public virtual void DestroyEndGame()
    {
        alive = false;
        anim.SetBool("Dead", true);
        StartCoroutine(Destroy());
    }
    IEnumerator Destroy()
    {
        if(canvas != null)
        {
            Destroy(canvas);
        }        
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void PlaySound(string name)
    {
        AudioManager audio = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
        audio.Play(name);
    }

    public void PlayRandomDeathSound()
    {
        AudioManager audio = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
        audio.Play(Random.Range(1, 20).ToString());
    }

    public void PlayRandomMonsterDeathSound()
    {
        AudioManager audio = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
        audio.Play(Random.Range(25, 28).ToString());
    }

    public void PlayRandomMonsterAttackSound()
    {
        AudioManager audio = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
        audio.Play(Random.Range(20, 25).ToString());
    }
}
