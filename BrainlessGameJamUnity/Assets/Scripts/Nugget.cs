using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Nugget : MonoBehaviour
{
    private Rigidbody2D rb;

    public Vector2 startPosition;
    public Vector2 launchDir;

    public float launchMultiplier;
    public float maxLaunchForce;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Transform>().position = new Vector3(startPosition[0], startPosition[1], 0);
        //rb.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Launch(launchDir);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ResetForce();
            ResetPosition();
        }
    }

    public Vector3 GetLaunchVec(Vector3 vec)
    {
        float force = Math.Min(vec.magnitude * launchMultiplier, maxLaunchForce);
        return vec.normalized * force;
    }

    public void Launch(Vector3 vec)
    {
        ResetForce();
        rb.AddForce(GetLaunchVec(vec));
    }

    void ResetForce()
    {
        // Clear linear and angular velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;

        // Optional: Put the Rigidbody to sleep to prevent minor forces (like gravity over a single frame) 
        // from immediately affecting it again, ensuring it stays still until a new force is applied.
        //rb.Sleep();
    }

    void ResetPosition()
    {
        // Reset Z-rotation and position
        rb.rotation = 0;
        rb.position = startPosition;
    }

        // Destroys veggies on contact
    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            WaveManager.Instance.OnDeath();
            Destroy(other.gameObject);
        }
    }
}