
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerV2 : MonoBehaviour
{
    public enum WaveState
    {
        Preparing,
        Spawning,
        Active,
        Completed,
        Finished
    }

    [System.Serializable]
    public class SpawnGroup
    {
        public GameObject enemyPrefab;

        [Min(1)]
        public int count = 1;

        [Range(0, 2)]
        public int pathNumber = 0;
    }

    [System.Serializable]
    public class WaveDefinition
    {
        public SpawnGroup[] groups;
    }

    [Header("Wave Settings")]
    public WaveDefinition[] waves;

    [Header("Spawn Positions")]
    public Transform[] spawnPositions = new Transform[3];

    [Header("Timers")]
    public float preparationTime = 30f;
    public float spawnInterval = 2f;
    public float completedMessageTime = 3f;

    
    public WaveState CurrentState { get; private set; }
    public float Countdown { get; private set; }

    public int TotalWaves => waves.Length;
    public int CurrentWaveNumber =>
        Mathf.Min(waveIndex + 1, TotalWaves);

    public int RemainingEnemies => activeEnemies.Count;

    private int waveIndex = 0;
    private float completedTimer;

    private List<GameObject> activeEnemies =
        new List<GameObject>();

    private void Start()
    {
        if (waves == null || waves.Length == 0 || spawnPositions == null || spawnPositions.Length < 3)

        {
            Debug.LogError("EnemySpawnerV2: Missing wave or spawn setup.");
            enabled = false;
            return;
        }

        PrepareWave();
    }

    private void Update()
    {
        // Respect the existing pause system.
        if (Time.timeScale == 0f)
            return;

        switch (CurrentState)
        {
            case WaveState.Preparing:
                UpdateCountdown();
                break;

            case WaveState.Spawning:
                UpdateEnemyCount();
                break;

            case WaveState.Active:
                UpdateEnemyCount();

                if (activeEnemies.Count == 0)
                    CompleteWave();

                break;

            case WaveState.Completed:
                completedTimer -= Time.deltaTime;

                if (completedTimer <= 0f)
                    AdvanceWave();

                break;
        }
    }

    private void PrepareWave()
    {
        Countdown = preparationTime;
        CurrentState = WaveState.Preparing;

        Debug.Log("Preparing Wave " + (waveIndex + 1));
    }

    private void UpdateCountdown()
    {
        // Enter starts the wave immediately.
        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            StartWaveEarly();
            return;
        }

        Countdown -= Time.deltaTime;

        if (Countdown <= 0f)
            StartWaveEarly();
    }

    public void StartWaveEarly()
    {
        // Prevent starting the same wave twice.
        if (CurrentState != WaveState.Preparing ||
            Time.timeScale == 0f)
            return;

        CurrentState = WaveState.Spawning;
        Countdown = 0f;

        activeEnemies.Clear();

        // Update the existing GameManager wave number.
        if (GameManager.Instance != null)
            GameManager.Instance.currentWave = waveIndex + 1;

        Debug.Log("Wave " + (waveIndex + 1) + " Started!");

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        WaveDefinition wave = waves[waveIndex];

        if (wave != null && wave.groups != null)
        {
            foreach (SpawnGroup group in wave.groups)
            {
                if (group == null || group.enemyPrefab == null)
                    continue;

                int path = group.pathNumber;

                if (path < 0 || path >= spawnPositions.Length ||
                    spawnPositions[path] == null)
                {
                    Debug.LogWarning("Invalid enemy spawn path.");
                    continue;
                }

                for (int i = 0; i < group.count; i++)
                {
                    GameObject enemy = Instantiate(group.enemyPrefab, spawnPositions[path].position, Quaternion.identity);

                    // Track every enemy spawned by this wave.
                    activeEnemies.Add(enemy);

                    // Assign its existing waypoint route.
                    EnemyMovementV2 movementV2 = enemy.GetComponent<EnemyMovementV2>();

                    if (movementV2 != null)
                    {
                        movementV2.pathNumber = path;
                    }
                    else
                    {
                        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();

                        if (movement != null)
                            movement.pathNumber = path;
                    }

                    yield return new WaitForSeconds(spawnInterval);
                }
            }
        }

        // All enemies have spawned, but may still be alive.
        CurrentState = WaveState.Active;

        Debug.Log("Wave spawning finished.");
    }

    private void UpdateEnemyCount()
    {
        // Remove references to destroyed enemies.
        activeEnemies.RemoveAll(enemy => enemy == null);
    }

    private void CompleteWave()
    {
        CurrentState = WaveState.Completed;
        completedTimer = completedMessageTime;

        Debug.Log("Wave " + (waveIndex + 1) + " Complete!");
    }

    private void AdvanceWave()
    {
        waveIndex++;

        if (waveIndex >= waves.Length)
        {
            CurrentState = WaveState.Finished;
            Debug.Log("All waves completed!");
            return;
        }

        PrepareWave();
    }
}
