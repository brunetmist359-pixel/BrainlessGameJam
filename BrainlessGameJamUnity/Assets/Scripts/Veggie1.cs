using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Veggie1 : MonoBehaviour
{
    // Defining Variables
    public float speed;
    private Rigidbody2D rb;

    public List<Transform> waypoints;
    

    // pathfinding variables
    private int CurrentMovePoint = 0;

    // assigning object components to variable
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    // Move from right to left
    void FixedUpdate()
    {
        if (waypoints == null || waypoints.Count == 0) return;

        Transform target = waypoints[CurrentMovePoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        
        Vector3 direction = (target.position - transform.position).normalized;
       

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            CurrentMovePoint++;
            if (CurrentMovePoint >= waypoints.Count)
            {
                DestroySelf();
            }
        }
    }

    
    

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
