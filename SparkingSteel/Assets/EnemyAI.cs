using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Remove health from specified hitbox functions referenced from https://youtu.be/mvVM1RB4HXk

public class EnemyAI : MonoBehaviour {

    private NavMeshAgent agent;
    public Transform BigMech;
    static Animator anim;
    public GameObject leftAttack;
    public GameObject rightAttack;
    public GameObject player;
    public GameObject projectile;
    public ParticleSystem projectileParticleSystem;
    public PlayerHealth playerHealth;
    public Collider rPunch;
    public Collider lPunch;
    public Rigidbody rigidBody;
    public bool canLunge;
    public float distance;

    // Use this for initialization
    void Start () {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        leftAttack = GameObject.Find("L_Hand");
        rightAttack = GameObject.Find("R_Hand");
        player = GameObject.Find("BigMech");
        Collider PlayerHit = player.GetComponent<Collider>();
        Collider rPunch = rightAttack.GetComponent<Collider>();
        Collider lPunch = leftAttack.GetComponent<Collider>();
        playerHealth  = player.GetComponent<PlayerHealth>();
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        canLunge = true;
    }

    public void FixedUpdate()
    {
        anim.SetFloat("distance", distance);


    }
    // Update is called once per frame
    void Update () {
        distance = Vector3.Distance(BigMech.position, this.transform.position);
        //If the Enemy is close enough to the player and is not destroyed, continue
        if (distance < 400 && anim.GetBool("destroyed") == false)
        {

            Vector3 direction = BigMech.position - this.transform.position;
            direction.y = 0;
        
            anim.SetBool("isIdle", false);
            //If distance greater than 100 between the Enemy and the Player mech, run after the Player Mech
            if (distance > 100 && anim.GetBool("hitL") == false && anim.GetBool("hitR") == false && anim.GetBool("hookL") == false
                && anim.GetBool("hookR") == false)
            {
        
                agent.SetDestination(BigMech.position);
                anim.SetBool("isRunning", true);
                anim.SetBool("isPunch", false);
                anim.SetBool("fire", false);
                anim.SetBool("canLunge", true);
                /*If distance is greater than 250 and canLunge bool is true, lunge attack then seet CanLunge bool to false. 
                This will be reset to true when the enemy and player's distance increases*/
                if (distance > 250 && canLunge)
                {
                    anim.SetTrigger("lunge");
                    anim.SetBool("lunge", true);
                    anim.SetBool("canLunge", false);

                }

            }
            //Else punch the enemy when close enough
            else if (anim.GetBool("hitL") == false && anim.GetBool("hitR") == false && anim.GetBool("hookL") == false 
                && anim.GetBool("hookR") == false)
            {
                anim.SetBool("isPunch", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("fire", false);
                agent.SetDestination(this.transform.position);
            }
            else
            {
                anim.SetBool("isPunch", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("fire", false);
                agent.SetDestination(this.transform.position);
            }
        }
        else if (anim.GetBool("destroyed") == false)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isPunch", false);
            anim.SetBool("fire", false);
            agent.SetDestination(this.transform.position);
        }
        
	}

    private void EnemyAttackPunchL()
    {
        Collider[] cols = Physics.OverlapBox(lPunch.bounds.center, lPunch.bounds.extents, lPunch.transform.rotation, LayerMask.GetMask("Player"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform)
            {
                continue;
            }
            Debug.Log(c.name);
            Debug.Log("Enemy Hit Me");

            
            playerHealth.RemoveHealthLeft(70f);
        }
    }

    private void EnemyAttackPunchR()
    {      
        Collider[] cols = Physics.OverlapBox(rPunch.bounds.center, rPunch.bounds.extents, rPunch.transform.rotation, LayerMask.GetMask("Player"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform)
            {
                continue;
            }

            Debug.Log(c.name);
            Debug.Log("Enemy Hit Me");

            playerHealth.RemoveHealthRight(70f);
        }
    }

    private void CannotBeHit()
    {
        anim.SetBool("canHit", false);
    }

    void PlayWalkSound()
    {
        var AudioSource = GetComponent<AudioSource>();
        AudioSource.Play();

    }

}
