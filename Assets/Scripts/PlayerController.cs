using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Playermovement playerMovement;
    private Vector2 movement;

    void Start()
    {
        playerMovement = GetComponent<Playermovement>();
        playerMovement.Initialize(rb, movespeed);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.sqrMagnitude > 1)
        {
            movement = movement.normalized;
        }
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        playerMovement.userMove(movement);
    }

    void FixedUpdate()
    {
        playerMovement.theMove();
    }
}
