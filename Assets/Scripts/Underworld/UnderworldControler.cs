using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnderworldControler : MonoBehaviour
{
    public static UnderworldControler player;
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

    public GameObject HookProjectile;

    void Awake()
    {
        player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementInputs();
        Movement();

        Attacks();

    }

    void Attacks()
    {

        //Aiming
        if (aiming & !activeProjectile)
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
        if (do_hit && !aiming)
        {

            foreach (var item in AttackCollider())
            {

                MeleeAttack(item.GetComponent<Enemies>());
            }
        }


    }

    void ShowRodAim()
    {
        //We want to show a line that would show how to hook is gonna travel when shot
        //TEMP currently nothing
    }

    void ShootRod()
    {
        //Get direction
        Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * HookSpeed;

        //Teleport Bullet to player
        HookProjectile.transform.position = transform.position;

        //Set Velocioty
        HookProjectile.GetComponent<Rigidbody2D>().velocity = dir * HookSpeed;

        HookProjectile.GetComponent<HookControler>().HookLength = HookLength / HookSpeed;


        activeProjectile = true;
    }

    public void ReturnProjectile()
    {
        HookProjectile.transform.position = new Vector2(1000, 1000);
        HookProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2();

        Debug.Log(LayerMask.NameToLayer("Ground"));

        activeProjectile = false;
    }

    public void HitEnemyProjectile(Enemies enemy)
    {
        ShootAttack(enemy);
    }
    void ShootAttack(Enemies enemy)
    {
        enemy.Damage(20);
    }
    void MeleeAttack(Enemies enemy)
    {

        enemy.Damage(5);
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
        //Checks if can move 
        //Currenlty this is always true
        rb.velocity = new Vector2(horizontal_inputs * WalkSpeed, rb.velocity.y);

        //Jump
        if (on_ground && jump_input)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    //Checks if on ground
    //Currently not implemented


    void OnTriggerEnter2D(Collider2D col)
    {
        on_ground = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        on_ground = false;
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
