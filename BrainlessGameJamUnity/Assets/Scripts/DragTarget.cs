using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTarget : MonoBehaviour
{
    public Nugget nugget;

    private Vector3 dragStartPos;
    private Vector3 dragDelta;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        dragStartPos = Input.mousePosition;
        dragDelta = Vector3.zero;
    }

    void OnMouseUp()
    {
        if (dragDelta.magnitude > 0) {
            nugget.Launch(dragDelta);
        }
    }

    void OnMouseDrag()
    {
        dragDelta = dragStartPos - Input.mousePosition;
        //Debug.Log($"drag delta: {dragDelta.x} {dragDelta.y}.");
    }
}
