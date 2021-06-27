using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiIf : AiAction
{
    public AiAction actionIfTrue;
    public AiAction actionIfFalse;
    public override IEnumerator ActionStart()
    {
        nextAction = TestStatement() ? actionIfTrue : actionIfFalse;
        
        StartCoroutine(ActionEnd());
        yield break;
    }


    public abstract bool TestStatement();
}
