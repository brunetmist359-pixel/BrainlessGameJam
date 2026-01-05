using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject Veggie1prefab;
    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    public GameObject SpawnPoint3;

    //create list for spawnpoints to live in
    public List<GameObject> SpawnPoints = new List<GameObject>();

    // Assign SpawnPoints to list
    void Awake()
    {
        SpawnPoints.Add(SpawnPoint1);
        SpawnPoints.Add(SpawnPoint2);
        SpawnPoints.Add(SpawnPoint3);
    }

    // Will eventually be in charge of calling and updating wavestate
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn(Veggie1prefab);
        }
    }

    // Spawns a veggie at a random spawn point every time its called
    public void Spawn(GameObject enemytype)
    {
        int randomindex = Random.Range(0, SpawnPoints.Count);
        GameObject spawnpoint = SpawnPoints[randomindex];

        Instantiate(enemytype, spawnpoint.transform.position, Quaternion.identity);
    }
}
