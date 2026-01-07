using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int PointRating;
    public float speed;

    public void DestroySelf()
    {
        WaveManager.Instance.OnDeath();
        Destroy(gameObject);
    }
}
