using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
//using System.Numerics;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    UnderworldControler player;
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
    
    void Start()
    {
        player = UnderworldControler.player;
        id = UnityEngine.Random.Range(-2147483648, 2147483647);

        rb = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        attackTime -= Time.deltaTime;
        jumpTime -= Time.deltaTime;
        
        Movement();
        AttackPlayer();

        Death();
    }
    public void Damage(float DamageAmount)
    {
        Health -= DamageAmount;

        Debug.Log(gameObject.name + " Got Damaged");
    }

    void Movement()
    {   
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
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            jumpTime = jumpRate;
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        on_ground = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        on_ground = false;
    }


    public void AttackPlayer()
    {
        
        if(attackTime <= 0 && playerDistance() < 3)
        {
            foreach (var item in AttackCollider())
            {
                MeleeAttack(item.GetComponent<Enemies>());
            }
            
            attackTime = attackSpeed;
        }
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
        if(Health <= 0)
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
