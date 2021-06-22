using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    Rigidbody2D rb;

    public float moveSpeed;
    public Vector2 moveDirection;
    public Vector2 lookDirection;
    public Laser laser;
    public float maxCharge;
    public float charge;

    private Vector2 mousePosition;
    private Vector2 mouseWorldPosition;
    private bool firing;
    private InputDevice lastLookDevice;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
    }

    void Update()
    {
        Look();
        if (firing)
            Firing();
        else
        {
            charge += Time.deltaTime;
            charge = Mathf.Min(charge, maxCharge);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext callbackContext) => moveDirection = callbackContext.ReadValue<Vector2>();

    public void OnLook(InputAction.CallbackContext callbackContext)
    {
        lastLookDevice = callbackContext.control.device;
        if (lastLookDevice == Mouse.current) //mouse look
        {
            mousePosition = callbackContext.ReadValue<Vector2>();
            mouseWorldPosition = GameManager.instance.mainCamera.ScreenToWorldPoint(mousePosition);
            lookDirection = (mouseWorldPosition - (Vector2) transform.position);
        }
        else //controller look
        {
            Vector2 v = callbackContext.ReadValue<Vector2>();
            if (v.magnitude > 0.1f)
                lookDirection = callbackContext.ReadValue<Vector2>();
        }
    }


    public void OnFire(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            FireStart();
        }
        else if (callbackContext.canceled)
        {
            FireStop();
        }
    }


    void Move()
    {
        rb.MovePosition(rb.position + moveDirection * (Time.fixedDeltaTime * moveSpeed));

        if (lastLookDevice == Mouse.current)
        {
            mouseWorldPosition = GameManager.instance.mainCamera.ScreenToWorldPoint(mousePosition);
            lookDirection = (mouseWorldPosition - (Vector2) transform.position);
        }
    }

    void Look()
    {
        transform.right = lookDirection;
    }

    void FireStart()
    {
        if (charge > 0)
        {
            firing = true;
            laser.LaserStart();
        }
    }

    void Firing()
    {
        if (charge > 0)
        {
            charge -= Time.deltaTime;
            charge = Mathf.Max(charge, 0);
            laser.LaserTick();
        }
        else if(firing)
        {
            FireStop();
        }
    }

    void FireStop()
    {
        firing = false;
        laser.LaserStop();
    }
}