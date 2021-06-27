using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMoveToPlayer : AiMovement
{
    private Transform player;
    public float timeToMove;
    
    public override IEnumerator ActionStart()
    {
        StartCoroutine(base.ActionStart());

        player = GameManager.instance.player.transform;

        yield return new WaitForSeconds(timeToMove);
        StartCoroutine(ActionEnd());
        yield break;
    }
    
    public override void ActionTick()
    {
        base.ActionTick();
        Vector2 directionToPlayer = ((Vector2)player.position - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + (Time.fixedDeltaTime * speed * directionToPlayer));
    }

    public override IEnumerator ActionEnd()
    {
        StartCoroutine(base.ActionEnd());
        yield break;
    }
}
