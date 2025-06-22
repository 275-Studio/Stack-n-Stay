using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    public float speed = 2f;
    private bool move = false;
    public Rigidbody2D rb;

    public void StartTruck()
    {
        move = true;
    }
    void Update()
    {
        if (move)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }
}
