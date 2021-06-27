using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class Projectile : Entity
{
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float speed;
    private void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(player)
            player.TakeDamage(damage);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (Vector2)transform.right * speed * Time.fixedDeltaTime);
    }
}
