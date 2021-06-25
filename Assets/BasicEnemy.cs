using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BasicEnemy : Entity
{

    public float comfortDistance;
    public float comfortDistanceTolerance;
    public float speed;
    public float damage;
    

    protected Vector2 playerPosition;
    protected Vector2 directionToPlayer;

    protected override void Awake()
    {
        base.Awake();
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

    public override void Die()
    {
        if(deathPs)
            Instantiate(deathPs, transform.position, Quaternion.identity);
        GetComponent<Collider2D>().enabled = false;
        Color c = sr.color;
        c.a = 0.2f;
        sr.color = c;
        GameManager.instance.FreezeFrame(0.08f);
        Destroy(this);
    }
}