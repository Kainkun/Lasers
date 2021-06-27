using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIfPlayerLineOfSight : AiIf
{
    public float maxDistance;
    public LayerMask layerMask;
    
    public override bool TestStatement()
    {
        Transform player = GameManager.instance.player.transform;
        Vector2 directionToPlayer = ((Vector2)player.position - (Vector2)transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, maxDistance, layerMask);
        
        if (hit && hit.transform.GetComponent<Player>())
            return true;
        else
            return false;
    }
}
