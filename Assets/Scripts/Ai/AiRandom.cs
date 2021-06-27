using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class AiRandom : AiAction
{
    public List<Probability> probabilities;

    [Serializable]
    public class Probability
    {
        public AiAction action;
        public float proportion;
    }

    private AiAction chosenAction;
    
    public override IEnumerator ActionStart()
    {
        chosenAction = GetRandomAction(probabilities);
        nextAction = chosenAction.nextAction;
        chosenAction.onActionEnd.AddListener(onActionEnd.Invoke);
        
        StartCoroutine(chosenAction.ActionStart());
        yield break;
    }
    
    public override void ActionTick()
    {
        chosenAction.ActionTick();
    }

    public override IEnumerator ActionEnd()
    {
        yield break;
    }

    public AiAction GetRandomAction(List<Probability> probabilityList)
    {
        float total = 0;
        foreach (Probability probability in probabilityList)
        {
            total += probability.proportion;
        }
        float chosen = Random.Range(0f, total);
        float count = 0;
        foreach (Probability probability in probabilityList)
        {
            count += probability.proportion;
            if (chosen <= count)
                return probability.action;
        }

        Debug.LogError("this shouldn't call");
        return null;
    }
}
