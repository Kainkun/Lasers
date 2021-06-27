using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject deathPs;

    [HideInInspector]
    public SpriteRenderer sr;

    [HideInInspector]
    public Rigidbody2D rb;

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void TakeDamage(float amount)
    {
        if (!hitEffectiveActive)
            StartCoroutine(HitEffect());
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        if (deathPs)
            Instantiate(deathPs, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private bool hitEffectiveActive;

    IEnumerator HitEffect()
    {
        hitEffectiveActive = true;

        Color c = sr.color;

        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = c;
        yield return new WaitForSeconds(0.1f);

        hitEffectiveActive = false;
    }
}