using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMoveToPlayer : AiMovement
{
    public override IEnumerator ActionStart()
    {
        StartCoroutine(base.ActionStart());
        
        print("mtp start");

        yield return new WaitForSeconds(1);

        StartCoroutine(ActionEnd());
        yield break;
    }
    
    public override void ActionTick()
    {
        base.ActionTick();
        
        print("mtp tick");
    }

    public override IEnumerator ActionEnd()
    {
        print("mtp end");
        StartCoroutine(base.ActionEnd());
        yield break;
    }
}
