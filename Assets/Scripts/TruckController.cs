using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    public float speed = 2f;
    private bool move = false;
    public Rigidbody2D rb;

    private void Awake()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void StartTruck()
    {
        move = true;
    }

    public void StopTruck()
    {
        move = false;
    }
    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (move)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }
}
