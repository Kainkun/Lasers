using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : Entity
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Die();
    }
}
