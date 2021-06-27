using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWaitRange : AiAction
{
    public float timeMin;
    public float timeMax;
    
    public override IEnumerator ActionStart()
    {
        StartCoroutine(base.ActionStart());

        yield return new WaitForSeconds(Random.Range(timeMin,timeMax));
        StartCoroutine(ActionEnd());
        yield break;
    }
}
