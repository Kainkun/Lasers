using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AiAction : MonoBehaviour
{
    public UnityEvent onActionEnd;
    public virtual IEnumerator ActionStart()
    {
        yield break;
    }
    
    public virtual void ActionTick()
    {
        
    }
    
    public virtual IEnumerator ActionEnd()
    {
        onActionEnd.Invoke();
        yield break;
    }
}
