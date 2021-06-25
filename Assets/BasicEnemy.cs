using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Entity
{
    private Rigidbody2D rb;

    public float comfortDistance;
    public float comfortDistanceTolerance;
    public float speed;
    public float damage;

    protected Vector2 playerPosition;
    protected Vector2 directionToPlayer;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (GameManager.instance.player != null)
            playerPosition = (Vector2) GameManager.instance.player.transform.position;
        directionToPlayer = (playerPosition - (Vector2) transform.position).normalized;

        GoToComfortDistance();
    }

    void GoToComfortDistance()
    {
        float distanceToPlayer = Vector2.Distance(playerPosition, transform.position);
        if (distanceToPlayer > (comfortDistance + comfortDistanceTolerance))
            rb.MovePosition(rb.position + (Time.fixedDeltaTime * speed * directionToPlayer));
        else if (distanceToPlayer < (comfortDistance - comfortDistanceTolerance))
            rb.MovePosition(rb.position + (Time.fixedDeltaTime * speed * -directionToPlayer));
    }
}