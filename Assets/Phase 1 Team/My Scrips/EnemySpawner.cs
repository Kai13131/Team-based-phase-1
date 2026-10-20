using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject demonPrefab; // Reference to the enemy prefab
    public GameObject cyclopsPrefab; // Reference to the enemy prefab
    public GameObject nueNIPrefab; // Reference to the enemy prefab
    public GameObject bossPrefab;

    public Transform spawnPosition; // Position where enemies will be spawned
    public Transform spawnPosition_1;
    public Transform spawnPosition_2;
    public float spawnRate = 2f; // Time interval between spawns

    public int maxWaves = 10;
    public float waveInterval = 30f;
    public int currentWave = 0;
    public float timer = 0f;
    public int enemySpawnCount = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(WaveManager());
    }

    IEnumerator WaveManager()
    {
        while (currentWave < maxWaves)
        {
            currentWave++;

            // Tell GameManager the current wave
            GameManager.Instance.GetCurrentWave(currentWave);

            Debug.Log("Wave " + currentWave + " started!");

            // Start spawning enemies for this wave
            yield return StartCoroutine(SpawnWave());

            // Wait until the next wave
            if (currentWave < maxWaves)
            {
                Debug.Log("Wave " + currentWave + " finished. Next wave in 30 seconds.");

                yield return new WaitForSeconds(waveInterval);
            }
        }

        Debug.Log("All 10 waves completed!");
    }

    IEnumerator SpawnWave()
    {
        // Wave 1-2
        if (currentWave <= 2)
        {
            for (int i = 0; i < enemySpawnCount; i++)
            {
                SpawnEnemy(demonPrefab, spawnPosition, 0);

                yield return new WaitForSeconds(spawnRate);
            }
        }

        // Wave 3-4
        else if (currentWave <= 4)
        {
            for (int i = 0; i < enemySpawnCount; i++)
            {
                // Nue-Ni
                SpawnEnemy(nueNIPrefab, spawnPosition, 0);

                yield return new WaitForSeconds(spawnRate);

                // Demon
                SpawnEnemy_1(demonPrefab, spawnPosition_1, 1);

                yield return new WaitForSeconds(spawnRate);

                SpawnEnemy_1(demonPrefab, spawnPosition_1, 1);

                yield return new WaitForSeconds(spawnRate);
            }
        }

        // Wave 5-6
        else if (currentWave <= 6)
        {
            for (int i = 0; i < enemySpawnCount; i++)
            {
                // Nue-Ni
                SpawnEnemy_1(nueNIPrefab, spawnPosition_1, 1);

                yield return new WaitForSeconds(spawnRate);

                // Demon
                SpawnEnemy(demonPrefab, spawnPosition, 0);

                yield return new WaitForSeconds(spawnRate);

                // Cyclops
                SpawnEnemy_1(cyclopsPrefab, spawnPosition_1, 1);

                yield return new WaitForSeconds(spawnRate);
            }
        }

        // Wave 7-9
        else if (currentWave <= 9)
        {
            for (int i = 0; i < enemySpawnCount; i++)
            {
                // Nue-Ni
                SpawnEnemy_1(nueNIPrefab, spawnPosition_1, 1);

                yield return new WaitForSeconds(spawnRate);

                // Demon
                SpawnEnemy(demonPrefab, spawnPosition, 0);
                SpawnEnemy_2(demonPrefab, spawnPosition_2, 2);

                yield return new WaitForSeconds(spawnRate);

                // Cyclops
                SpawnEnemy_1(cyclopsPrefab, spawnPosition_1, 1);

                yield return new WaitForSeconds(spawnRate);
            }
        }

        // Wave 10
        else if (currentWave == 10)
        {
            // Boss spawns once
            SpawnEnemy_1(bossPrefab, spawnPosition_1, 1);

            Debug.Log("BOSS HAS SPAWNED!");
        }
    }

    void SpawnEnemy(GameObject enemy, Transform spawnPoint, int pathNumber)
    {

        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(1f, 10f);

        Vector3 spawnPosition = spawnPoint.position + new Vector3(randomX, randomY, 0f);

        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);

        EnemyMovement movement = newEnemy.GetComponent<EnemyMovement>();
        movement.pathNumber = pathNumber;
    }
    void SpawnEnemy_1(GameObject enemy, Transform spawnPoint, int pathNumber)
    {

        float randomX = Random.Range(1f, 10f);
        float randomY = Random.Range(-1f, 1f);

        Vector3 spawnPosition = spawnPoint.position + new Vector3(randomX, randomY, 0f);

        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);

        EnemyMovement movement = newEnemy.GetComponent<EnemyMovement>();
        movement.pathNumber = pathNumber;
    }
    void SpawnEnemy_2(GameObject enemy, Transform spawnPoint, int pathNumber)
    {

        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-10f, 1f);

        Vector3 spawnPosition = spawnPoint.position + new Vector3(randomX, randomY, 0f);

        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);

        EnemyMovement movement = newEnemy.GetComponent<EnemyMovement>();
        movement.pathNumber = pathNumber;
    }
}
