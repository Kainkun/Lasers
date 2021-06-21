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
    public float rotateSpeed;
    
    private Vector2 playerPosition;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    

    private void FixedUpdate()
    {
        playerPosition = (Vector2) GameManager.instance.player.transform.position;

        GoToComfortDistance();
    }

    void GoToComfortDistance()
    {
        float distanceToPlayer = Vector2.Distance(playerPosition, transform.position);
        if(distanceToPlayer  > (comfortDistance + comfortDistanceTolerance))
            rb.MovePosition(rb.position + (Time.fixedDeltaTime * speed * (playerPosition - (Vector2)transform.position).normalized));
        else if(distanceToPlayer  < (comfortDistance - comfortDistanceTolerance))
            rb.MovePosition(rb.position + (Time.fixedDeltaTime * speed * ((Vector2)transform.position - playerPosition).normalized));
    }
}
