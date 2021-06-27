using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer lineRenderer;
    private LayerMask layerMask;

    public ParticleSystem psStart;
    public ParticleSystem psEnd;
    public ParticleSystem psHitEnemy;

    public AudioSource soundStart;
    public AudioSource soundDuring;
    public AudioSource soundEnd;

    public float width;
    public float maxDistance;
    public float damagePerSecond;
    public float pushForce;

    private bool isHittingEnemy;
    private bool isLaserOn;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        layerMask = ~LayerMask.GetMask("LaserTransparent");
        lineRenderer.widthMultiplier = width;
    }

    private void Start()
    {
    }

    public void LaserStart()
    {
        isLaserOn = true;
        lineRenderer.enabled = true;
        psStart.Play();
        psEnd.Play();
        soundStart.Play();
        soundDuring.Play(); 
        GameManager.instance.SetCamShakeAmplitude(1);
    }

    public void LaserTick()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, transform.right, maxDistance, layerMask);
        if (raycastHit2D)
        {
            lineRenderer.SetPosition(1, Vector3.right * (raycastHit2D.distance + width / 2));
            psEnd.transform.localPosition = Vector3.right * (raycastHit2D.distance + width / 2);
            psHitEnemy.transform.localPosition = Vector3.right * (raycastHit2D.distance + width / 2);
            Entity entity = raycastHit2D.collider.GetComponent<Entity>();
            if (entity)
            {
                if (!isHittingEnemy)
                {
                    soundDuring.pitch = 2f;
                    isHittingEnemy = true;
                    psHitEnemy.Play();
                }

                entity.TakeDamage(damagePerSecond * Time.deltaTime);
                entity.rb.AddForce(transform.right * pushForce, ForceMode2D.Force);
            }
            else
            {
                if (isHittingEnemy)
                {
                    soundDuring.pitch = 1f;
                    isHittingEnemy = false;
                    psHitEnemy.Stop();
                }
            }
        }
        else
        {
            lineRenderer.SetPosition(1, Vector3.right * (maxDistance + width / 2));
            psEnd.transform.localPosition = Vector3.right * (maxDistance + width / 2);
            psHitEnemy.transform.localPosition = Vector3.right * (maxDistance + width / 2);
        }
    }

    public void LaserStop()
    {
        if (!isLaserOn)
            return;

        isLaserOn = false;
        lineRenderer.enabled = false;
        psStart.Stop();
        psEnd.Stop();
        psHitEnemy.Stop();
        soundDuring.Stop();
        soundEnd.Play();
        GameManager.instance.SetCamShakeAmplitude(0);
    }
}