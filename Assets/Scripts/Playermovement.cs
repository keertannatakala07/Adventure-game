using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    private Vector2 movement;

    public void Initialize(Rigidbody2D rb, float speed)
    {
        this.rb = rb;
        this.speed = speed;
    }

    public void userMove(Vector2 input)
    {
        movement = input.normalized;
    }

    public void theMove()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
