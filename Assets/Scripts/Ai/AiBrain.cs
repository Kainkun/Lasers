using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AiBrain : MonoBehaviour
{
    public AiAction startingAction;
    public AiAction currentAction;
    public void Start()
    {
        StartAction(startingAction);
    }

    private UnityEvent currentTicker = new UnityEvent();
    private void Update()
    {
        currentTicker?.Invoke();
    }

    public void StartAction(AiAction action)
    {
        currentAction = action;
        currentAction.onActionEnd.AddListener(FinishedAction);
        currentTicker.AddListener(currentAction.ActionTick);
        StartCoroutine(currentAction.ActionStart());
    }
    
    public void FinishedAction()
    {
        currentTicker.RemoveListener(currentAction.ActionTick);
        currentAction.onActionEnd?.RemoveListener(FinishedAction);
        
        if(currentAction.nextAction)
            StartCoroutine(C_StartNextAction(currentAction.nextAction));
    }
    
    IEnumerator C_StartNextAction(AiAction action)
    {
        yield return new WaitForEndOfFrame();
        StartAction(action);
    }
}