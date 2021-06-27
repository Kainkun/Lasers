using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDash : AiMovement
{
    public float chargeupTime;
    public float recoveryTime;
    
    public override IEnumerator ActionStart()
    {
        StartCoroutine(base.ActionStart());

        yield return new WaitForSeconds(chargeupTime);

        Transform player = GameManager.instance.player.transform;
        Vector2 directionToPlayer = ((Vector2)player.position - (Vector2)transform.position).normalized;
        rb.AddForce(directionToPlayer * speed, ForceMode2D.Impulse);

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
