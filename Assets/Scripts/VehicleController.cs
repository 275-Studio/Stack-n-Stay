using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float speed = 6f;
    private bool move = false;
    public Rigidbody2D rb;

    private void Start()
    {
        transform.position = new Vector3(-3.4f, -0.5f, 0f);
    }

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    public void StartVehicle()
    {
        move = true;
    }

    public void StopVehicle()
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
