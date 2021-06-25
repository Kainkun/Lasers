using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    private SpriteRenderer sr;

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float amount)
    {
        if(!hitEffectiveActive)
            StartCoroutine(HitEffect());
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    public void Die()
    {
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