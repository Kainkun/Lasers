using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AiBrain : MonoBehaviour
{
    public bool isLooping = true;
    public List<AiAction> actions = new List<AiAction>();
    public int currentActionIndex;

    public void Start()
    {
        StartAction(currentActionIndex);
    }

    private UnityEvent currentTicker;
    private void Update()
    {
        currentTicker?.Invoke();
    }

    public void StartAction(int index)
    {
        currentActionIndex = index;
        actions[currentActionIndex].onActionEnd.AddListener(FinishedAction);
        currentTicker.AddListener(actions[currentActionIndex].ActionTick);
        StartCoroutine(actions[currentActionIndex].ActionStart());
    }
    
    public void FinishedAction()
    {
        currentTicker.RemoveListener(actions[currentActionIndex].ActionTick);
        actions[currentActionIndex].onActionEnd?.RemoveListener(FinishedAction);
        
        if (isLooping)
            currentActionIndex = (currentActionIndex + 1) % actions.Count;
        else if (currentActionIndex < (actions.Count - 1))
            currentActionIndex++;
        else
            return;
        
        StartCoroutine(C_StartNextAction(currentActionIndex));
    }
    
    IEnumerator C_StartNextAction(int index)
    {
        yield return new WaitForEndOfFrame();
        StartAction(index);
    }
}