using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDash : AiMovement
{
    public override IEnumerator ActionStart()
    {
        StartCoroutine(base.ActionStart());

        print("dash start");

        yield return new WaitForSeconds(1);

        StartCoroutine(ActionEnd());
        yield break;
    }
    
    public override void ActionTick()
    {
        base.ActionTick();
        
        print("dash tick");
    }

    public override IEnumerator ActionEnd()
    {
        print("dash end");
        StartCoroutine(base.ActionEnd());
        yield break;
    }
}
