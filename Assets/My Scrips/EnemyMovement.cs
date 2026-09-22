using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyState
    {
        Moving,
        Slowed,
        Dead
    }
    public EnemyState currentState;

    public Transform[] waypoints; // Array of waypoints for the enemy to follow
    private int currentWaypointIndex = 0; // Index of the current waypoint

    public float normalSpeed = 2f;
    public float slowedSpeed = 1f;

    private float currentSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpeed = normalSpeed; // Initialize current speed to normal speed
        currentState = EnemyState.Moving; // Set the initial state of the enemy to Moving

        GameObject waypointParent = GameObject.Find("Way_Point_Manager");
        waypoints = new Transform[waypointParent.transform.childCount]; // Initialize the waypoints array based on the number of child objects
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = waypointParent.transform.GetChild(i); // Assign each child transform to the waypoints array
        }
    }

    // Update is called once per frame
    void Update()
    {
        StateSwitch(); // Call the method to handle state-based behavior


    }

    void StateSwitch()
    {
        switch (currentState)
        {
            case EnemyState.Moving:
                Move();
                break;

            case EnemyState.Slowed:
                Move();
                break;

            case EnemyState.Dead:
                break;
        }
    }

    void Move()
    {

        if (currentWaypointIndex >= waypoints.Length)
        {
            Destroy(gameObject); // Destroy the enemy when it reaches the end of the waypoints
            GameManager.Instance.baseHealth--; // Decrease the base health when an enemy reaches the end of the waypoints
            Debug.Log("Enemy reached the base! Base health: " + GameManager.Instance.baseHealth);

            return; // No more waypoints to follow    
        }
        Transform target = waypoints[currentWaypointIndex]; // Get the current target waypoint

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            currentSpeed * Time.deltaTime
            ); // Move towards the target waypoint

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex++; // Move to the next waypoint when close enough to the current one
        }

        if (currentState == EnemyState.Slowed)
        {
            GetComponent<SpriteRenderer>().color = Color.blue; // Change the enemy's color to blue when slowed
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red; // Reset the enemy's color to white when not slowed
        }
    }

    public void ApplySlow(float duration)
    {
        StopCoroutine("SlowRoutine"); // Stop any existing slow effect
        StartCoroutine(SlowRoutine(duration)); // Start a new slow effect with the specified duration

    }

    IEnumerator SlowRoutine(float duration)
    {
        currentState = EnemyState.Slowed; // Set the enemy state to Slowed
        currentSpeed = slowedSpeed; // Reduce the enemy's speed
        yield return new WaitForSeconds(duration); // Wait for the specified duration
        currentState = EnemyState.Moving; // Reset the enemy state to Moving
        currentSpeed = normalSpeed; // Restore the enemy's speed to normal
    }
}
