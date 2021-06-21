using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : BasicEnemy
{
    public GameObject projectile;
    public float projectileSpeed;

    void Start()
    {
        StartCoroutine(ShootLoop());
    }

    void Update()
    {
    }

    IEnumerator ShootLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            GameObject tempProjectile = Instantiate(projectile, (Vector2)transform.position + directionToPlayer, Quaternion.identity);
            tempProjectile.GetComponent<Rigidbody2D>().velocity = directionToPlayer * projectileSpeed;
            tempProjectile.GetComponent<Projectile>().damage = damage;

            yield return null;
        }
    }
}