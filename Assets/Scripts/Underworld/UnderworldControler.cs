using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Burst.Intrinsics;



public class UnderworldControler : MonoBehaviour
{
    public static UnderworldControler player;
    SoundController soundController;
    public StateAnimator stateAnimator;
    public FishingRod fishingRod;
    WorldManager worldManager;
    public float WalkSpeed;
    public float JumpForce;
    public KeyCode Left = KeyCode.A, Right = KeyCode.D, Jump = KeyCode.W, Jump2 = KeyCode.Space, Hit_key = KeyCode.Mouse0, Rod_Hit_Key = KeyCode.Mouse1, Rod_Reel_Key = KeyCode.E;

    public Collider2D ground_collider;
    Rigidbody2D rb;


    float horizontal_inputs;
    bool jump_input;
    [SerializeField]
    bool on_ground;

    bool do_hit;
    bool aiming;
    public bool activeProjectile;

    public float AttackDistance;
    public float HookLength;
    public float HookSpeed;
    public Vector2 AttackSize;
    public float Money;
    public int day;

    float JumpAnimTime;

    public GameObject HookProjectile;

    public float playerHealth = 50f;

    bool attacking;
    bool do_jump_anim;
    bool do_move_anim;
    bool right_anim;

    public float attackTime;
    [Header("Ouchy")]
    public GameObject OuchyText;
    public Vector2 OuchySpawnDisplacement;
    public float ouchyRotationDisplacement;
    public float OuchyTime;
    public Vector3 OuchySpot;
    public Vector2 OuchyRotation;

    [Header("Hook Shader")]
    public Material HookShader;
    public Vector3 HookDisplacement;

    void Awake()
    {
        player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        soundController = SoundController.soundController;
        worldManager = WorldManager.wm;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInputs();
        Movement();



        Attacks();

        Death();

        Animations();
        HookShaderUpdate();

    }
    void HookShaderUpdate()
    {
        HookShader.SetVector("_Hook_Position", HookProjectile.transform.position);
        HookShader.SetVector("_Rod_Positon", transform.position + new Vector3(HookDisplacement.x * (right_anim ? 1 : -1), HookDisplacement.y, HookDisplacement.z));
        HookShader.SetInt("_No_Show", activeProjectile ? 1 : 0);
    }
    void Animations()
    {
        if (activeProjectile)
        {
            if (right_anim)
            {
                if (stateAnimator.CurrentState != "ThrowR")
                {
                    stateAnimator.left = false;
                    stateAnimator.SwitchState("ThrowR");
                }
            }
            else if (stateAnimator.CurrentState != "ThrowL")
            {
                stateAnimator.left = true;
                stateAnimator.SwitchState("ThrowL");
            }
        }
        else if (attacking)
        {
            if (right_anim)
            {
                if (stateAnimator.CurrentState != "AttackR")
                {
                    stateAnimator.left = false;
                    stateAnimator.SwitchState("AttackR");
                }
            }
            else if (stateAnimator.CurrentState != "AttackL")
            {
                stateAnimator.left = true;
                stateAnimator.SwitchState("AttackL");
            }
        }
        else if (do_jump_anim)
        {
            if (right_anim)
            {
                if (stateAnimator.CurrentState != "JumpR")
                {
                    stateAnimator.left = false;
                    stateAnimator.SwitchState("JumpR");
                }
            }
            else if (stateAnimator.CurrentState != "JumpL")
            {
                stateAnimator.left = true;
                stateAnimator.SwitchState("JumpL");
            }
        }
        else if (do_move_anim)
        {
            if (right_anim)
            {
                if (stateAnimator.CurrentState != "WalkR")
                {
                    stateAnimator.left = false;
                    stateAnimator.SwitchState("WalkR");
                }
            }
            else if (stateAnimator.CurrentState != "WalkL")
            {
                stateAnimator.left = true;
                stateAnimator.SwitchState("WalkL");
            }
        }
        else
        {
            if (right_anim)
            {
                if (stateAnimator.CurrentState != "IdleR")
                {
                    stateAnimator.left = false;
                    stateAnimator.SwitchState("IdleR");
                }
            }
            else if (stateAnimator.CurrentState != "IdleL")
            {
                stateAnimator.left = true;
                stateAnimator.SwitchState("IdleL");
            }
        }
    }
    void Attacks()
    {

        //Aiming
        if (aiming & !activeProjectile & on_ground & !attacking)
        {
            //slow play movement
            //TEMP currently nothing

            //Show aim line
            ShowRodAim();

            //Shoot Projectile
            if (do_hit)
            {
                ShootRod();
            }
        }


        //Melee attack
        if (do_hit & !aiming & !attacking & on_ground)
        {
            soundController.playHellSound(8, 0.1f);

            StartCoroutine(AttackMelee());
            foreach (var item in AttackCollider())
            {

                MeleeAttack(item.GetComponent<Enemies>());
            }
        }


    }

    void Death()
    {
        if (playerHealth <= 0)
        {
            //Do later
        }

    }

    void ShowRodAim()
    {
        //We want to show a line that would show how to hook is gonna travel when shot
        //TEMP currently nothing
    }

    void ShootRod()
    {
        soundController.playHellSound(13, 0.5f);

        //Get direction
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - (transform.position + new Vector3(HookDisplacement.x * (right_anim ? 1 : -1), HookDisplacement.y, HookDisplacement.z));
        dir = dir.normalized;
        Debug.Log(dir.magnitude);

        //Teleport Bullet to player
        HookProjectile.transform.position = transform.position + new Vector3(HookDisplacement.x * (right_anim ? 1 : -1), HookDisplacement.y, HookDisplacement.z);

        //Set Velocioty
        HookProjectile.GetComponent<Rigidbody2D>().velocity = dir * HookSpeed;

        HookProjectile.GetComponent<HookControler>().HookLength = worldManager.GetDepth(fishingRod.Depth) / HookSpeed;


        activeProjectile = true;
    }

    public void ReturnProjectile()
    {
        HookProjectile.transform.position = new Vector2(1000, 1000);
        HookProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2();

        Debug.Log(LayerMask.NameToLayer("Ground"));

        activeProjectile = false;

        HookProjectile.GetComponent<HookControler>().IDsHit.Clear();
    }

    public void HitEnemyProjectile(Enemies enemy)
    {
        ShootAttack(enemy);
    }
    void ShootAttack(Enemies enemy)
    {
        enemy.Damage(10 * worldManager.GetPower(fishingRod.Power));
    }
    void MeleeAttack(Enemies enemy)
    {
        enemy.Damage(5 * worldManager.GetPower(fishingRod.Power));
    }

    IEnumerator AttackMelee()
    {
        attacking = true;
        yield return new WaitForSeconds(attackTime);
        attacking = false;
    }

    public void Damage(float DamageAmount)
    {
        soundController.playHellSound(9, 0.1f);

        playerHealth -= DamageAmount;

        StartCoroutine(OuchyEffect());
    }

    IEnumerator OuchyEffect()
    {
        GameObject OuchyObject = Instantiate(OuchyText, transform.position + OuchySpot + new Vector3(Random.Range(-OuchySpawnDisplacement.x, OuchySpawnDisplacement.x), Random.Range(-OuchySpawnDisplacement.y, OuchySpawnDisplacement.y), -1), new Quaternion());
        OuchyObject.transform.eulerAngles = new Vector3(0, 0, Random.Range(-ouchyRotationDisplacement, ouchyRotationDisplacement));

        OuchyObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        OuchyObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(OuchyTime);
        OuchyObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        OuchyObject.GetComponent<Rigidbody2D>().AddTorque(Random.Range(OuchyRotation.x, OuchyRotation.y));
        yield return new WaitForSeconds(10);
        Destroy(OuchyObject);

    }

    void MovementInputs()
    {
        //reset input variables
        horizontal_inputs = 0;
        jump_input = false;



        //Collect Inputs
        //Horizontal Inputs
        if (Input.GetKey(Left))
        {
            horizontal_inputs -= 1;
        }
        if (Input.GetKey(Right))
        {
            horizontal_inputs += 1;
        }

        //Jump
        if (Input.GetKeyDown(Jump) || Input.GetKeyDown(Jump2))
        {
            jump_input = true;
        }


        //Melee attack
        do_hit = Input.GetKeyDown(Hit_key);

        //Aiming
        aiming = Input.GetKey(Rod_Reel_Key);
    }


    void Movement()
    {
        //Horizontal
        //reset horizontal velocity
        rb.velocity = new Vector2(0, rb.velocity.y);
        //Checks if can move 
        if (!activeProjectile && !attacking)
        {
            if (horizontal_inputs == 1)
            {
                right_anim = true;
            }
            else if (horizontal_inputs == -1)
            {
                right_anim = false;
            }

            do_move_anim = horizontal_inputs != 0;
            rb.velocity = new Vector2(horizontal_inputs * WalkSpeed, rb.velocity.y);
        }


        //Jump
        if (on_ground && jump_input && !activeProjectile && !attacking)
        {
            soundController.playHellSound(10, 0.1f);

            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    //Checks if on ground
    //Currently not implemented


    void OnTriggerEnter2D(Collider2D col)
    {
        on_ground = true;
        do_jump_anim = false;

    }

    void OnTriggerStay2D(Collider2D col)
    {
        on_ground = true;
        do_jump_anim = false;

    }

    void OnTriggerExit2D(Collider2D other)
    {
        on_ground = false;
        do_jump_anim = true;
    }


    Collider2D[] AttackCollider()
    {
        Collider2D[] outp;

        Vector2 dir = new Vector2(1, 0) * (LookDirection() ? 1 : -1);

        outp = Physics2D.OverlapBoxAll((Vector2)(transform.position) + dir * AttackDistance, AttackSize, 0, 1 << 8);

        return outp;
    }

    //returns true when mosue is to the right of the play otherwise false
    bool LookDirection()
    {
        Vector2 screen_pos = Camera.main.WorldToScreenPoint(transform.position);

        //TEMP always set to true
        return screen_pos.x <= Input.mousePosition.x;
    }
}
