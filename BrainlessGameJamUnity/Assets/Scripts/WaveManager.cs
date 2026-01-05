using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject Veggie1prefab;

    // Defining possible spawn locations
    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    public GameObject SpawnPoint3;

    //create List for pathfinding
    public List<Transform> TopPathNodes;
    public List<Transform> MiddlePathNodes;
    public List<Transform> BottomPathNodes;

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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Spawn(Veggie1prefab);
        }
    }

    // Spawns a veggie at a random spawn point every time its called
    public void Spawn(GameObject enemytype)
    {
        List<Transform> SelectedPath = TopPathNodes;

        int randomindex = Random.Range(0, SpawnPoints.Count);
        GameObject spawnpoint = SpawnPoints[randomindex];

        if (randomindex == 0) SelectedPath = TopPathNodes;
        else if (randomindex == 1) SelectedPath = MiddlePathNodes;
        else if (randomindex == 2) SelectedPath = BottomPathNodes;

        GameObject enemy = Instantiate(enemytype, spawnpoint.transform.position, Quaternion.identity);
        enemy.GetComponent<Veggie1>().waypoints = SelectedPath;
    }
}
