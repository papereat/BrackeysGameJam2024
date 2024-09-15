
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

//using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Enemies : MonoBehaviour
{
    UnderworldControler player;
    SoundController soundController;
    public float Health = 50;
    public float enemySpeed = 0.1f;
    public float attackDamage = 5f;
    public float attackSpeed = 1f;
    private float attackTime = 0f;
    public float AttackDistance = 1f;
    public Vector2 AttackSize = new Vector2(0.5f, 1);
    public int id;

    bool on_ground;
    public float JumpForce;
    Rigidbody2D rb;

    public float jumpRate = 1f;
    private float jumpTime = 0f;

    public StateAnimator stateAnimator;

    public float AttackDuration;

    bool attacking;

    bool do_move_anim;

    bool look_left;

    public float knockbackTime = 1;


    void Start()
    {
        player = UnderworldControler.player;
        soundController = SoundController.soundController;

        id = UnityEngine.Random.Range(-2147483648, 2147483647);

        rb = transform.GetComponent<Rigidbody2D>();

        StartCoroutine(IdleSounds(UnityEngine.Random.Range(1, 4), 0));
    }
    IEnumerator IdleSounds(int sound, float time)
    {
        yield return new WaitForSeconds(time);

        soundController.playHellSound(sound, 0.1f);
        int randSound = UnityEngine.Random.Range(1, 4);

        StartCoroutine(IdleSounds(randSound, UnityEngine.Random.Range(1, 15)));
    }

    void Update()
    {
        attackTime -= Time.deltaTime;
        jumpTime -= Time.deltaTime;

        Movement();
        AttackPlayer();

        Animations();

        Death();
    }

    void Animations()
    {
        stateAnimator.GetComponent<SpriteRenderer>().flipX = look_left;
        if (attacking)
        {
            if (stateAnimator.CurrentState != "Attack")
            {
                stateAnimator.left = false;
                stateAnimator.SwitchState("Attack");
            }
        }
        else if (!on_ground)
        {
            if (stateAnimator.CurrentState != "Jump")
            {
                stateAnimator.left = false;
                stateAnimator.SwitchState("Jump");
            }
        }
        else if (do_move_anim)
        {
            if (stateAnimator.CurrentState != "Walk")
            {
                stateAnimator.left = false;
                stateAnimator.SwitchState("Walk");
            }
        }
        else
        {
            if (stateAnimator.CurrentState != "Idle")
            {
                stateAnimator.left = false;
                stateAnimator.SwitchState("Idle");
            }
        }
    }
    public void Damage(float DamageAmount, bool doKnockback)
    {
        soundController.playHellSound(7, 0.25f);

        Health -= DamageAmount;
        if(doKnockback)
        {
            StartCoroutine(pushBack((transform.position - player.transform.position).normalized));
        }

        Debug.Log(transform.position - player.transform.position);
    }
    IEnumerator pushBack(Vector2 direction)
    {
        float time = 0;
        while (true)
        {
            transform.position += new Vector3(direction.x, direction.y, 0) * 10 * 0.05f;
            time += 0.05f;
            if (time >= knockbackTime)
            {
                break;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    void Movement()
    {
        //Check if enenmy shoud move
        do_move_anim = true;

        //Moving the Enemy    
        Vector2 direction = (player.transform.position - transform.position).normalized;
        //transform.position += (Vector3)direction * enemySpeed * Time.deltaTime;

        rb.velocity = new Vector2(direction.x * enemySpeed, rb.velocity.y);

        //switches sprite direction depending on direction
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);  // Face right
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }

        //detects obstacles and jumps
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 1f, 1 << 7);

        if ((jumpTime <= 0) && hit.collider != null && on_ground)
        {
            soundController.playHellSound(5, 0.25f);

            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            jumpTime = jumpRate;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        on_ground = true;
    }    
    
    void OnTriggerStay2D(Collider2D col)
    {
        on_ground = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        on_ground = false;
    }


    public void AttackPlayer()
    {

        if (attackTime <= 0 && playerDistance() < 3 & on_ground)
        {
            soundController.playHellSound(6, 0.25f);

            StartCoroutine(AttackMelee());
            foreach (var item in AttackCollider())
            {
                MeleeAttack(item.GetComponent<Enemies>());
            }

            attackTime = attackSpeed;
        }
    }
    IEnumerator AttackMelee()
    {
        attacking = true;
        yield return new WaitForSeconds(AttackDuration);
        attacking = false;
    }
    float playerDistance()
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }
    void MeleeAttack(Enemies enemy)
    {
        player.Damage(attackDamage);
    }
    void Death()
    {
        if (Health <= 0)
        {
            Destroy(this.gameObject);
            int value = Random.Range(1, 10);
            player.Money += value;
            player.TotalMoney += value;
        }
        
        if(transform.position.y <= -10)
        {
            Destroy(this.gameObject);
        }

    }
    Collider2D[] AttackCollider()
    {
        Collider2D[] outp;

        Vector2 dir = new Vector2(1, 0) * (LookDirection() ? 1 : -1);

        outp = Physics2D.OverlapBoxAll((Vector2)(transform.position) + dir * AttackDistance, AttackSize, 0, 1 << 3);

        return outp;
    }

    bool LookDirection()
    {
        return transform.position.x <= player.transform.position.x;
    }

}
