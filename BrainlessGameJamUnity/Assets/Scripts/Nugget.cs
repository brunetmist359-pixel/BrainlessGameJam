using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nugget : MonoBehaviour
{
    private Rigidbody2D rb;

    public float launchForce;
    public Vector2 launchDir;

    void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClearVelocity();

            // FIXME should be framerate agnostic
            rb.position = new Vector2(0, 0);
            rb.AddForce(launchDir * launchForce, ForceMode2D.Impulse);
        }
    }
    
    private void ClearVelocity()
    {
        // Clear linear and angular velocity
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        
        // Optional: Put the Rigidbody to sleep to prevent minor forces (like gravity over a single frame) 
        // from immediately affecting it again, ensuring it stays still until a new force is applied.
        rb.Sleep();
    }
}
