using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMoveRandomDirection : AiMovement
{
    private Vector2 direction;
    public float timeToMove;
    
    public override IEnumerator ActionStart()
    {
        StartCoroutine(base.ActionStart());

        direction = Random.insideUnitCircle.normalized;
        yield return new WaitForSeconds(timeToMove);
        StartCoroutine(ActionEnd());
        yield break;
    }
    
    public override void ActionTick()
    {
        base.ActionTick();
        rb.MovePosition(rb.position + (Time.fixedDeltaTime * speed * direction));
    }

    public override IEnumerator ActionEnd()
    {
        StartCoroutine(base.ActionEnd());
        yield break;
    }
}
