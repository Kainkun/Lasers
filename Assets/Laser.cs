using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer lineRenderer;
    private LayerMask layerMask;

    public float maxDistance;
    

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        layerMask = ~LayerMask.GetMask("LaserTransparent");
    }

    void Update()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, transform.right, maxDistance, layerMask);
        if(raycastHit2D)
            lineRenderer.SetPosition(1, Vector3.right * raycastHit2D.distance);
        else
            lineRenderer.SetPosition(1, Vector3.right * maxDistance);

    }
}
