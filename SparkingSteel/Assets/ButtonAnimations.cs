using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Remove health from specified hitbox functions referenced from https://youtu.be/mvVM1RB4HXk

public class ButtonAnimations : MonoBehaviour {

    //Game Components
    public Animator anim;
    public Animator enemyAnim;
    public Collider[] attackHitBoxes;
    public Collider armR;
    public Collider armL;
    public Text checkL;
    public Text checkR;
    public Image controlInstructions;
    public GameObject Player;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject enemy;
    public GameObject hitEffect;
    public GameObject ultEffectR;
    public GameObject ultEffectL;
    public GameObject ultHitboxF;
    public GameObject ultHitboxB;
    public GameObject ultHitboxR;
    public GameObject ultHitboxL;
    public GameObject hitClone;
    public ParticleSystem sparks;
    public ParticleSystem ultSparkR;
    public ParticleSystem ultSparkL;
    public OVRPlayerController playerScript;
    public AudioSource punchSoundL;
    public AudioSource punchSoundR;
    public EnemyHealth enemyHealth;

    //Variable which control animatons and effects
    private float vert;
    private float hori;
    private float walk;
    private float sprint;
    private float turn;
    private float punchR;
    private float punchL;
    private float hookR;
    private float hookL;
    public bool effect;
    public bool canDash;
    public bool ultPunchL;
    public bool ultPunchR;

    //Controller Buttons
    public bool rightTrigger;
    public bool leftTrigger;
    public bool rightShoulder;
    public bool leftShoulder;
    public bool stickButton;
    public bool xButton;
    public bool bButton;
 
       


 

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        Player = GameObject.Find("OVRPlayerController");
        leftArm = GameObject.Find("Left_Fist");
        rightArm = GameObject.Find("Right_Fist");
        enemy = GameObject.Find("Enemy Mech");
        hitEffect = GameObject.Find("HitPunch");
        hitClone = GameObject.Find("HitPunch(Clone");
        ultEffectR = GameObject.Find("UltPunchR");
        ultEffectL = GameObject.Find("UltPunchL");
        ultHitboxF = GameObject.Find("UltHitboxF");
        ultHitboxB = GameObject.Find("UltHitboxB");
        ultHitboxR = GameObject.Find("UltHitboxR");
        ultHitboxL = GameObject.Find("UltHitboxL");

        enemyAnim = enemy.GetComponent<Animator>();
        armR = rightArm.GetComponent<Collider>();
        armL = leftArm.GetComponent<Collider>();
        sparks = hitEffect.GetComponent<ParticleSystem>();
        ultSparkR = ultEffectR.GetComponent<ParticleSystem>();
        ultSparkL = ultEffectL.GetComponent<ParticleSystem>();
        playerScript = Player.GetComponent<OVRPlayerController>();
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        punchSoundL = leftArm.GetComponent<AudioSource>();
        punchSoundR = rightArm.GetComponent<AudioSource>();


        effect = false;
        ultPunchL = true;
        ultPunchR = true;
        Physics.IgnoreLayerCollision(11 , 9, true);
        StartCoroutine("TurnOffControlInstructions");
        



    }

    // Update is called once per frame
    void Update() {

        vert = Input.GetAxis("Vertical");
        hori = Input.GetAxis("Horizontal");
        Walk(); 
        Sprinting();
        Punching();
        Turning();
        PunchHook();
        UltPunch();

        hitClone = GameObject.Find("HitPunch(Clone)");

        rightTrigger = OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);
        leftTrigger = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
        rightShoulder = OVRInput.Get(OVRInput.Button.SecondaryShoulder);
        leftShoulder = OVRInput.Get(OVRInput.Button.PrimaryShoulder);
        stickButton = OVRInput.Get(OVRInput.Button.PrimaryThumbstick);
        xButton = OVRInput.Get(OVRInput.Button.Three);
        bButton = OVRInput.Get(OVRInput.Button.Two);

        if (sparks)
        {
            Destroy(hitClone, 1);
        }

    }

    public void FixedUpdate()
    {
        anim.SetFloat("Walk", walk);
        anim.SetFloat("Turn", turn);
        anim.SetFloat("Sprint", sprint);
        anim.SetFloat("PunchR", punchR);
        anim.SetFloat("PunchL", punchL);
        anim.SetFloat("HookR", hookR);
        anim.SetFloat("HookL", hookL);

    }

    public void Walk()
    {
        Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        if (primaryAxis.y > 0.0f || primaryAxis.y < 0.0f)
        {
            walk = 1f;
        }
        else
        {
            walk = 0f;
        }
    }

    public void Sprinting()
    {
        Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        if (stickButton && primaryAxis.y > 0.0f)
        {
            sprint = 0.1f;
            vert = 0f;
        }
        else
        {
            sprint = 0f;
        }
    }

    public void Punching()
    {
        if (rightShoulder)
        {
            punchR = 0.1f;
        }
        else
        {
            punchR = 0f;
        }
        if (leftShoulder)
        {
            punchL = 0.1f;
        }
        else
        {
            punchL = 0f;
        }
    }

    public void PunchHook()
    {
        if (rightTrigger)
        {
            hookR = 0.1f;
        }
        else
        {
            hookR = 0f;
        }
        if (leftTrigger)
        {
            hookL = 0.1f;
        }
        else
        {
            hookL = 0f;
        }
    }

    public void UltPunch()
    {
        if(bButton && ultPunchR)
        {
            anim.SetTrigger("bigBangR");
            ultPunchR = false;

            checkR.text = "X";
            checkR.color = Color.red;
        }
        else if (xButton && ultPunchL)
        {
            anim.SetTrigger("bigBangL");
            ultPunchL = false;

            checkL.text = "X";
            checkL.color = Color.red;
        }
    }

    public void Turning()
    {
        Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        if (primaryAxis.x < -0.5f)
        {
            turn = 0f;
        }

        else if (primaryAxis.x > 0.5f)
        {
            turn = 1f;
        }
        else
        {
            turn = 0.5f;
        }
    }


    //Turns on the dash at the beginning of the dash animation. The script for dashing is locaed in OVRPlayerController.cs
    private void DashOn()
    {
        playerScript.dash = true;

    }
    //Turns off the dash when the dash ends
    private void DashOff()
    {
        playerScript.dash = false;
    }
    //Can the player dash? this is to ensure the dash cant be held down giving the player a constant increase in speed
    public void CanDash()
    {
        playerScript.canDash = true;
    }

    public void CantDash()
    {
        playerScript.canDash = false;
    }




    private void LaunchAttackPunchL()
    {
        Collider[] cols = Physics.OverlapBox(armL.bounds.center, armL.bounds.extents, armL.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform)
            {
                continue;
            }

            Debug.Log("Punch has connected with Enemy");

            enemyHealth.RemoveHealthLeft(5f);
            punchSoundL.Play();
        }
    }

    //Right Punch properties, checks if punch connects with enemy collider and sends the amount of health to remove from the enemy
    private void LaunchAttackPunchR()
    {
        Collider[] cols = Physics.OverlapBox(armR.bounds.center, armR.bounds.extents, armR.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            //Makes sure whatever is hit by the atack isnt a part of the player. Skip our collider, continue checking.
            if (c.transform.root == transform)
            {
                continue;
            }

            Debug.Log(c.name);
            Debug.Log("Punch has connected with Enemy");

            enemyHealth.RemoveHealthRight(5f);
            punchSoundR.Play();
        }
    }

    private void LaunchHookL()
    {  
        Collider[] cols = Physics.OverlapBox(armL.bounds.center, armL.bounds.extents, armL.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform)
            {
                continue;
            }
            Debug.Log(c.name);
            Debug.Log("Punch has connected with Enemy");

            enemyHealth.RemoveHealthLeftHook(5f);
            punchSoundL.Play();
        }
    }

    private void LaunchHookR()
    {
        Collider[] cols = Physics.OverlapBox(armR.bounds.center, armR.bounds.extents, armR.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform)
            {
                continue;
            }
            Debug.Log(c.name);
            Debug.Log("Punch has connected with Enemy");

            enemyHealth.RemoveHealthRightHook(5f);
            punchSoundR.Play();
        }
    }

    private void UltAttackL()
    {
        Collider[] cols = Physics.OverlapBox(armL.bounds.center, armL.bounds.extents, armL.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform)
            {
                continue;
            }
            Debug.Log(c.name);
            Debug.Log("UltPunchL has connected with Enemy");

            enemyHealth.RemoveHealthUlt(50f);
            punchSoundL.Play();
        }

    }

    private void UltAttackR()
    {
        Collider[] cols = Physics.OverlapBox(armR.bounds.center, armR.bounds.extents, armR.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform)
            {
                continue;
            }
            Debug.Log(c.name);
            Debug.Log("UltPunchL has connected with Enemy");

            enemyHealth.RemoveHealthUlt(50f);
            punchSoundR.Play();
        }
    }

    private void UltParticlesR()
    {
        ultSparkR.Play();
    }

    private void UltParticlesL()
    {
        ultSparkL.Play();
    }

    

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (sparks.particleCount < 4)
            {
                Instantiate(sparks, contact.point, Quaternion.identity);
            }
        }
    }



    void PlayWalkSound()
    {
        var AudioSource = GetComponent<AudioSource>();
        AudioSource.Play();

    }

    IEnumerator TurnOffControlInstructions()
    {
            yield return new WaitForSeconds(20f);
            Color c = controlInstructions.color;
            c.a = 0;
            controlInstructions.color = c;      
    }
}
