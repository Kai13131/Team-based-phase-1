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
    public Transform[] waypoints_1;
    public Transform[] waypoints_2;
    public int pathNumber = 0;
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
        GameObject waypointParent_1 = GameObject.Find("Way_Point_Manager_1");
        GameObject waypointParent_2 = GameObject.Find("Way_Point_Manager_2");

        // Initialize the waypoints array based on the number of child objects
        waypoints = new Transform[waypointParent.transform.childCount];
        waypoints_1 = new Transform[waypointParent_1.transform.childCount];
        waypoints_2 = new Transform[waypointParent_2.transform.childCount];

        if (pathNumber == 0)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                // Assign each child transform to the waypoints array
                waypoints[i] = waypointParent.transform.GetChild(i);
            }
        }
        else if (pathNumber == 1)
        {

            for (int i = 0; i < waypoints.Length; i++)
            {
                // Assign each child transform to the waypoints array
                waypoints_1[i] = waypointParent_1.transform.GetChild(i);
            }
        }
        else if(pathNumber == 2)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                // Assign each child transform to the waypoints array
                waypoints_2[i] = waypointParent_2.transform.GetChild(i);
            }
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
        if(pathNumber == 0)
        {
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
        }
        else if(pathNumber == 1)
        {
            Transform target = waypoints_1[currentWaypointIndex];

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
        }
        else if(pathNumber == 2)
        {
            Transform target = waypoints_2[currentWaypointIndex];

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
        }

        if (currentState == EnemyState.Slowed)
        {
            // Change the enemy's color to blue when slowed
            GetComponent<SpriteRenderer>().color = Color.blue; 
        }
        else
        {
            // Reset the enemy's color to white when not slowed
            GetComponent<SpriteRenderer>().color = Color.white; 
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
