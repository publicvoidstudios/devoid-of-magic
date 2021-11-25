using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] TMP_Text damageText;
    [SerializeField] GameObject damageTextGO;
    [SerializeField] public Vector3[] offsets;
    public int hp;
    public int maxhp;
    GoalDetector gd;
    Animator anim;
    public bool alive;
    public Image health;
    public float ratio;
    public int laserDamage;
    [SerializeField] GameObject drop;
    private void Start()
    {
        alive = true;
        anim = GetComponent<Animator>();
        maxhp = hp;
        gd = GameObject.FindGameObjectWithTag("GD").GetComponent<GoalDetector>();
    }
    private void Update()
    {
        if(hp > maxhp) //If totally healed
        {
            hp = maxhp;
        }
        ratio = (float)hp / (float)maxhp;
        health.fillAmount = ratio;
        if (hp <= 0 && alive)
        {
            Die();
        }
    }
    public virtual void GetDamage(int damage)
    {
        hp -= damage;
        if (canvas != null)
        {
            damageTextGO.SetActive(true);
            damageText.text = damage.ToString();
            damageText.transform.position = Camera.main.WorldToScreenPoint(transform.position + offsets[Random.Range(0, offsets.Length)]);
        }
    }
    public virtual void Die()
    {
        alive = false;
        gd.killedEnemies += 1;
        anim.SetBool("Dead", true);
        if (drop != null)
        {
            Instantiate(drop, transform.position, Quaternion.identity);
        }
        StartCoroutine(Destroy());
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
    public void PlaySound(string name)
    {
        AudioManager audio = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
        audio.Play(name);
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
