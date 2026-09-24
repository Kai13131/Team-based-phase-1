using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject demonPrefab; // Reference to the enemy prefab
    public GameObject cyclopsPrefab; // Reference to the enemy prefab
    public GameObject nueNIPrefab; // Reference to the enemy prefab

    public Transform spawnPosition; // Position where enemies will be spawned

    public float spawnRate = 2f; // Time interval between spawns

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, spawnRate); // Start spawning enemies at regular intervals    
    }

    void SpawnEnemy()
    {
        Instantiate(demonPrefab, spawnPosition.position, Quaternion.identity); // Spawn an enemy at the specified position
        Instantiate(cyclopsPrefab, spawnPosition.position, Quaternion.identity); // Spawn an enemy at the specified position
        Instantiate(nueNIPrefab, spawnPosition.position, Quaternion.identity); // Spawn an enemy at the specified position
    }

}
