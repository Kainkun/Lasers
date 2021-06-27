using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWait : AiAction
{
    public float time;
    
    public override IEnumerator ActionStart()
    {
        StartCoroutine(base.ActionStart());

        yield return new WaitForSeconds(time);
        StartCoroutine(ActionEnd());
        yield break;
    }
}
