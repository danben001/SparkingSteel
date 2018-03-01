using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Referenced from https://youtu.be/9W0xLonwbLo

public class PlayerHealth : MonoBehaviour {

    public Image currentHealth;
    public Text defeat;

    public float health = 900f;
    public float maxHealth = 900f;
    public Animator anim;
    public ParticleSystem damageSparks;
    public GameObject cockpit;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        cockpit = GameObject.Find("Damaged");
        damageSparks = cockpit.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        float ratio = health / maxHealth;
        currentHealth.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }

    public void RemoveHealthLeft(float amount)
    {
        anim.SetTrigger("isHitR");
        Debug.Log("Enemy Hit me");
        health -= amount;
        if (health <= 200f)
        {
            damageSparks.Play();
        }
        if (health <= 0f)
        {
            Color c = defeat.color;
            c.a = 100;
            defeat.color = c;
            anim.SetBool("destroyed", true); 
        }
    }

    public void RemoveHealthRight(float amount)
    {
        anim.SetTrigger("isHitL");

        Debug.Log("Enemy Hit Me");

        health -= amount;
        if (health <= 200f)
        {
            damageSparks.Play();
        }
        if (health <= 0f)
        {
           
            Color c = defeat.color;
            c.a = 100;
            defeat.color = c;
            anim.SetBool("destroyed", true);
        }
    }
}