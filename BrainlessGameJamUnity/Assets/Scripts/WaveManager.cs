using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Allows other scripts to call functions on this script
    public static WaveManager Instance;

    //List of variables for player stats
    public int PlayerHealth;

    // List of variables to keep track of game state
    public int WaveCount;
    public bool WaveInProgress;
    public int Enemycount;
    public bool gameover;

    // List of needed variables for UI
    public GameObject gameoverPanel;

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
        if(Input.GetKeyDown(KeyCode.Space) && WaveInProgress == false)
        {
            StartCoroutine(StartWave(WaveCount));
        }
        if (Enemycount > 0) WaveInProgress = true; else WaveInProgress = false;

        if (PlayerHealth <= 0) GameOver();

        if (gameover && Input.GetKeyDown(KeyCode.P)) RestartGame();
     
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
        WaveInProgress = true;
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
            Enemycount++;
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

    // for detecting and managing wave states
    public void OnDeath()
    {
        Enemycount--;
        
    }

    // defines startup conditions for awake and game resets.
    public void Initialize()
    {
        Instance = this;

        SpawnPoints.Add(SpawnPoint1);
        SpawnPoints.Add(SpawnPoint2);
        SpawnPoints.Add(SpawnPoint3);

        Enemycount = 0;
        WaveCount = 1;
        PlayerHealth = 10;
        WaveInProgress = false;
    }

    // Called to initiate GameOver
    public void GameOver()
    {
        gameover = true;
        gameoverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // Called to restart the game
    public void RestartGame()
    {
        Time.timeScale = 1f;
        gameoverPanel.SetActive(false);
        Initialize();
    }
        

}
