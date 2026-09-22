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

    // Array of waypoints for the enemy to follow
    public Transform[] waypoints;
    // Index of the current waypoint
    private int currentWaypointIndex = 0; 

    public float normalSpeed = 2f;
    public float slowedSpeed = 1f;

    private float currentSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize current speed to normal speed
        currentSpeed = normalSpeed;
        // Set the initial state of the enemy to Moving
        currentState = EnemyState.Moving; 

        GameObject waypointParent = GameObject.Find("Way_Point_Manager");
        // Initialize the waypoints array based on the number of child objects
        waypoints = new Transform[waypointParent.transform.childCount]; 
        for (int i = 0; i < waypoints.Length; i++)
        {
            // Assign each child transform to the waypoints array
            waypoints[i] = waypointParent.transform.GetChild(i); 
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
            // Destroy the enemy when it reaches the end of the waypoints
            Destroy(gameObject);
            // Decrease the base health when an enemy reaches the end of the waypoints
            GameManager.Instance.baseHealth--; 
            Debug.Log("Enemy reached the base! Base health: " + GameManager.Instance.baseHealth);

            return; // No more waypoints to follow    
        }
        // Get the current target waypoint
        Transform target = waypoints[currentWaypointIndex]; 

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            currentSpeed * Time.deltaTime
            ); // Move towards the target waypoint

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            // Move to the next waypoint when close enough to the current one
            currentWaypointIndex++; 
        }

        if (currentState == EnemyState.Slowed)
        {
            // Change the enemy's color to blue when slowed
            GetComponent<SpriteRenderer>().color = Color.blue; 
        }
        else
        {
            // Reset the enemy's color to white when not slowed
            GetComponent<SpriteRenderer>().color = Color.red; 
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
