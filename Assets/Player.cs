using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    public float moveSpeed;
    public Vector2 moveDirection;
    public Vector2 lookDirection;
    public Laser laser;
    
    private Vector2 mousePosition;
    private Vector2 mouseWorldPosition;
    private bool firing;
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
        if(firing)
            Firing();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext callbackContext) => moveDirection = callbackContext.ReadValue<Vector2>();

    public void OnLook(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.control.device == Mouse.current) //mouse look
        {
            mousePosition = callbackContext.ReadValue<Vector2>();
            mouseWorldPosition = GameManager.instance.mainCamera.ScreenToWorldPoint(mousePosition);
            lookDirection = (mouseWorldPosition - (Vector2)transform.position);
        }
        else //controller look
        {
            lookDirection = callbackContext.ReadValue<Vector2>();
        }
    }



    public void OnFire(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            firing = true;
            FireStart();
        }
        else if (callbackContext.canceled)
        {
            firing = false;
            FireStop();
        }
    }
        

    void Move()
    {
        rb.MovePosition(rb.position + moveDirection * (Time.fixedDeltaTime * moveSpeed)); 
        
        mouseWorldPosition = GameManager.instance.mainCamera.ScreenToWorldPoint(mousePosition);
        lookDirection = (mouseWorldPosition - (Vector2)transform.position);
    }

    void Look()
    {
        transform.right = lookDirection;
    }

    void FireStart()
    {
        laser.LaserStart();
    }

    void Firing()
    {
        laser.LaserTick();
    }
    
    void FireStop()
    {
        laser.LaserStop();
    }
}