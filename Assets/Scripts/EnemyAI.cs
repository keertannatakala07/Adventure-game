using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float velocity;
    public float aroundUs;
    public float attackAroundUS;
    public LayerMask whatIsPlayer;
    public GameObject projectile; 
    public float projectileVelocity = 10f; 

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    public Vector3 dir;

    public bool toTurn;

    private bool follow;
    private bool attack;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        anim.SetBool("isMoving", follow);
        follow = Physics2D.OverlapCircle(transform.position, aroundUs, whatIsPlayer);
        attack = Physics2D.OverlapCircle(transform.position, attackAroundUS, whatIsPlayer);
        dir = target.position - transform.position;
        dir.Normalize();
        movement = dir;
        if (toTurn)
        {
            anim.SetFloat("X", dir.x);
            anim.SetFloat("Y", dir.y);
        }
        if (attack)
        {
            ShootProjectile();
        }
    }

    private void FixedUpdate()
    {
        if (follow && !attack)
        {
            MoveCharacter(movement);
        }
        if (attack)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * velocity * Time.deltaTime));
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(this.projectile, transform.position, Quaternion.identity);
        Vector2 shootDirection = target.position - transform.position;
        shootDirection.Normalize();

        float angle = 45f; 
        float rad = angle * Mathf.Deg2Rad;
        float rotX = shootDirection.x * Mathf.Cos(rad) - shootDirection.y * Mathf.Sin(rad);
        float rotY = shootDirection.x * Mathf.Sin(rad) + shootDirection.y * Mathf.Cos(rad);

        Vector2 rotatedDirection = new Vector2(rotX, rotY);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.direction = rotatedDirection;
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = rotatedDirection * projectileVelocity;
    }

}
