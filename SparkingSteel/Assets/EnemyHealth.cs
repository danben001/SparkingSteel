using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Referenced from https://youtu.be/9W0xLonwbLo

public class EnemyHealth : MonoBehaviour {

    public float health = 300f;
    public Animator anim;
    public Animator playerAnim;
    public Text playerVictory;
    public GameObject playerMech;
    public GameObject rightArm;
    public int counterRandom;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMech = GameObject.Find("BigMech");
        playerAnim = playerMech.GetComponent<Animator>();

    }

    public void RemoveHealthLeft(float amount)
    {
        anim.SetTrigger("hitL");
        anim.SetBool("canHit", true);

        Debug.Log("Hello");

        health -= amount;
        if (health <= 0.0f)
        {
            anim.SetBool("destroyed", true);
            Color c = playerVictory.color;
            c.a = 100;
            playerVictory.color = c;
        }
        counterRandom = Random.Range(1, 10);
        if (counterRandom == 3)
        {
            anim.SetTrigger("counter");

        }
    }

    public void RemoveHealthLeftHook(float amount)
    {
        anim.SetTrigger("hookL");
        anim.SetBool("canHit", true);

        Debug.Log("Hello");

        health -= amount;
        if (health <= 0)
        {
            anim.SetBool("destroyed", true);
            Color c = playerVictory.color;
            c.a = 100;
            playerVictory.color = c;
        }
        counterRandom = Random.Range(1, 10);
        if (counterRandom == 3)
        {
            anim.SetTrigger("counter");
        }
    }

    public void RemoveHealthRight(float amount)
    {
        anim.SetTrigger("hitR");
        anim.SetBool("canHit", true);

        Debug.Log("Hello");

        health -= amount;
        if (health <= 0)
        {
            anim.SetBool("destroyed", true);
            Color c = playerVictory.color;
            c.a = 100;
            playerVictory.color = c;
        }
        counterRandom = Random.Range(1, 10);
        if (counterRandom == 3)
        {
            anim.SetTrigger("counter");

        }
    }

    public void RemoveHealthRightHook(float amount)
    {
        anim.SetTrigger("hookR");
        anim.SetBool("canHit", true);

        Debug.Log("Hello");

        health -= amount;
        if (health <= 0)
        {
            anim.SetBool("destroyed", true);
            Color c = playerVictory.color;
            c.a = 100;
            playerVictory.color = c;
        }

        counterRandom = Random.Range(1, 10);
        if (counterRandom == 3)
        {
            anim.SetTrigger("counter");

        }
    }

    public void RemoveHealthUlt(float amount)
    {
        anim.SetTrigger("ultHit");
        anim.SetBool("canHit", true);

        Debug.Log("Hello");

        health -= amount;
        if (health <= 0)
        {
            anim.SetBool("destroyed", true);
            Color c = playerVictory.color;
            c.a = 100;
            playerVictory.color = c;
        }

    }
}


