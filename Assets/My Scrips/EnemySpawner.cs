using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject demonPrefab; // Reference to the enemy prefab
    public GameObject cyclopsPrefab; // Reference to the enemy prefab
    public GameObject nueNIPrefab; // Reference to the enemy prefab

    public Transform spawnPosition; // Position where enemies will be spawned

    public float spawnRate = 2f; // Time interval between spawns

    public int maxWaves = 10;
    public float waveInterval = 60f;
    public int currentWave = 0;
    public float timer = 0f;
    public int enemySpawnCount = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //InvokeRepeating("SpawnEnemy", 1f, spawnRate); // Start spawning enemies at regular intervals    

        StartWave();
    }

    void Update()
    {
        if (currentWave >= maxWaves)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            StartWave();
        }

    }
    void StartWave()
    {
        currentWave++;
        GameManager.Instance.GetCurrentWave(currentWave);

        Debug.Log("Wave " + currentWave + " started!");

        // Spawn enemies for this wave
        SpawnEnemies();

        // Reset timer to 60 seconds
        timer = waveInterval;
    }
    void SpawnEnemies() 
    {
        
        if(currentWave <= 2)
        {
            for (int i = 0; i < currentWave + enemySpawnCount; i++)
            {
                SpawnEnemy(demonPrefab, i);
            }
        }
        else if(currentWave <= 5)
        {
            for(int i = 0;i < currentWave + enemySpawnCount; i++)
            {
                SpawnEnemy(demonPrefab, i);
            }
            for (int i = 0; i < currentWave + enemySpawnCount; i++)
            {
                SpawnEnemy(nueNIPrefab, i);
            }
        }
        else if(currentWave <= 10)
        {
            for (int i = 0; i < currentWave + enemySpawnCount; i++)
            {
                SpawnEnemy(demonPrefab, i);

            }
            for (int i = 0; i < currentWave + enemySpawnCount; i++)
            {
                SpawnEnemy(nueNIPrefab, i);
            }
            for (int i = 0; i < currentWave + enemySpawnCount; i++)
            {
                SpawnEnemy(cyclopsPrefab, i);
            }
        }
    }

    void SpawnEnemy(GameObject enemy, int index)
    {
        float randomX = Random.Range(1, -1);
        float randomY = Random.Range(1, 10);

        Instantiate(enemy, spawnPosition.position + new Vector3(randomX, randomY, 0), Quaternion.identity);
    }
}
