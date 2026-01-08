using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchVisualizer : MonoBehaviour
{
    public static int simulationSteps = 1000;
    static GameObject clone = null;

    public static void StartVisualization(GameObject obj)
    {
        obj.GetComponent<Rigidbody2D>().simulated = false;
        obj.GetComponent<BoxCollider2D>().enabled = false;

        clone = Instantiate(obj, obj.transform.position, Quaternion.identity);
        clone.GetComponent<Rigidbody2D>().simulated = true;
        clone.GetComponent<BoxCollider2D>().enabled = true;
        Physics2D.simulationMode = SimulationMode2D.Script;
    }

    // FIXME this breaks if obj has a translate or scale applied
    public static void DrawVisualization(GameObject obj, Vector3 f)
    {
        var rb = clone.GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        rb.AddForce(f);

        var pts = new List<Vector3>();
        for (int i = 1; i < simulationSteps; i++) {
            Physics2D.Simulate(Time.fixedDeltaTime);
            pts.Add(rb.transform.position);
        }

        clone.transform.position = obj.transform.position;
        clone.transform.rotation = obj.transform.rotation;

        // Draw
        LineRenderer lr = clone.GetComponent<LineRenderer>();
        lr.enabled = true;
        lr.positionCount = pts.Count;
        lr.SetPositions(pts.ToArray());
    }

    public static void StopVisualization(GameObject obj)
    {
        obj.GetComponent<Rigidbody2D>().simulated = true;
        obj.GetComponent<BoxCollider2D>().enabled = true;
        
        Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
        Destroy(clone);
    }
}
