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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void MoveInput(InputAction.CallbackContext callbackContext)
    {
        moveDirection = callbackContext.ReadValue<Vector2>();
    }

    void Move()
    {
        rb.position += moveDirection;
    }

    public void Shoot()
    {
    }
}