using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Allows other scripts to call functions on this script
    public static WaveManager instance;


    // List of variables to keep track of game state
    public int WaveCount;

    // List of variables containing prefabs for enemies
    public List<Enemy> EnemyTypes = new List<Enemy>();

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

    
    
 
    // Assign SpawnPoints to list and initialize important variables
    void Awake()
    {
        Initialize();
    }

    // Will eventually be in charge of calling and updating wavestate
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartWave(WaveCount));
        }
        Debug.Log(WaveCount);
    }

    // Spawns a veggie at a random spawn point every time its called
    public void Spawn(Enemy enemytype)
    {
        List<Transform> SelectedPath = TopPathNodes;

        int randomindex = Random.Range(0, SpawnPoints.Count);
        GameObject spawnpoint = SpawnPoints[randomindex];

        if (randomindex == 0) SelectedPath = TopPathNodes;
        else if (randomindex == 1) SelectedPath = MiddlePathNodes;
        else if (randomindex == 2) SelectedPath = BottomPathNodes;

        Enemy enemy = Instantiate(enemytype, spawnpoint.transform.position, Quaternion.identity);
        enemy.GetComponent<Veggie1>().waypoints = SelectedPath;
    }

    // called at the start of each wave in order to spawn enemies
    public IEnumerator StartWave(int wavecount)
    {
        int pointsallowed = (wavecount * 10);
        WaveCount++;



        while (pointsallowed > 0)
        {
            Enemy chosentype = ChooseEnemyType(pointsallowed);

            if (chosentype == null)
            {
                
                break;
            }
                

            Spawn(chosentype);
            pointsallowed -= chosentype.PointRating;

            yield return new WaitForSeconds(0.5f);
        }
    }
    
    // called by StartWave to determine the type of enemy spawned according to budget remaining
    private Enemy ChooseEnemyType(int points)
    {
        int randomindex = Random.Range(0, EnemyTypes.Count);
        Enemy chosenenenemy = EnemyTypes[randomindex];

        if (chosenenenemy.PointRating <= points)
        {
            return chosenenenemy;
        }
        else return null;
    }

    // defines startup conditions for awake and game resets.
    public void Initialize()
    {
        SpawnPoints.Add(SpawnPoint1);
        SpawnPoints.Add(SpawnPoint2);
        SpawnPoints.Add(SpawnPoint3);


        WaveCount = 1;
    }
}
