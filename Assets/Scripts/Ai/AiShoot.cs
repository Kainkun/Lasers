using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiShoot : AiAttack
{
    public float chargeupTime;
    public float recoveryTime;
    public GameObject projectile;
    public float projectileSpeed;
    
    public override IEnumerator ActionStart()
    {
        StartCoroutine(base.ActionStart());

        yield return new WaitForSeconds(chargeupTime);

        Transform player = GameManager.instance.player.transform;
        Vector2 directionToPlayer = ((Vector2)player.position - (Vector2)transform.position).normalized;
        GameObject tempProjectile = Instantiate(projectile, (Vector2)transform.position + directionToPlayer, Quaternion.identity);
        tempProjectile.transform.right = directionToPlayer;
        tempProjectile.GetComponent<Projectile>().damage = damage;
        tempProjectile.GetComponent<Projectile>().speed = projectileSpeed;

        yield return new WaitForSeconds(recoveryTime);
        StartCoroutine(ActionEnd());
        yield break;
    }
    
    public override void ActionTick()
    {
        base.ActionTick();
    }

    public override IEnumerator ActionEnd()
    {
        StartCoroutine(base.ActionEnd());
        yield break;
    }
}