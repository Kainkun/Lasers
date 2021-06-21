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
            Vector2 mousePosition = callbackContext.ReadValue<Vector2>();
            Vector2 mouseWorldPosition = GameManager.instance.mainCamera.ScreenToWorldPoint(mousePosition);
            lookDirection = (mouseWorldPosition - (Vector2)transform.position);
        }
        else //controller look
        {
            lookDirection = callbackContext.ReadValue<Vector2>();
        }
    }



    public void OnFire(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
            Fire();
    }
        

    void Move()
    {
        rb.position += moveDirection * (Time.fixedDeltaTime * moveSpeed);
    }

    void Look()
    {
        transform.right = lookDirection;
    }

    void Fire()
    {
        print("pew");
    }
}