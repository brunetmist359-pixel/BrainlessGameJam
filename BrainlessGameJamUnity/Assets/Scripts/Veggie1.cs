using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Veggie1 : MonoBehaviour
{
    // Defining Variables
    public float speed;
    private Rigidbody2D rb;
    
    
    // assigning object components to variable
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.WakeUp();
        Debug.Log("start has ran");
    }

    // Move from right to left
    void FixedUpdate()
    {
        rb.velocity = Vector2.left * speed;
        Debug.Log("Update is running");
    }

    // collision checks 
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Nugget"))
        {
            DestroySelf();
        }
                
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
