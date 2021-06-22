using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    Rigidbody2D _rb;

    public float moveSpeed;
    public Vector2 moveDirection;
    public Vector2 lookDirection;
    public Laser laser;
    public float maxCharge;
    public float _charge;

    public float Charge
    {
        get => _charge;
        set
        {
            _charge = Mathf.Clamp(value, 0, maxCharge);
            GameManager.instance.chargeBar.SetPercent(_charge / maxCharge);
        }
    }

    private Vector2 _mousePosition;
    private Vector2 _mouseWorldPosition;
    private bool _firing;
    private InputDevice _lastLookDevice;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
    }

    void Update()
    {
        Look();
        if (_firing)
            Firing();
        else
            Charge += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext callbackContext) => moveDirection = callbackContext.ReadValue<Vector2>();

    public void OnLook(InputAction.CallbackContext callbackContext)
    {
        _lastLookDevice = callbackContext.control.device;
        if (_lastLookDevice == Mouse.current) //mouse look
        {
            _mousePosition = callbackContext.ReadValue<Vector2>();
            _mouseWorldPosition = GameManager.instance.mainCamera.ScreenToWorldPoint(_mousePosition);
            lookDirection = (_mouseWorldPosition - (Vector2) transform.position);
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
        _rb.MovePosition(_rb.position + moveDirection * (Time.fixedDeltaTime * moveSpeed));

        if (_lastLookDevice == Mouse.current)
        {
            _mouseWorldPosition = GameManager.instance.mainCamera.ScreenToWorldPoint(_mousePosition);
            lookDirection = (_mouseWorldPosition - (Vector2) transform.position);
        }
    }

    void Look()
    {
        transform.right = lookDirection;
    }

    void FireStart()
    {
        if (Charge > 0)
        {
            _firing = true;
            laser.LaserStart();
        }
    }

    void Firing()
    {
        if (Charge > 0)
        {
            Charge -= Time.deltaTime;
            laser.LaserTick();
        }
        else if (_firing)
        {
            FireStop();
        }
    }

    void FireStop()
    {
        _firing = false;
        laser.LaserStop();
    }
}