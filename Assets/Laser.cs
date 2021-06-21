using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer lineRenderer;
    private LayerMask layerMask;
    private ParticleSystem ps;

    public float width;
    public float maxDistance;
    public float damagePerSecond;
    

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        layerMask = ~LayerMask.GetMask("LaserTransparent");
        lineRenderer.widthMultiplier = width;
        ps = GetComponentInChildren<ParticleSystem>();
    }

    public void LaserStart()
    {
        lineRenderer.enabled = true;
        ps.Play();
    }

    public void LaserTick()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, transform.right, maxDistance, layerMask);
        if (raycastHit2D)
        {
            lineRenderer.SetPosition(1, Vector3.right * (raycastHit2D.distance + width/2));
            ps.transform.localPosition = Vector3.right * (raycastHit2D.distance + width / 2);
            Entity entity = raycastHit2D.collider.GetComponent<Entity>();
            if (entity)
            {
                entity.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, Vector3.right * (maxDistance + width/2));
            ps.transform.localPosition = Vector3.right * (maxDistance + width/2);
        }
    }

    public void LaserStop()
    {
        lineRenderer.enabled = false;
        ps.Stop();
    }
}
