using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float speed = 6f;
    private bool move = false;
    private bool hasPlayedSFX = false;

    public Rigidbody2D rb;
    public AudioClip vehicleSFX;
    private AudioSource audioSource;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartVehicle()
    {
        move = true;
        hasPlayedSFX = false; 
    }

    public void StopVehicle()
    {
        move = false;
        hasPlayedSFX = false; 
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (move)
        {
            if (!hasPlayedSFX && vehicleSFX != null && audioSource != null)
            {
                audioSource.PlayOneShot(vehicleSFX);
                hasPlayedSFX = true;
            }

            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }
}

