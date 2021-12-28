using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyEasecontroller : MonoBehaviour
{
    private Rigidbody rb;
    public float accelerationTime = 1f;
    public float maxSpeed = 4f;
    private Vector3 movement;
    private float timeLeft;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            movement = new Vector3(Random.Range(-1f, 1f), 0.0f, Random.Range(-1f, 1f));
            timeLeft += accelerationTime;
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(movement * maxSpeed);
    }
}