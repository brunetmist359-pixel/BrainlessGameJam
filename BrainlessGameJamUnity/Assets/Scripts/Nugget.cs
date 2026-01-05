using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nugget : MonoBehaviour
{
    private Rigidbody2D rb;

    public Vector2 startPosition;
    public float launchForce;
    public Vector2 launchDir;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetForce();
            rb.AddForce(launchDir * launchForce, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ResetForce();
            ResetPosition();
        }
    }

    private void ResetForce()
    {
        // Clear linear and angular velocity
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        // Optional: Put the Rigidbody to sleep to prevent minor forces (like gravity over a single frame) 
        // from immediately affecting it again, ensuring it stays still until a new force is applied.
        //rb.Sleep();
    }

    private void ResetPosition()
    {
        // Reset Z-rotation and position
        rb.rotation = 0;
        rb.position = startPosition;
    }
}