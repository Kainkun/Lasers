using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovement : AiAction
{
    public float speed;
    [HideInInspector]
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
